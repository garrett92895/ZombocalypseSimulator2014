using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Modes.HordeMode
{
    public class Wave
    {
        public int StartZedCount { get; private set; }
        public int ZedCount { get; set; }

        public int SpecialSpawnRate { get; set; }

        /// <summary>
        /// Sets the values to increase to the next wave
        /// </summary>
        public void NextWave()
        {
            StartZedCount += StartZedCount / 2;
            ZedCount = StartZedCount;

            //StartSpecialSpawnRate += ;
        }
    }
}
