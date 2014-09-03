using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Modes.HordeMode
{
    public class Horde
    {
        public bool Active { get; set; }

        public ObservableCollection<SpawnZoneMarker> SpawnMarkers { get; set; }

        public Wave CurrentWave { get; set; }
    }
}
