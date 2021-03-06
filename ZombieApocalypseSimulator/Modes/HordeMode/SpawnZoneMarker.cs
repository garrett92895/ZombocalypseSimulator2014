﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Modes.HordeMode
{
    [Serializable()]
    public class SpawnZoneMarker
    {
        public Coordinate TopLeft { get; set; }
        public Coordinate BottomRight { get; set; }

        public SpawnZoneMarker(Coordinate NewTopLeft, Coordinate NewBottomRight)
        {
            TopLeft = NewTopLeft;
            BottomRight = NewBottomRight;
        }
    }
}