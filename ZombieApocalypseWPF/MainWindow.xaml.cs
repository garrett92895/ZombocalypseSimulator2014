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
using ZombieApocalypseSimulator;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseWPF.Converters;

namespace ZombieApocalypseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Controller c;
        public RoutedCommand Add_Item = new RoutedCommand();
        public RoutedCommand Add_Player = new RoutedCommand();
        public RoutedCommand Add_Zombie = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();

            c = new Controller();
            Character NewPlayer = new Bruiser();
            Coordinate Coor = new Coordinate(2, 3);
            c.AddCharacterToField(NewPlayer, Coor);
            PlayerControl.c = NewPlayer;

            Character Zed1 = ZedFactory.GetInstance("Tank");
            Coordinate ZedCoor1 = new Coordinate(1, 2);
            c.AddCharacterToField(Zed1, ZedCoor1);
            ZombieControl.c = Zed1;

            //Character Zed2 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor2 = new Coordinate(8, 9);
            //c.AddCharacterToField(Zed2, ZedCoor2);

            //Character Zed3 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor3 = new Coordinate(7, 7);
            //c.AddCharacterToField(Zed3, ZedCoor3);

            //Character Zed4 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor4 = new Coordinate(7, 8);
            //c.AddCharacterToField(Zed4, ZedCoor4);

            //Character Zed5 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor5 = new Coordinate(7, 9);
            //c.AddCharacterToField(Zed5, ZedCoor5);

            //Character Zed6 = ZedFactory.GetInstance("Sloucher");
            //Coordinate ZedCoor6 = new Coordinate(5, 5);
            //c.AddCharacterToField(Zed6, ZedCoor6);

            //Item Gun = WeaponFactory.GetInstance("Ranged|Shotgun|80|3d6|4");
            //Coordinate GunCoor = new Coordinate(1, 2);
            //c.AddItemToField(Gun, GunCoor);

            PopulateBoard();

        }

        private void PopulateBoard()
        {

            Board.Rows = c.Field.Height;
            Board.Columns = c.Field.Width;

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    Canvas nc = new Canvas();
                    //nc.Resources.Add("Trap", c.Field.GridSquares[i, j].ActiveTrap);
                    //nc.Resources.Add("Coordinate", c.Field.GridSquares[i, j].Coordinate);
                    //nc.Resources.Add("IsOccupiable", c.Field.GridSquares[i, j].IsOccupiable);
                    //nc.Resources.Add("ItemList", c.Field.GridSquares[i, j].ItemList);
                    //nc.Resources.Add("Character", c.Field.GridSquares[i, j].OccupyingCharacter);

                    Rectangle ci = new Rectangle();

                    Binding b = new Binding();
                    b.Source = c.Field.GridSquares[i, j].OccupyingCharacter;
                    b.Converter = new CharacterToImageConverter();

                    ci.SetBinding(Rectangle.FillProperty, b);
                    ci.Height = 45;
                    ci.Width = 45;


                    nc.Children.Add(ci);

                    nc.Margin = new Thickness(1);
                    nc.Background = Brushes.White;

                    Board.Children.Add(nc);

                }

            }
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

        private void Add_Item_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Add_Player_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Add_Zombie_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        
    }
}
