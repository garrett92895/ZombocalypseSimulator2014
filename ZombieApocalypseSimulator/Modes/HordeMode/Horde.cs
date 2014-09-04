using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator.Modes.HordeMode
{
    public class Horde
    {
        public bool Active { get; set; }

        public ObservableCollection<SpawnZoneMarker> SpawnMarkers { get; set; }

        public Wave CurrentWave { get; set; }

        public int TurnsLasting { get; set; }

        private int GridHeight { get; set; }
        private int GridWidth { get; set; }

        public Horde(int NewHeight, int NewWidth)
        {
            GridHeight = NewHeight;
            GridWidth = NewWidth;
            SpawnZoneMarker LeftZone = new SpawnZoneMarker(new Coordinate(0,0), new Coordinate(0,GridHeight));
            SpawnZoneMarker TopZone = new SpawnZoneMarker(new Coordinate(0,0), new Coordinate(GridWidth,0));
            SpawnZoneMarker RightZone = new SpawnZoneMarker(new Coordinate(GridWidth, 0), new Coordinate(GridWidth, GridHeight));
            SpawnZoneMarker BottomZone = new SpawnZoneMarker(new Coordinate(0, GridHeight), new Coordinate(GridWidth, GridHeight));
            SpawnMarkers.Add(LeftZone);
            SpawnMarkers.Add(TopZone);
            SpawnMarkers.Add(RightZone);
            SpawnMarkers.Add(BottomZone);
        }

        /// <summary>
        /// Returns a list of new Zeds without any locations
        /// </summary>
        /// <returns></returns>
        //public List<Zed> NextSpawns()
        //{
        //    //return CurrentWave.NextSpawns();
        //}
    }
}
