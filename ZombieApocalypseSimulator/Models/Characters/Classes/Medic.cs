using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Medic : Player
    {
        public static List<Health> DocsHealthPacks = new List<Health>
        {
            new Health { AmountHealed = "1d10", Name = "A Small Health-kit"},
            new Health { AmountHealed = "2d10", Name = "A Medium Health-kit"},
            new Health { AmountHealed = "3d10", Name = "A Large Health-kit"},

        };

        public void heal(Coordinate c)
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
                }
            }
        }

        public void revive(Coordinate c)
        {
            if (c.GetType() == typeof(Corpse))
            {
                if (DocsHealthPacks.Count > 1)
                {
                    int rev = Dice.Roll(1, 20);
                    if (rev >= 14)
                    {
                        
                    }
                }
            }
        }
    }
}