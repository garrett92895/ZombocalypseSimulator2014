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
using ZombieApocalypseSimulator;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseWPF.Converters;
using ZombieApocalypseSimulator.Models.Enums;
using ZombieApocalypseWPF.Windows;
using ZombieApocalypseSimulator.Modes.HordeMode;

namespace ZombieApocalypseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Controller c;
        public bool canEdit;
        public Horde hordeMode;
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
                this.CharacterComboBox.SelectedValue = _selectedPlayer;
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
                this.ZCharacterComboBox.SelectedValue = _selectedZombie;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            canEdit = true;
            PlayerControl.Level_Up_Button.Click += Level_Up_Player_Button_Click;
            PlayerControl.Level_Down_Button.Click += Level_Down_Player_Button_Click;
            

            ZombieControl.Level_Up_Button.Click += Level_Up_Zombie_Button_Click;
            ZombieControl.Level_Down_Button.Click += Level_Down_Zombie_Button_Click;

            PlayerControl.AddItemButton.Click += Player_AddItemButton_Click;
            ZombieControl.AddItemButton.Click += Zombie_AddItemButton_Click;

            c = new Controller();

            Character NewPlayer = new Fighter();
            Coordinate Coor = new Coordinate(2, 3);
            c.AddCharacterToField(NewPlayer, Coor);
            SelectedPlayer = (Player)NewPlayer;

            Character NewPlayer1 = new Engineer();
            Coordinate Coor1 = new Coordinate(3, 3);
            c.AddCharacterToField(NewPlayer1, Coor1);

            Character Trader = new Trader();
            Coordinate Coor2 = new Coordinate(1, 1);
            c.AddCharacterToField(Trader, Coor2);

            //c.Field.Height = 30;
            //c.Field.Width = 30;

            for (int i = 0; i < 10; i++)
            {
                Trader.Items.Add(WeaponFactory.RandomWeapon());
            }

            //Character Zed3 = ZedFactory.GetInstance("Shank");
            //Coordinate ZedCoor3 = new Coordinate(5, 5);
            //c.AddCharacterToField(Zed3, ZedCoor3);
            //SelectedZombie = (Zed)Zed3;

            //Weapon Gun = WeaponFactory.GetInstance("Winchester|Ranged|Shotgun|80|3d6|4");
            //Coordinate GunCoor = new Coordinate(3, 3);
            //c.AddItemToField(Gun, GunCoor);

            //Trap akbar = new Trap { Damage = "1d2", Description = "It's a Trap", Name = "Legos", StatusEffect = StatusEffect.Crippled };
            //c.AddTrapToField(akbar, new Coordinate(5, 4));

            CharacterComboBox.ItemsSource = c.Players;
            CharacterComboBox.SelectionChanged += PlayerComboBox_SelectionChanged;

            ZCharacterComboBox.ItemsSource = c.Zeds;
            ZCharacterComboBox.SelectionChanged += ZombieComboBox_SelectionChanged;

            
            PopulateBoard();

            MessageBoxResult dr = MessageBox.Show("Would you like to enable intelligent zombies?",
                      "Zombie Mode", MessageBoxButton.YesNo);
            if(dr.Equals(MessageBoxResult.Yes))
            {
                c.AI.IntelligentAI = true;
            }
            else
            {
                c.AI.IntelligentAI = false;
            }
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
                    nc.Resources.Add("Square", c.Field.GridSquares[i, j]);



                    Binding HeightBind = new Binding("ActualHeight");
                    HeightBind.Source = nc;

                    Binding WidthBind = new Binding("ActualWidth");
                    WidthBind.Source = nc;

                    Rectangle CharRec = new Rectangle();
                    Binding CharBind = new Binding("OccupyingCharacter");
                    CharBind.Source = c.Field.GridSquares[i, j];
                    CharBind.Converter = new CharacterToImageConverter();
                    CharRec.SetBinding(Rectangle.FillProperty, CharBind);
                    CharRec.SetBinding(Rectangle.HeightProperty, HeightBind);
                    CharRec.SetBinding(Rectangle.WidthProperty, WidthBind);


                    Rectangle ItemRec = new Rectangle();
                    Binding ItemBind = new Binding("ItemList");
                    ItemBind.Source = c.Field.GridSquares[i, j];
                    ItemBind.Converter = new ItemToImageConverter();
                    ItemRec.SetBinding(Rectangle.FillProperty, ItemBind);
                    ItemRec.SetBinding(Rectangle.HeightProperty, HeightBind);
                    ItemRec.SetBinding(Rectangle.WidthProperty, WidthBind);

                    Rectangle TrapRec = new Rectangle();
                    Binding TrapBind = new Binding("ActiveTrap");
                    TrapBind.Source = c.Field.GridSquares[i, j];
                    TrapBind.Converter = new ItemToImageConverter();
                    TrapRec.SetBinding(Rectangle.FillProperty, TrapBind);
                    TrapRec.SetBinding(Rectangle.HeightProperty, HeightBind);
                    TrapRec.SetBinding(Rectangle.WidthProperty, WidthBind);



                    nc.Children.Add(TrapRec);
                    nc.Children.Add(ItemRec);
                    nc.Children.Add(CharRec);

                    ContextMenu cm = new ContextMenu();

                    Binding OccupyBind = new Binding("IsOccupiable");
                    OccupyBind.Source = c.Field.GridSquares[i, j];
                    OccupyBind.Converter = new BoolToImageConverter();

                    nc.Margin = new Thickness(1);
                    nc.SetBinding(Canvas.BackgroundProperty, OccupyBind);

                    nc.MouseLeftButtonUp += nc_MouseLeftButtonUp;
                    nc.MouseRightButtonUp += nc_MouseRightButtonUp;

                    Board.Children.Add(nc);
                }
            }
        }

        public static Item newItem = null;
        public static int xCoor;
        public static int yCoor;
        public static Character NewCharacter;

        private Item AddItem()
        {
            Window aiw = new AddItemWindow();
            aiw.ShowDialog();
            Item i = newItem;
            newItem = null;
            return i;
        }

        private void Player_AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPlayer == null)
                return;

            if (canEdit)
            {
                Item i = AddItem();

                if (i != null && SelectedPlayer != null)
                    SelectedPlayer.Items.Add(i);
            }
        }

        private void Zombie_AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedZombie == null)
                return;

            if (canEdit)
            {
                Item i = AddItem();

                if (i != null && SelectedZombie != null)
                    SelectedZombie.Items.Add(i);
            }
        }

        private void Level_Up_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPlayer != null)
                SelectedPlayer.LevelUp();
        }
       
        private void Level_Down_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPlayer != null)
                SelectedPlayer.LevelDown();
        }

        private void Level_Up_Zombie_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedZombie != null)
                SelectedZombie.LevelUp();
        }

        private void Level_Down_Zombie_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedZombie != null)
                SelectedZombie.LevelDown();
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

        private void nc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
            if (canEdit)
            {
                Canvas tempc = (Canvas)sender;
                GridSquare tempgq = (GridSquare)tempc.Resources["Square"];

                if (tempgq.OccupyingCharacter != null)
                    return;

                tempgq.IsOccupiable = !tempgq.IsOccupiable;
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
                case Key.I:
                    c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X - 1, SelectedZombie.Location.Y));
                    break;
                case Key.J:
                    c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X, SelectedZombie.Location.Y - 1));
                    break;
                case Key.K:
                    c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X + 1, SelectedZombie.Location.Y));
                    break;
                case Key.L:
                    c.Field.MoveCharacterToSquare(SelectedZombie, new Coordinate(SelectedZombie.Location.X, SelectedZombie.Location.Y + 1));
                    break;

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
            MessageBox.Show("Would you like to save the current game?");
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

        private void Player_Add(object sender, RoutedEventArgs e)
        {
            if (canEdit)
            {
                xCoor = -9;
                yCoor = -9;
                NewCoorWindow ncw = new NewCoorWindow(c.Field.Width, c.Field.Height);
                ncw.ShowDialog();

                if (xCoor == -9 || yCoor == -9)
                    return;

                AddPlayerWindow apw = new AddPlayerWindow();
                apw.ShowDialog();

                if (NewCharacter == null)
                    return;

                c.AddCharacterToField(NewCharacter, new Coordinate(xCoor, yCoor));

                NewCharacter = null;

                xCoor = -9;
                yCoor = -9;
            }
        }

        private void Zombie_Add(object sender, RoutedEventArgs e)
        {
            if (canEdit)
            {
                xCoor = -9;
                yCoor = -9;
                NewCoorWindow ncw = new NewCoorWindow(c.Field.Width, c.Field.Height);
                ncw.ShowDialog();

                if (xCoor == -9 || yCoor == -9)
                    return;

                AddZombieWindow apw = new AddZombieWindow();
                apw.ShowDialog();

                if (NewCharacter == null)
                    return;

                c.AddCharacterToField(NewCharacter, new Coordinate(xCoor, yCoor));

                NewCharacter = null;

                xCoor = -9;
                yCoor = -9;
            }
        }

        private void Item_Add(object sender, RoutedEventArgs e)
        {
            if (canEdit)
            {
                xCoor = -9;
                yCoor = -9;
                NewCoorWindow ncw = new NewCoorWindow(c.Field.Width, c.Field.Height);
                ncw.ShowDialog();

                if (xCoor == -9 || yCoor == -9)
                    return;

                Item i = AddItem();


                c.AddItemToField(i, new Coordinate(xCoor, yCoor));

                xCoor = -9;
                yCoor = -9;
            }
        }

        private void EditMode_Click(object sender, RoutedEventArgs e)
        {
            this.canEdit = !this.canEdit;
        }


        
    }
}
