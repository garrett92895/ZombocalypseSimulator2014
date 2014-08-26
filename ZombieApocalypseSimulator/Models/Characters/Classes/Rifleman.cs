using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Rifleman : Player
    {
        public Rifleman()
        {
            Name = "Bob";
            Items = new List<Item>();
            ItemLimit = 5;
            //Items.Add(new MeleeWeapon { Condition = 100, Damage = "2d6", IsEquiped = false, 
            //    MeleeWeaponType = MeleeWeaponType.Blunt, Name = "Small Crowbar", IgnoresAR = false });
            Items.Add(new Health { AmountHealed = "2d10", Description = "A Medium Health-kit", Name = "Health Pack" });
            Items.Add(WeaponFactory.GetInstance("Deagle|Ranged|Handgun|100|10d60|12"));
            Items.Add(WeaponFactory.GetInstance("Small Crowbar|Melee|Blunt|100|2d6"));
            //EquippedWeapon = (Weapon)Items.ElementAt(0);
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

        public List<Ammo> RifleManAmmo = new List<Ammo>(200);
        //private readonly byte RiflemanMaxAmmo = 200;

        public byte bonusToAttack()
        {
            byte bonus = 0;
            string weaponType = EquippedWeaponType();
            if (weaponType == "Ranged")
            {
                bonus = 1;
            }
            return bonus;
        }

        public override int RangedAttack()
        {
            byte hit = (byte)(Dice.Roll(1, 20));
            hit += bonusToAttack();
            return hit;
        }

        public override string ToString()
        {
            string s = "";
            s += "Class: Rifleman";
            s += "\r\nHealth: " + Health + "/" + MaxHealth;
            s += "\r\nSDC: " + sdc + "/" + MaxSDC;
            s += "\r\nLevel: " + Level;
            s += "\r\nSpeed: " + Speed;
            s += "\r\nIQ: " + IntelligenceQuotient;
            s += "\r\nME: " + MentalEndurance;
            s += "\r\nMA: " + MentalAffinity;
            s += "\r\nPS: " + PhysicalStrength;
            s += "\r\nPP: " + PhysicalProwess;
            s += "\r\nPB: " + PhysicalBeauty;
            s += "\r\nPE: " + PhysicalEndurance;

            return s;
        }
    }
}