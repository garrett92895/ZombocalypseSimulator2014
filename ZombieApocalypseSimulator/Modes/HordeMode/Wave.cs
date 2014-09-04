using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator.Modes.HordeMode
{
    public class Wave
    {
        public int StartZedCount { get; private set; }
        public int ZedCount { get; set; }
        public int SpawnRate { get; set; }
        public int SpecialSpawnRate { get; set; }

        public Wave(int NewZedCount, int NewSpawnRate = 20, int NewSpecialSpawnRate = 5)
        {
            StartZedCount = NewZedCount;
            ZedCount = StartZedCount;
            SpawnRate = NewSpawnRate;
            SpecialSpawnRate = NewSpecialSpawnRate;
        }

        /// <summary>
        /// Sets the values to increase to the next wave
        /// </summary>
        public void NextWave()
        {
            StartZedCount += StartZedCount / 2;
            ZedCount = StartZedCount;

            SpecialSpawnRate += 5;

            //StartSpecialSpawnRate += ;
        }

        //public List<Zed> NextSpawns()
        //{
        //    int ZedsThisSpawn = StartZedCount / SpawnRate;
        //    List<Zed> NewZeds = new List<Zed>(ZedsThisSpawn);
        //}
    }
}
