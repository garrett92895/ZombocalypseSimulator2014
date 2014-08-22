using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Characters.Classes;

namespace ZombieApocalypseSimulator.Factories
{
    public static class ZedFactory
    {
        public static Random rand = new Random();
        public static Zed RandomZed()
        {
            return MakeZeds().ElementAt(rand.Next(4));
        }

        public static Zed RandomSpecial()
        {
            List<Zed> Zeds = MakeZeds();
            return MakeZeds().ElementAt(rand.Next(3));
        }

        public static Zed GetInstance(string zombieType)
        {
            List<Zed> Zeds = MakeZeds();
            Zed NewZed = null;
            switch(zombieType)
            {
                case "Tank":
                    NewZed = Zeds.ElementAt(0);
                    break;
                case "FastAttack":
                    NewZed = Zeds.ElementAt(1);
                    break;
                case "Shank":
                    NewZed = Zeds.ElementAt(2);
                    break;
                default:
                    NewZed = Zeds.ElementAt(3);
                    break;
            }
            return NewZed;
        }

        private static List<Zed> MakeZeds()
        {
            List<Zed> Zeds = new List<Zed>();
            Zeds.Add(new Tank());
            Zeds.Add(new FastAttack());
            Zeds.Add(new Shank());
            Zeds.Add(new Sloucher());
            return Zeds;
        }
    }
}
