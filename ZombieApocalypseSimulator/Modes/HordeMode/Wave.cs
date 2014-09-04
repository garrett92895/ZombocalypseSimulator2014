using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator.Modes.HordeMode
{
    public class Wave
    {
        /// <summary>
        /// Total number of Zeds to spawn in this wave
        /// </summary>
        public int StartZedCount { get; private set; }

        /// <summary>
        /// Number of Zeds that have not spawned
        /// </summary>
        public int ZedCount { get; set; }

        /// <summary>
        /// The number of Rounds that the Wave should last, will spawn an equal number of Zeds each round
        /// </summary>
        public int NumberOfRounds { get; set; }

        /// <summary>
        /// Percentage of the spawned Zeds that will be Specials
        /// </summary>
        public int SpecialSpawnRate { get; set; }

        /// <summary>
        /// Maximum percentage of the spawned Zeds that can be Specials
        /// </summary>
        public int MaxSpecialSpawnRate { get; set; }

        /// <summary>
        /// Number of Rounds in between each Wave
        /// </summary>
        public int BreakRounds { get; set; }

        /// <summary>
        /// Counts the number of rounds that have gone by in a break
        /// </summary>
        private int BreakRoundCounter { get; set; }

        public Wave(int NewZedCount, int NewNumberOfRounds = 5, int NewSpecialSpawnRate = 5, int NewMaxSpecialSpawnRate = 40, int NewBreakRounds = 5)
        {
            StartZedCount = NewZedCount;
            ZedCount = StartZedCount;
            NumberOfRounds = NewNumberOfRounds;
            SpecialSpawnRate = NewSpecialSpawnRate;
            MaxSpecialSpawnRate = NewMaxSpecialSpawnRate;
            BreakRounds = NewBreakRounds;
        }

        /// <summary>
        /// Sets the values to increase to the next wave
        /// </summary>
        public void NextWave()
        {
            StartZedCount += StartZedCount / 2;
            ZedCount = StartZedCount;

            SpecialSpawnRate += 5;
            if (SpecialSpawnRate > MaxSpecialSpawnRate)
            {
                SpecialSpawnRate = MaxSpecialSpawnRate;
            }
            BreakRoundCounter = 0;
        }

        public List<Zed> NextSpawns()
        {
            List<Zed> NewZeds = new List<Zed>();
            //Spawns Zeds if the break is over
            if (BreakRoundCounter > BreakRounds)
            {
                int ZedsThisSpawn = (StartZedCount / NumberOfRounds) + 1;
                if (ZedsThisSpawn > ZedCount)
                {
                    ZedsThisSpawn = ZedCount;
                }
                for (int i = 0; i < ZedsThisSpawn; i++)
                {
                    bool SpawnSpecial = DieRoll.RollOne(100) < SpecialSpawnRate;
                    Zed NewZed = (SpawnSpecial) ? ZedFactory.RandomSpecial() : ZedFactory.GetInstance("Sloucher");
                    NewZeds.Add(NewZed);
                }
                ZedCount -= ZedsThisSpawn;
                if (ZedCount <= 0)
                {
                    NextWave();
                }
            }
            //Does not spawn Zeds if the break is not over yet
            else
            {
                BreakRoundCounter++;
            }
            return NewZeds;
        }
    }
}
