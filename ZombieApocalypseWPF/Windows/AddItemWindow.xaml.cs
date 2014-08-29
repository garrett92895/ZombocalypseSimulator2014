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
using System.Windows.Shapes;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        Item I = new Item();

        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void DoneWithItem()
        {
        }
    }
}
