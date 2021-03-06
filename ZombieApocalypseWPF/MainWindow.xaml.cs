﻿using System;
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

namespace ZombieApocalypseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public Controller c { get; set; }
		public Settings settings { get; set; }
        private Character LastCharacterSelected;
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
                this.LastCharacterSelected = SelectedPlayer;
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
                this.LastCharacterSelected = SelectedZombie;

            }
        }

        public MainWindow()
        {
            InitializeComponent();
            settings = new Settings();
            PlayerControl.Level_Up_Button.Click += Level_Up_Player_Button_Click;
            PlayerControl.Level_Down_Button.Click += Level_Down_Player_Button_Click;
            

            ZombieControl.Level_Up_Button.Click += Level_Up_Zombie_Button_Click;
            ZombieControl.Level_Down_Button.Click += Level_Down_Zombie_Button_Click;

            PlayerControl.AddItemButton.Click += Player_AddItemButton_Click;
            ZombieControl.AddItemButton.Click += Zombie_AddItemButton_Click;

            c = new Controller();

            Character NewPlayer = new Fighter();
            Coordinate Coor = new Coordinate(8, 2);
            c.AddCharacterToField(NewPlayer, Coor);
            NewPlayer.Speed = 3;
            SelectedPlayer = (Player)NewPlayer;


            Character NewPlayer2 = new Rifleman();
            Coordinate Coor2 = new Coordinate(3, 2);
            c.AddCharacterToField(NewPlayer2, Coor2);
            NewPlayer2.Speed = 3;

            Player NewPlayer1 = new Engineer();
            Coordinate Coor1 = new Coordinate(2, 2);
            NewPlayer1.Health -= 5;
            NewPlayer1.Speed = 7;
            c.AddCharacterToField(NewPlayer1, Coor1);

            c.Field.KillCharacter(NewPlayer1);

            Trader NewTrader = new Trader(200);
            Coordinate TraderCoor = new Coordinate(1, 1);
            c.AddCharacterToField(NewTrader, TraderCoor);

            Character Zed3 = ZedFactory.GetInstance("Shank");
            Coordinate ZedCoor3 = new Coordinate(5, 5);
            Zed3.Speed = 7;
            c.AddCharacterToField(Zed3, ZedCoor3);
            SelectedZombie = (Zed)Zed3;

            Weapon Gun = WeaponFactory.GetInstance("Trogdor Gun|Ranged|Shotgun|80|3d6|4");
            Coordinate GunCoor = new Coordinate(3, 3);
            c.AddItemToField(Gun, GunCoor);

            Weapon v = WeaponFactory.GetInstance("Consumate V|Melee|Slash|80|3d6|4");
            Coordinate vCoor = new Coordinate(3, 3);
            c.AddItemToField(v, vCoor);

            c.AddItemToField(WeaponFactory.RandomWeapon(), GunCoor);

            //Trap akbar = new Trap { Damage = new DieRoll(1, 2, 3, 4), Description = "It's a Trap", Name = "Legos", StatusEffect = StatusEffect.Crippled };
            //c.AddTrapToField(akbar, new Coordinate(5, 4));

            CharacterComboBox.ItemsSource = c.Players;
            CharacterComboBox.SelectionChanged += PlayerComboBox_SelectionChanged;

            ZCharacterComboBox.ItemsSource = c.Zeds;
            ZCharacterComboBox.SelectionChanged += ZombieComboBox_SelectionChanged;

            
            PopulateBoard();

            c.AI.IntelligentAI = false;

            c.DetermineTurnOrder();
            c.NextTurn();
            PaintMoves();

            settings.CanEdit = false;
            settings.ShowBattleScene = false;
            settings.EnforceTurnOrder = true;
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
                    Binding ItemBind = new Binding();
                    ItemBind.Source = c.Field.GetGridSquareAt(new Coordinate(i, j)).ItemList;
                    ItemBind.Converter = new ItemToImageConverter();
                    ItemRec.SetBinding(Rectangle.FillProperty, ItemBind); //Issues here
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

                    //ContextMenu cm = new ContextMenu();
                    //cm.ItemsSource = new List<Button>
                    //{
                    //    new Button { Content = "Trade", Click += ShowTradeMenu  }
                    //};

                    Binding OccupyBind = new Binding("IsOccupiable");
                    OccupyBind.Source = c.Field.GridSquares[i, j];
                    OccupyBind.Converter = new BoolToImageConverter();

                    nc.Margin = new Thickness(1);
                    nc.SetBinding(Canvas.BackgroundProperty, OccupyBind);

                    nc.MouseLeftButtonUp += nc_MouseLeftButtonUp;
                    //nc.MouseRightButtonUp += nc_MouseRightButtonUp;

                    Board.Children.Add(nc);
                }
            }
        }

        //private void ShowTradeMenu(object sender, RoutedEventArgs e)
        //{

        //}

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

            if (settings.CanEdit)
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

            if (settings.CanEdit)
            {
                Item i = AddItem();

                if (i != null && SelectedZombie != null)
                    SelectedZombie.Items.Add(i);
            }
        }

        private void Level_Up_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPlayer != null && settings.CanEdit)
            {
                    SelectedPlayer.LevelUp();
            }
        }
       
        private void Level_Down_Player_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPlayer != null && settings.CanEdit)
                SelectedPlayer.LevelDown();
        }

        private void Level_Up_Zombie_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedZombie != null && settings.CanEdit)
                SelectedZombie.LevelUp();
        }

        private void Level_Down_Zombie_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedZombie != null && settings.CanEdit)
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
            if (tempgq.OccupyingCharacter == null || LastCharacterSelected == null)
                return;

            //Actions if the game is in normal mode and the currently selected player is the current player
            if(!settings.CanEdit && LastCharacterSelected == c.CurrentPlayer && LastCharacterSelected.MSquares >= 3)
            {
                if(LastCharacterSelected is Player && LastCharacterSelected == c.CurrentPlayer && tempgq.OccupyingCharacter is Zed)
                {
                    if ((c.Field.AdjacentCharacters(LastCharacterSelected, false).Contains(tempgq.OccupyingCharacter)) && ((Player)LastCharacterSelected).EquippedWeapon is MeleeWeapon)
                        c.MeleeAttack(tempgq.OccupyingCharacter);
                    else if (((Player)LastCharacterSelected).EquippedWeapon is RangedWeapon)
                        c.RangedAttack(ActionTypes.AimedRangedAttack, tempgq.OccupyingCharacter);

                }
                else if (LastCharacterSelected == c.CurrentPlayer && tempgq.IsOccupiable == true && tempgq.OccupyingCharacter == null)
                {
                    int MoveCost = c.Field.ShortestPathCost(LastCharacterSelected, tempgq.Coordinate);
                    if (LastCharacterSelected.MSquares >= MoveCost)
                    {
                        LastCharacterSelected.MSquares -= MoveCost;
                        c.Field.MoveCharacterToSquare(LastCharacterSelected, tempgq.Coordinate);
                    }
                }
                else if (LastCharacterSelected is Zed && tempgq.OccupyingCharacter is Player && LastCharacterSelected.Equals(c.CurrentPlayer) && (c.Field.AdjacentCharacters(LastCharacterSelected, false).Contains(tempgq.OccupyingCharacter)))
                {
                    c.MeleeAttack(tempgq.OccupyingCharacter);
                    
                }
                return;
            }

            // Determines actions when settings is enforcing turn order
            if (settings.EnforceTurnOrder)
            {
                if (tempgq.OccupyingCharacter is Zed)
                    SelectedZombie = (Zed)tempgq.OccupyingCharacter;

                else if (tempgq.OccupyingCharacter is Player)
                    SelectedPlayer = (Player)tempgq.OccupyingCharacter;
                if(SelectedZombie != null
                    && SelectedZombie.Equals(LastCharacterSelected))
                {
                    LastCharacterSelected = SelectedZombie;
                }
                else if(SelectedPlayer != null
                    && SelectedPlayer.Equals(LastCharacterSelected))
                {
                    //Moves the Character if they are allowed to move based on the current settings
                    if (settings.CanEdit)
                        c.Field.MoveCharacterToSquare(LastCharacterSelected, tempgq.Coordinate);
                    else if (LastCharacterSelected == c.CurrentPlayer && LastCharacterSelected.MSquares >= c.Field.ShortestPathCost(LastCharacterSelected, tempgq.Coordinate))
                    {
                        c.CurrentPlayer.MSquares -= c.Field.ShortestPathCost(LastCharacterSelected, tempgq.Coordinate);
                        MoveCharacter(tempgq.Coordinate);
                    }
                }
                else
                {
                    if(LastCharacterSelected == c.CurrentPlayer)
                    {
						c.CurrentPlayer.MSquares -= c.Field.ShortestPathCost(LastCharacterSelected, tempgq.Coordinate);
                        c.Field.MoveCharacterToSquare(LastCharacterSelected, tempgq.Coordinate);
                        MoveCharacter(tempgq.Coordinate);
                    }
                }
            }
                //Moves selected Characters or selects a new character
            else
            {
                if (tempgq.OccupyingCharacter is Zed)
                    SelectedZombie = (Zed)tempgq.OccupyingCharacter;

                else if (tempgq.OccupyingCharacter is Player)
                    SelectedPlayer = (Player)tempgq.OccupyingCharacter;


                if (tempgq.OccupyingCharacter is Zed)
                    SelectedZombie = (Zed)tempgq.OccupyingCharacter;


                else if (tempgq.OccupyingCharacter == null)
                    if (LastCharacterSelected != null)
                        MoveCharacter(tempgq.Coordinate);

                    else if (tempgq.OccupyingCharacter == null)
                        if (LastCharacterSelected != null)
                            MoveCharacter(tempgq.Coordinate);

            }
            PaintMoves();
        }

        /// <summary>
        /// Highlights parts of the board that can be moved to by the currently selected character
        /// </summary>
        public void PaintMoves()
        {
            BoardOverlay.Children.Clear();
            List<Coordinate> possMoves = c.Field.PossibleMovesForCharacter(LastCharacterSelected);
            List<Character> possAttacks = new List<Character>();
            if (LastCharacterSelected is Player)
                possAttacks = c.Field.PossibleRangedTargets(LastCharacterSelected, c.Zeds);

            List<Coordinate> possAttackCoors = new List<Coordinate>();

            foreach (Character ch in possAttacks)
                possAttackCoors.Add(ch.Location);

            for (int i = 0; i < c.Field.Height; i++)
            {
                for (int j = 0; j < c.Field.Width; j++)
                {
                    Coordinate currCoor = new Coordinate(i, j);
                    Canvas reccy = new Canvas();
                    reccy.MouseLeftButtonUp += nc_MouseLeftButtonUp;
                    reccy.MouseRightButtonUp += nc_MouseRightButtonUp;

                    if (possAttackCoors.Contains(currCoor, new LocationComparer()))
                        reccy.Background = new SolidColorBrush(new Color { A = 100, R = 255, B = 50, G = 50 });
                    else if (possMoves.Contains(currCoor, new LocationComparer()))
                        reccy.Background = new SolidColorBrush(new Color { A = 100, R = 0, B = 50, G = 250 });

                    reccy.Resources.Add("Square", c.Field.GetGridSquareAt(currCoor));

                    BoardOverlay.Children.Add(reccy);
                }
            }
        }

        private void nc_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (settings.CanEdit)
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
            Coordinate MoveTo = null;
            if (LastCharacterSelected == null)
                return;
            
            switch (e.Key)
            {
                case Key.W:
                    if ( (c.Field.GridSquares[LastCharacterSelected.Location.X - 1, LastCharacterSelected.Location.Y].OccupyingCharacter is Zed && LastCharacterSelected is Player)
                        || (c.Field.GridSquares[LastCharacterSelected.Location.X - 1, LastCharacterSelected.Location.Y].OccupyingCharacter is Player && LastCharacterSelected is Zed))
                    {
                        if (LastCharacterSelected is Zed)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X - 1, LastCharacterSelected.Location.Y].OccupyingCharacter);
                        else if (((Player)LastCharacterSelected).EquippedWeapon is MeleeWeapon)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X - 1, LastCharacterSelected.Location.Y].OccupyingCharacter);
                        else
                            c.RangedAttack(ActionTypes.AimedRangedAttack, c.Field.GridSquares[LastCharacterSelected.Location.X - 1, LastCharacterSelected.Location.Y].OccupyingCharacter);
                    }
                    else
                        MoveTo = new Coordinate(LastCharacterSelected.Location.X - 1, LastCharacterSelected.Location.Y);
                    break;
                case Key.A:

                    if ((c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y - 1].OccupyingCharacter is Zed && LastCharacterSelected is Player)
                        || (c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y - 1].OccupyingCharacter is Player && LastCharacterSelected is Zed))
                    {
                        if (LastCharacterSelected is Zed)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y - 1].OccupyingCharacter);
                        else if (((Player)LastCharacterSelected).EquippedWeapon is MeleeWeapon)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y - 1].OccupyingCharacter);
                        else
                            c.RangedAttack(ActionTypes.AimedRangedAttack, c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y - 1].OccupyingCharacter);
                    }
                    else
                        MoveTo = new Coordinate(LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y - 1);

                    break;
                case Key.S:
                    
                    if ((c.Field.GridSquares[LastCharacterSelected.Location.X + 1, LastCharacterSelected.Location.Y].OccupyingCharacter is Zed && LastCharacterSelected is Player)
                        || (c.Field.GridSquares[LastCharacterSelected.Location.X + 1, LastCharacterSelected.Location.Y].OccupyingCharacter is Player && LastCharacterSelected is Zed))
                    {
                        if (LastCharacterSelected is Zed)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X + 1, LastCharacterSelected.Location.Y].OccupyingCharacter);
                        else if (((Player)LastCharacterSelected).EquippedWeapon is MeleeWeapon)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X + 1, LastCharacterSelected.Location.Y].OccupyingCharacter);
                        else
                            c.RangedAttack(ActionTypes.AimedRangedAttack, c.Field.GridSquares[LastCharacterSelected.Location.X + 1, LastCharacterSelected.Location.Y].OccupyingCharacter);
                    }
                    else
                        MoveTo = new Coordinate(LastCharacterSelected.Location.X + 1, LastCharacterSelected.Location.Y);

                    break;
                case Key.D:

                    if ((c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y + 1].OccupyingCharacter is Zed && LastCharacterSelected is Player)
                        || (c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y + 1].OccupyingCharacter is Player && LastCharacterSelected is Zed))
                    {
                        if (LastCharacterSelected is Zed)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y + 1].OccupyingCharacter);
                        else if (((Player)LastCharacterSelected).EquippedWeapon is MeleeWeapon)
                            c.MeleeAttack(c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y + 1].OccupyingCharacter);
                        else
                            c.RangedAttack(ActionTypes.AimedRangedAttack, c.Field.GridSquares[LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y + 1].OccupyingCharacter);
                    }
                    else
                        MoveTo = new Coordinate(LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y + 1);

                    MoveTo = new Coordinate(LastCharacterSelected.Location.X, LastCharacterSelected.Location.Y + 1);
                    break;

                case Key.Enter:
                    EndTurn_Click(this, null);
                    break;
            }
            if (MoveTo != null)
            {
                if (settings.EnforceTurnOrder && LastCharacterSelected == c.CurrentPlayer)
                {
                    GridSquare Target = c.Field.GetGridSquareAt(MoveTo);
                    if (LastCharacterSelected is Player && Target != null &&  Target.OccupyingCharacter is Trader)
                    {
                        TradeWindow Trade = new TradeWindow((Player)LastCharacterSelected, (Trader)c.Field.GetGridSquareAt(MoveTo).OccupyingCharacter);
                        Trade.ShowDialog();
                    }
                    else
                    {
                        int MoveCost = c.Field.ShortestPathCost(LastCharacterSelected, MoveTo);
                        if (c.CurrentPlayer.MSquares - MoveCost >= 0)
                        {

                            c.CurrentPlayer.MSquares -= MoveCost;
                            MoveCharacter(MoveTo);

                            c.Field.MoveCharacterToSquare(LastCharacterSelected, MoveTo);
                            MoveCharacter(MoveTo);
                        }
                    }
                }
                else if (settings.CanEdit && !settings.EnforceTurnOrder)
                {
                    MoveCharacter(MoveTo);
                }
            }
            PaintMoves();
        }
        
        /// <summary>
        /// Either moves the latest character or commences battle
        /// </summary>
        /// <param name="Destination"></param>
        public void MoveCharacter(Coordinate Destination)
        {

            if (Destination != null && Destination.X >= 0 && Destination.Y >= 0 && Destination.X < c.Field.Width && Destination.Y < c.Field.Height )
            {
                Character Victim = c.Field.GetGridSquareAt(Destination).OccupyingCharacter;

                if (Victim != null)
                {
                    if (settings.ShowBattleScene)
                    {

                    }
                    else
                    {
                        if (LastCharacterSelected.Equals(SelectedPlayer))
                        {
                            if (SelectedPlayer.EquippedWeapon is RangedWeapon)
                            {

                            }
                            else
                            {
                                c.MeleeAttack(Victim);
                            }
                        }
                    }
                }
                else
                {
                    c.Field.MoveCharacterToSquare(LastCharacterSelected, Destination);
                }

                c.Field.MoveCharacterToSquare(LastCharacterSelected, Destination);
                PaintMoves();

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
            MessageBoxResult Response = MessageBox.Show("Would you like to save the current game?", "Save Game", MessageBoxButton.YesNoCancel);
            if(Response.Equals(MessageBoxResult.Yes))
            {
                SavePrompt();
            }
            if(!Response.Equals(MessageBoxResult.Cancel))
            {
                c = new Controller();
            }
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
                    c.Field.ClearBoard();
                    this.Board.Children.Clear();
                    c.Load(OpenFile.FileName, input);
                    PopulateBoard();
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
            MessageBoxResult Response = MessageBox.Show("Would you like to save the current game?", "Save Game", MessageBoxButton.YesNoCancel);
            if(Response.Equals(MessageBoxResult.Yes))
            {
                SavePrompt();
            }
            if(!Response.Equals(MessageBoxResult.Cancel))
            {
                this.Close();
            }
        }

        /// <summary>
        /// Prompts the user to save their current information
        /// </summary>
        private void SavePrompt()
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Saved ZombiApoc Games|*.zombieapoc";

            if (SaveFile.ShowDialog() == true)
            {
                Stream output = File.Create(SaveFile.FileName);

                if (output != null)
                {
                    c.Save(SaveFile.FileName, output);
                    output.Flush();
                    output.Close();
                }
                
            }
        }

        private void Player_Add(object sender, RoutedEventArgs e)
        {
            if (settings.CanEdit)
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
                if (c.PlayerOrder != null)
                    c.PlayerOrder.Push(NewCharacter);

                NewCharacter = null;

                xCoor = -9;
                yCoor = -9;
            }
        }

        private void Zombie_Add(object sender, RoutedEventArgs e)
        {
            if (settings.CanEdit)
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
                if (c.ZedOrder != null)
                    c.ZedOrder.Push(NewCharacter);
                NewCharacter = null;

                xCoor = -9;
                yCoor = -9;
            }
        }

        private void Item_Add(object sender, RoutedEventArgs e)
        {
            if (settings.CanEdit)
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

        private void EndTurn_Click(object sender, RoutedEventArgs e)
        {
            if (settings.EnforceTurnOrder)
            {
                c.NextTurn();
                Console.WriteLine(c.CurrentPlayer);
                //SelectedPlayer = null;
                //SelectedZombie = null;
                LastCharacterSelected = c.CurrentPlayer;
                PaintMoves();
            }
        }

        public void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.SettingsControl.w = this;
			sw.SettingsControl.init();
            sw.Show();

        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SavePrompt();
        }
    }
}
