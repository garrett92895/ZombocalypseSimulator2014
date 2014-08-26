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
using ZombieApocalypseWPF.UserControls;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Controller c;
        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get
            {
                return _selectedPlayer;
            }
            set
            {
                _selectedPlayer = value;
                this.PlayerControl.c = _selectedPlayer;
            }
        } 
        
        private Zed _selectedZombie;
        public Zed SelectedZombie
        {
            get
            {
                return _selectedZombie;
            }
            set
            {
                _selectedZombie = value;
                this.ZombieControl.c = _selectedZombie;
            }
        }



        public MainWindow()
        {
            InitializeComponent();
            PlayerControl.Level_Up_Button.Click += Level_Up_Player_Button_Click;
            PlayerControl.Level_Down_Button.Click += Level_Down_Player_Button_Click;
            ZombieControl.Level_Up_Button.Click += Level_Up_Zombie_Button_Click;
            ZombieControl.Level_Down_Button.Click += Level_Down_Zombie_Button_Click;

            c = new Controller();
            Character NewPlayer = new Bruiser();
            Coordinate Coor = new Coordinate(2, 3);
            c.AddCharacterToField(NewPlayer, Coor);
            PlayerControl.c = NewPlayer;
            SelectedPlayer = (Player)NewPlayer;

            Character NewPlayer2 = new Medic();
            Coordinate Coor2 = new Coordinate(3, 3);
            c.AddCharacterToField(NewPlayer2, Coor2);

            Character NewPlayer3 = new HalfZombie();
            Coordinate Coor3 = new Coordinate(4, 2);
            c.AddCharacterToField(NewPlayer3, Coor3);


            Character NewPlayer4 = new Engineer();
            Coordinate Coor4 = new Coordinate(4, 3);
            c.AddCharacterToField(NewPlayer4, Coor4);

            //Character NewPlayer5 = new Fighter();
            //Coordinate Coor5 = new Coordinate(0, 0);
            //c.AddCharacterToField(NewPlayer5, Coor5);

            Character NewPlayer6 = new Rifleman();
            Coordinate Coor6 = new Coordinate(5, 2);
            c.AddCharacterToField(NewPlayer6, Coor6);

            Character Zed1 = ZedFactory.GetInstance("Tank");
            Coordinate ZedCoor1 = new Coordinate(1, 2);
            c.AddCharacterToField(Zed1, ZedCoor1);
            ZombieControl.c = Zed1;
            SelectedZombie = (Zed)Zed1;

            Character Zed2 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor2 = new Coordinate(8, 9);
            c.AddCharacterToField(Zed2, ZedCoor2);

            Character Zed3 = ZedFactory.GetInstance("Shank");
            Coordinate ZedCoor3 = new Coordinate(7, 7);
            c.AddCharacterToField(Zed3, ZedCoor3);

            Character Zed4 = ZedFactory.GetInstance("FastAttack");
            Coordinate ZedCoor4 = new Coordinate(7, 8);
            c.AddCharacterToField(Zed4, ZedCoor4);

            Character Zed5 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor5 = new Coordinate(7, 9);
            c.AddCharacterToField(Zed5, ZedCoor5);

            Character Zed6 = ZedFactory.GetInstance("Sloucher");
            Coordinate ZedCoor6 = new Coordinate(5, 5);
            c.AddCharacterToField(Zed6, ZedCoor6);

            PlayerControl.CharacterComboBox.ItemsSource = c.Players;
            PlayerControl.CharacterComboBox.SelectionChanged += PlayerComboBox_SelectionChanged;
            ZombieControl.CharacterComboBox.ItemsSource = c.Zeds;
            ZombieControl.CharacterComboBox.SelectionChanged += ZombieComboBox_SelectionChanged;



            Trap akbar = new Trap { Damage = "1d6", Description = "The destroyer of feet", Name = "Legos", StatusEffect = StatusEffect.Crippled };
            c.Field.GridSquares[1, 1].ActiveTrap = akbar;


            PlayerControl.CharacterType.Content = "Players";
            ZombieControl.CharacterType.Content = "Zombies";


            PopulateBoard();

        }

        private void ZombieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            this.SelectedZombie = (Zed)cb.SelectedItem;
            
        }

        private void PlayerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            this.SelectedPlayer = (Player)cb.SelectedItem;
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
                    nc.Resources.Add("Square", c.Field.GridSquares[i, j]);

                    Rectangle r = new Rectangle();
                    Rectangle r2 = new Rectangle();
                    Rectangle r3 = new Rectangle();

                    Binding b = new Binding("OccupyingCharacter");
                    b.Source = c.Field.GridSquares[i, j]; 
                    b.Converter = new CharacterToImageConverter();
                    
                    Binding b2 = new Binding("ItemList");
                    b2.Source = c.Field.GridSquares[i, j];
                    b2.Converter = new ItemToImageConverter();

                    Binding b3 = new Binding("IsOccupiable");
                    b3.Source = c.Field.GridSquares[i, j];
                    b3.Converter = new BoolToImageConverter();

                    r.SetBinding(Rectangle.FillProperty, b);
                    r.Height = 45;
                    r.Width = 45;

                    r2.SetBinding(Rectangle.FillProperty, b2);
                    r2.Height = 45;
                    r2.Width = 45;

                    r3.SetBinding(Rectangle.FillProperty, b3);
                    r3.Height = 50;
                    r3.Width = 50;

                    nc.Children.Add(r2);
                    nc.Children.Add(r);
                    nc.Children.Add(r3);

                    nc.Margin = new Thickness(1);
                    nc.Background = Brushes.Firebrick;

                    nc.MouseLeftButtonUp += nc_MouseLeftButtonUp;
                    nc.MouseRightButtonUp += nc_MouseRightButtonUp;

                    Board.Children.Add(nc);

                }

            }
        }

        private void Level_Up_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedPlayer.LevelUp();
        }

        private void Level_Down_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedPlayer.LevelDown();
        }

        private void Level_Up_Zombie_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedZombie.LevelUp();
        }

        private void Level_Down_Zombie_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedZombie.LevelDown();
        }

        void nc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Canvas tempc = (Canvas)sender;
            GridSquare tempgq = (GridSquare)tempc.Resources["Square"];

            if (tempgq.OccupyingCharacter is Zed)
                SelectedZombie = (Zed)tempgq.OccupyingCharacter;
            else if (tempgq.OccupyingCharacter is Player)
                SelectedPlayer = (Player)tempgq.OccupyingCharacter;
        }

        
        private void nc_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Canvas tempc = (Canvas)sender;
            GridSquare tempgq = (GridSquare)tempc.Resources["Square"];

            if (tempgq.OccupyingCharacter != null)
                return;

            tempgq.IsOccupiable = !tempgq.IsOccupiable;

            if (tempgq.IsOccupiable)
                tempc.Background = Brushes.Firebrick;
            else
                tempc.Background = Brushes.Black;
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
            c = new Controller();
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


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    c.Field.MoveCharacterToSquare(SelectedPlayer, new Coordinate(SelectedPlayer.Location.X - 1, SelectedPlayer.Location.Y));
                    break;
                case Key.A:
                    c.Field.MoveCharacterToSquare(SelectedPlayer, new Coordinate(SelectedPlayer.Location.X, SelectedPlayer.Location.Y - 1));
                    break;
                case Key.S:
                    c.Field.MoveCharacterToSquare(SelectedPlayer, new Coordinate(SelectedPlayer.Location.X + 1, SelectedPlayer.Location.Y));
                    break;
                case Key.D:
                   c.Field.MoveCharacterToSquare(SelectedPlayer, new Coordinate(SelectedPlayer.Location.X, SelectedPlayer.Location.Y + 1));
                    break;
                case Key.Up:
                    c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X - 1, SelectedZombie.Location.Y));
                    break;
                case Key.Left:
                     c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X, SelectedZombie.Location.Y - 1));
                    break;
                case Key.Down:
                    c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X + 1, SelectedZombie.Location.Y));
                    break;
                case Key.Right:
                    c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X, SelectedZombie.Location.Y + 1));
                    break;

            }
        }
        
    }
}
