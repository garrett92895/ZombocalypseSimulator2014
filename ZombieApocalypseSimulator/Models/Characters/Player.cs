﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Characters
{
    class Player : Character
    {
        public string Name { get; set; }
        public int ItemLimit { get; set; }
        public List<Item> Items { get; set; }
        public Weapon EquippedWeapon { get; set; }
        private byte rollAttributes()
        {
            byte roll = (byte)(Dice.Roll(3, 6));
            if (roll == 16 || roll == 17 || roll == 18)
            {
                roll += (byte)(Dice.Roll(1, 6));
                if (roll == 22 || roll == 23 || roll == 24)
                {
                    roll += (byte)(Dice.Roll(1, 6));
                }
            }
            byte attribute = (byte)roll;
            return roll;
        }

        public Player()
        {
            Items = new List<Item>();
            ItemLimit = 5;
            Items.Add(new MeleeWeapon { Condition = 100, Damage = "2d6", IsEquiped = false, 
                MeleeWeaponType = MeleeWeaponType.Blunt, Name = "Small Crowbar", IgnoresAR = false });
            EquippedWeapon = (Weapon)Items.ElementAt(0);
            IntelligenceQuotient = rollAttributes();
            MentalEndurance = rollAttributes();
            MentalAffinity = rollAttributes();
            PhysicalStrength = rollAttributes();
            PhysicalEndurance = rollAttributes();
            PhysicalProwess = rollAttributes();
            PhysicalBeauty = rollAttributes();
            Speed = rollAttributes();
            ArmorRating = 0;
            base.SetLife();
        }

		public Player(byte IQ, byte ME, byte MA, byte PS, byte PE, byte PP, byte PB, byte SP)
		{
			IntelligenceQuotient = IQ;
			MentalEndurance = ME;
			MentalAffinity = MA;
			PhysicalStrength = PS;
			PhysicalEndurance = PE;
			PhysicalProwess = PP;
			PhysicalBeauty = PB;
			Speed = SP;
		}

        private readonly byte maxAmmo = 100;

        public override int MeleeAttack()
        {
            int Damage = EquippedWeapon.UseWeapon();
            Damage += bonusPS();
            //return 1000;
            return Damage;
        }
        public int RangedAttack()
        {
            return EquippedWeapon.UseWeapon();
        }

        public string EquippedWeaponType()
        {
            string type = null;
            if(EquippedWeapon.GetType() == typeof(RangedWeapon))
            {
                type = "Ranged";
            }
            else
            {
                type = "Melee";
            }
            return type;
        }

        public void AddItem(Item ItemToAdd)
        {
            if(Items.Count()  < ItemLimit)
            {
                Items.Add(ItemToAdd);
            }
        }
    
        public List<Weapon> GetWeapons()
        {
            List<Weapon> Weapons = new List<Weapon>();
            for(int i = 0; i < Items.Count(); i ++)
            {
                if(Items.ElementAt(i).GetType() == typeof(Weapon))
                {
                    Weapons.Add((Weapon)Items.ElementAt(i));
                }
            }
            return Weapons;
        }

        /// <summary>
        /// Returns true if the player has any weapons
        /// at all
        /// </summary>
        /// <returns></returns>
        public bool HasWeapon()
        {
            bool HasWeapon = false;
            if (GetWeapons().Any())
            {
                HasWeapon = true;
            }
            return HasWeapon;
        }

        public bool CanShoot()
        {
            bool CanShoot = false;

            if(EquippedWeaponType().Equals("Ranged"))
            {
                RangedWeapon EquippedRangedWeapon = (RangedWeapon)EquippedWeapon;
                if(EquippedRangedWeapon.CurrentClip.Amount > 0)
                {
                    CanShoot = true;
                }
            }

            return CanShoot;
        }

        public override string ToString()
        {
            string s = base.ToString();
            s += "\r\nInventory...";
            for(int i = 0; i < Items.Count(); i++)
            {
                s += "\r\n" + Items.ElementAt(i).ToString();
            }
            return s;
        }
    }
}