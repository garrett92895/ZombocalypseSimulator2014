﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator
{
    [Serializable()]
    public class DieRoll
    {

        public int NumberOfDice { get; set; }
        public int SidesPerDie { get; set; }
        public int Multiplier { get; set; }
        public int Modifyer { get; set; }

        private static Random Rand = new Random();
        /// <summary>
        /// Makes a DieRoll class which will store the number of dice and number of sides per die, along with the Multiplier and Modifyer for the roll to be made
        /// </summary>
        /// <param name="NumberOfDice"></param>
        /// <param name="SidesPerDie"></param>
        /// <returns></returns>
        public DieRoll(int NewNumberOfDice, int NewSidesPerDie, int NewMultiplyer = 1, int NewModifyer = 0)
        {
            NumberOfDice = NewNumberOfDice;
            SidesPerDie = NewSidesPerDie;
            Multiplier = NewMultiplyer;
            Modifyer = NewModifyer;
        }

        /// <summary>
        /// Rolls a given number of dice who have a given number of sides.  Has optional paramaters 
        /// that allow a multiplyer to modify the total roll, and a modifyer to be added to the die roll before the multiplyer
        /// </summary>
        /// <param name="NumberOfDice"></param>
        /// <param name="SidesPerDie"></param>
        /// <returns></returns>
        public int Roll()
        {
            int ReturnValue = 0;

            for ( int i = NumberOfDice; i > 0; i--)
            {
                ReturnValue += 1 + Rand.Next(SidesPerDie);
            }
            return (ReturnValue + Modifyer) * Multiplier;
        }

        /// <summary>
        /// Rolls a die with the given number of sides and returns the result of the roll.  
        /// In other words, returns a random number between 1 and the given number inclusive.
        /// </summary>
        /// <param name="NumberOfSides"></param>
        /// <returns></returns>
        public static int RollOne(int NumberOfSides)
        {
            return Rand.Next(NumberOfSides) + 1;
        }
    }
}