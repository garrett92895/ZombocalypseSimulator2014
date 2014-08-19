using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypse
{
    public static class Dice
    {
        private static Random Rand = new Random();
        /// <summary>
        /// Rolls a given number of dice who have a given number of sides.  Has optional paramaters 
        /// that allow a multiplyer to modify the total roll, and a modifyer to be added to the die roll before the multiplyer
        /// </summary>
        /// <param name="NumberOfDice"></param>
        /// <param name="SidesPerDie"></param>
        /// <returns></returns>
        public static int Roll(int NumberOfDice, int SidesPerDie, int Multiplyer = 1, int Modifyer = 0)
        {
            int ReturnValue = 0;

            for (; NumberOfDice > 0; NumberOfDice--)
            {
                ReturnValue += 1 + Rand.Next(SidesPerDie);
            }
            return (ReturnValue + Modifyer) * Multiplyer;
        }

        /// <summary>
        /// Rolls a given number of dice who have a given number of sides.  Has optional paramaters 
        /// that allow a multiplyer to modify the total roll, and a modifyer to be added to the die roll before the multiplyer. 
        /// With the expected format of #d#+#*#
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static int Roll(string Input)
        {
            int ReturnValue = Rand.Next(20);


			return ReturnValue;
        }
    }
}