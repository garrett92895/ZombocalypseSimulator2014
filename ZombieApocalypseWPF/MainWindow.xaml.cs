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

namespace ZombieApocalypseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clears the board and all data objects to make a new instance of the game, should prompt for user input on saving current state
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Opens a saved game file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Closes the game, should prompt for user input about saving the current state of the game
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
