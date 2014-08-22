using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZombieApocalypseWPF.UserControls
{
    /// <summary>
    /// Interaction logic for CharacterControl.xaml
    /// </summary>
    public partial class CharacterControl : UserControl
    {
        public ZombieApocalypseSimulator.Models.Characters.Character _c;
        public ZombieApocalypseSimulator.Models.Characters.Character c 
        {
            get
            {
                return _c;
            }

            set
            {
                _c = value;
                this.DataContext = c;
                this.InventoryGrid.ItemsSource = c.Items;
            }
        }
        public CharacterControl()
        {
            InitializeComponent();
            this.DataContext = c;
        }
    }
}
