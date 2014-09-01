using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class HalfZombie : Player
    {
        public HalfZombie()
        {
            Name = "Phill";
            Items = new ObservableCollection<Item>();
            ItemLimit = 5;
            //Items.Add(new MeleeWeapon { Condition = 100, Damage = "2d6", IsEquiped = false, 
            //    MeleeWeaponType = MeleeWeaponType.Blunt, Name = "Small Crowbar", IgnoresAR = false });
            Items.Add(WeaponFactory.GetInstance("Deagle|Ranged|Handgun|100|10d60|12"));
            Items.Add(WeaponFactory.GetInstance("Small Crowbar|Melee|Blunt|100|2d6"));
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


        private bool turnningCheck(byte roll)
        {
            byte check = (byte) DieRoll.RollOne(20);

            if (roll == 2)
            {
                if (check <= 2)
                {
                    isAlive = false;
                }
            }
            else if (roll == 4)
            {
                if (check <= 4)
                {
                    isAlive = false;
                }
            }
            return isAlive;
        }


        public override Attack MeleeAttack()
        {
            int Damage = 0;
            if (EquippedWeapon.Condition > 10)
            {
                Damage = EquippedWeapon.UseWeapon();
                Damage += bonusPS();
                Damage += 1;
            }
            else
            {
                Damage = 0;
            }
            return new Attack(Damage, false);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}