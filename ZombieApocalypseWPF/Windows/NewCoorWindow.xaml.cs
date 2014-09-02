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

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for NewCoorWindow.xaml
    /// </summary>
    public partial class NewCoorWindow : Window
    {
        public NewCoorWindow(int maxX, int maxY)
        {
            InitializeComponent();
            List<int> possX = new List<int>();
            List<int> possY = new List<int>();

            int i = 0;

            while (i <= maxX || i <= maxY)
            {
                i++;
                if (i <= maxX)
                    possX.Add(i);
                if (i <= maxY)
                    possY.Add(i);
            }

            xCoor.ItemsSource = possX;
            yCoor.ItemsSource = possY;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (xCoor.SelectedValue == null || yCoor.SelectedValue == null)
            {
                MessageBox.Show("You have failed to select an X and Y");
                    return;
            }
            MainWindow.xCoor = (int)xCoor.SelectedValue - 1;
            MainWindow.yCoor = (int)yCoor.SelectedValue - 1;
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
