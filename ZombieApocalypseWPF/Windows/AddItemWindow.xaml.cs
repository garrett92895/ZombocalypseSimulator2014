using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZombieApocalypseSimulator;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        ObservableCollection<Item> inventory = new ObservableCollection<Item>();
        public GameArea Field { private get; set; }
        public AddItemWindow()
        {
            InitializeComponent();
            Inventory.ItemsSource = inventory;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (ItemType.SelectedIndex)
            {
                case 0:
                    inventory.Add(new Corpse(new Bruiser()));
                    break;
                case 1:
                    inventory.Add(new Health());
                    break;
                case 2:
                    inventory.Add(new Magazine());
                    break;
                case 3:
                    inventory.Add(new MeleeWeapon());
                    break;
                case 4:
                    inventory.Add(new RangedWeapon());
                    break;
                case 5:
                    inventory.Add(new SparePart());
                    break;
                case 6:
                    inventory.Add(new Trap());
                    break;
            }
        }
    }
}
