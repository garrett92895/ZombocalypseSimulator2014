using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator
{
    public class Settings : INotifyPropertyChanged
    {
        #region Props and Backing Fields
        private int _WeaponDropRate;
        private int _HealthPackDropRate;
        private int _SparePartDropRate;
        private int _AmmoDropRate;
        public int WeaponDropRate 
        {
            get
            {
                return _WeaponDropRate;
            }
            set
            {
                _WeaponDropRate = value;
                NotifyPropertyChanged("WeaponDropRate");
            }
        }
        public int HealthPackDropRate
        {
            get
            {
                return _HealthPackDropRate;
            }
            set
            {
                _HealthPackDropRate = value;
                NotifyPropertyChanged("HealthPackDropRate");
            }
        }
        public int SparePartDropRate
        {
            get
            {
                return _SparePartDropRate;
            }
            set
            {
                _SparePartDropRate = value;
                NotifyPropertyChanged("SparePartDropRate");
            }
        }
        public int AmmoDropRate
        {
            get
            {
                return _AmmoDropRate;
            }
            set
            {
                _AmmoDropRate = value;
                NotifyPropertyChanged("AmmoDropRate");
            }
        }
        public bool CanEdit { get; set; }
        public bool EnforceTurnOrder { get; set; }
        public bool ShowBattleScene { get; set; }
        #endregion

        public Settings()
        {
            int StartingPercentage = 10;
            WeaponDropRate = StartingPercentage;
            HealthPackDropRate = StartingPercentage;
            SparePartDropRate = StartingPercentage;
            AmmoDropRate = StartingPercentage;
            CanEdit = true;
            EnforceTurnOrder = false;
            ShowBattleScene = true;
        }

        [field:NonSerialized () ]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String Info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Info));
            }
        }
    }
}
