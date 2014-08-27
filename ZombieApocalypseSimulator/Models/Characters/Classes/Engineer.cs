using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Engineer : Player
    {
        public Engineer()
        {
            Name = "Jane";
            Items = new List<Item>();
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


        public List<SparePart> Part = new List<SparePart>();
        public void GetParts()
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                if (Items.ElementAt(i).GetType() == typeof(SparePart))
                {
                    if (Part.Count < 11)
                    {
                        Part.Add((SparePart)Items.ElementAt(i));
                    }
                }
            }
        }

        public void AddTrap()
        {
            if (Part.Count > 3)
            {
                if (Items.Count() < ItemLimit)
                {
                    Items.Add(new Trap { Damage = "2d4", StatusEffect = StatusEffect.Crippled, Name = "Engineer Trap" });
                }
            }
        }

        public void FixWeapon(Weapon weapon)
        {
            if (Part.Count > 1)
            {
                double fix = (double)weapon.Condition;
                fix += weapon.Condition * .3;
            }
        }

        public override string ToString()
        {
            string s = "";
            s += "Class: Engineer";
            s += "\r\nHealth: " + Health + "/" + MaxHealth;
            s += "\r\nSDC: " + SDC + "/" + MaxSDC;
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