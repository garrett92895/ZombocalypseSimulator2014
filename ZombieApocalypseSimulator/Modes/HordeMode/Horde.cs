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
        public bool IsActive { get; set; }

        public ObservableCollection<SpawnZoneMarker> SpawnMarkers { get; set; }

        public Wave CurrentWave { get; set; }

        public int TurnsLasting { get; set; }

        private int GridHeight { get; set; }
        private int GridWidth { get; set; }

        public Horde(int NewHeight, int NewWidth, bool NewIsActive = true, Wave SetWave = null)
        {
            GridHeight = NewHeight;
            GridWidth = NewWidth;
            IsActive = NewIsActive;
            SpawnZoneMarker LeftZone = new SpawnZoneMarker(new Coordinate(0,0), new Coordinate(0,GridHeight-1));
            SpawnZoneMarker TopZone = new SpawnZoneMarker(new Coordinate(0,0), new Coordinate(GridWidth-1,0));
            SpawnZoneMarker RightZone = new SpawnZoneMarker(new Coordinate(GridWidth, 0), new Coordinate(GridWidth-1, GridHeight-1));
            SpawnZoneMarker BottomZone = new SpawnZoneMarker(new Coordinate(0, GridHeight), new Coordinate(GridWidth-1, GridHeight-1));
            SpawnMarkers = new ObservableCollection<SpawnZoneMarker>();
            SpawnMarkers.Add(LeftZone);
            SpawnMarkers.Add(TopZone);
            SpawnMarkers.Add(RightZone);
            SpawnMarkers.Add(BottomZone);

            if (SetWave == null)
            {
                SetWave = new Wave(10);
            }
            CurrentWave = SetWave;
        }

        /// <summary>
        /// Returns a list of the new Zeds in this wave without any locations
        /// </summary>
        /// <returns></returns>
        public List<Zed> NextSpawns()
        {
            return CurrentWave.NextSpawns();
        }
    }
}
