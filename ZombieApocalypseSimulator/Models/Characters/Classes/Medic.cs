using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Items.Add(WeaponFactory.GetInstance("Deagle|Ranged|Handgun|100|10d60|12"));
            Items.Add(WeaponFactory.GetInstance("Small Crowbar|Melee|Blunt|100|2d6"));
            EquippedWeapon = (Weapon)Items.ElementAt(1);
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
            new Health { AmountHealed = new DieRoll(1,10), Name = "A Small Health-kit"},
            new Health { AmountHealed = new DieRoll(2,10), Name = "A Medium Health-kit"},
            new Health { AmountHealed = new DieRoll(3,10), Name = "A Large Health-kit"},

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
                        heal = new DieRoll(1, 10).Roll();
                    }
                    else if(DocsHealthPacks.Equals("A Medium Health-kit"))
                    {
                        heal = new DieRoll(2, 10).Roll();;
                    }
                    else if (DocsHealthPacks.Equals("A Large Health-kit"))
                    {
                        heal = new DieRoll(3, 10).Roll(); ;
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
                    int rev = new DieRoll(1, 20).Roll(); ;
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
            return base.ToString();
        }
    }
}