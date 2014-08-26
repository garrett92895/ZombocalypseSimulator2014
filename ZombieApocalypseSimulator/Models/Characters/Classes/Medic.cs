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
    public class Medic : Player
    {
        public Medic()
        {
            Name = "Phill";
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

        public static List<Health> DocsHealthPacks = new List<Health>
        {
            new Health { AmountHealed = "1d10", Name = "A Small Health-kit"},
            new Health { AmountHealed = "2d10", Name = "A Medium Health-kit"},
            new Health { AmountHealed = "3d10", Name = "A Large Health-kit"},

        };

        public void heal(Coordinate c, Character character)
        {
            if (c.GetType() == typeof(Player))
            {
                if (DocsHealthPacks.Count > 0)
                {
                    int heal = 0;
                    if (DocsHealthPacks.Equals("A Small Health-kit"))
                    {
                        heal = Dice.Roll(1, 10);
                    }
                    else if(DocsHealthPacks.Equals("A Medium Health-kit"))
                    {
                        heal = Dice.Roll(2, 10);
                    }
                    else if (DocsHealthPacks.Equals("A Large Health-kit"))
                    {
                        heal = Dice.Roll(3, 10);
                    }
                    character.Health += heal;
                    Console.WriteLine("You heald for " + heal + "health points.");
                }
            }
        }

        public bool Revive(Coordinate c)
        {
            bool revive = false;
            if (c.GetType() == typeof(Corpse))
            {
                if (DocsHealthPacks.Count > 1)
                {
                    int rev = Dice.Roll(1, 20);
                    if (rev >= 14)
                    {
                        revive = true;
                    }
                }
            }
            return revive;
        }

        public override string ToString()
        {
            string s = "";
            s += "Class: Medic";
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