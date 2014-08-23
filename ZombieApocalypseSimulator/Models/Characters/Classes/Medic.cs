using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void heal()
        {

        }
    }
}