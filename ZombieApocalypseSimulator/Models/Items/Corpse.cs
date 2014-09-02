using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator.Models.Items
{
    public class Corpse : Item
    {
        public Character OriginalCharacter { get; set; }
        private int TurnCounter;
        private DieRoll ReviveRoll;

        public Corpse(Character C)
        {
            OriginalCharacter = C;
            TurnCounter = 0;
            ReviveRoll = new DieRoll(1, 100);
        }

        /// <summary>
        /// Rolls to see if this Corpse should become a Zombie
        /// </summary>
        /// <returns></returns>
        public bool RollRevive()
        {
            TurnCounter++;
            int Roll = ReviveRoll.Roll();
            return Roll < (5 * TurnCounter);
        }

        /// <summary>
        /// Makes a random type of Zed with the same properties as this corpse
        /// </summary>
        /// <returns></returns>
        public Zed SpawnZed()
        {
            Zed Z = ZedFactory.RandomSpecial();
            Z.Level = OriginalCharacter.Level;
            Z.Location = OriginalCharacter.Location;
            Z.PhysicalBeauty = OriginalCharacter.PhysicalBeauty - 2;
            Z.Speed = OriginalCharacter.Speed;

            return Z;
        }

    }
}