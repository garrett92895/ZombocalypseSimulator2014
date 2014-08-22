using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using Microsoft.Win32;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;

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
            Character pc = new Bruiser();
            Character zc = new Tank();

            this.PlayerControl.c = pc;
            this.ZombieControl.c = zc;
        }

        /// <summary>
        /// Clears the board and all data objects to make a new instance of the game, should prompt for user input on saving current state
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SavePrompt();
            //[controller] = new Controller(BoardWidth,BoardHeight);
        }

        /// <summary>
        /// Opens a saved game file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Saved Games|*.zombieapoc";
            if (OpenFile.ShowDialog() == true)
            {
                BinaryFormatter bf = new BinaryFormatter();
                Stream input;

                if ((input = OpenFile.OpenFile()) != null)
                {
                    throw new NotImplementedException();
                }
                input.Close();
            }
        }

        /// <summary>
        /// Closes the game, should prompt for user input about saving the current state of the game
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SavePrompt();
            this.Close();
        }

        /// <summary>
        /// Prompts the user to save their current information
        /// </summary>
        private void SavePrompt()
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Saved Games|*.zombieapoc";

            if (SaveFile.ShowDialog() == true)
            {
                BinaryFormatter bf = new BinaryFormatter();
                Stream output = File.Create(SaveFile.FileName);

                if (output != null)
                {
                    throw new NotImplementedException();
                }
            }
        }
        
    }
}
