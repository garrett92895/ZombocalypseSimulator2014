using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZombieApocalypseSimulator.Modes.HordeMode;

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for HordeSettings.xaml
    /// </summary>
    public partial class HordeSettings : Window , INotifyPropertyChanged
    {
        
        private Horde _HordeMode;
        public Horde HordeMode 
        { 
            get { return _HordeMode; } 
            set
            {
                _HordeMode = value;
                NotifyPropertyChanged("HordeMode");
            }
        }
        public HordeSettings(Horde NewHordeMode)
        {
            HordeMode = NewHordeMode;
            this.DataContext = HordeMode;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string Info)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Info));
            }
        }
    }
}
