using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Bruiser : Player
    {
        public Bruiser()
        {
            Name = "Phill";
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

        public override int rollsdc()
        {
            int armor = (12 + new DieRoll(2, 6).Roll());
            return armor;
        }

        public bool Shout()
        {
            bool isEffective = false;
            int shoutRoll = new DieRoll(1, 30).Roll();
            if (shoutRoll <= MentalAffinity)
            {
                isEffective = true;
            }
            return isEffective;
        }

        public override string ToString()
        {
            string s = "";
            s += "Class: Bruiser";
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