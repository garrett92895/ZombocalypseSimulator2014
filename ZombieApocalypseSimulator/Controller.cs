﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models;
using ZombieApocalypseSimulator.Models.Characters;
using CSC160_ConsoleMenu;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Enums;
using ZombieApocalypseSimulator.Models.Items.Enums;
using ZombieApocalypseSimulator.Modes.HordeMode;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace ZombieApocalypseSimulator
{
    public enum ActionTypes
    {
        CharacterScreen,
        ZedScreen,
        EndTurn,
        Move,
        Equip,
        MeleeAttack,
        AimedRangedAttack,
        UnaimedRangedAttack,
        PickUpItem,
        DropItem,
        GiveItem,
        Trade,
        Reload,
        LevelUp,
        LevelDown,
        Heal,
        Revive,
        Shout,
        MakeTrap,
        FixWeapon,
        SetTrap
    }
    public class Controller
    {

        #region Props and Backing Fields
        public GameArea Field { get; set; }
        public Character CurrentPlayer;
        private int MaxSquares;
        public int SquaresLeft { get; set; }
        public ZombieAI AI { get; set; }
        public List<Character> Zeds;
        public List<Character> Players;
        public List<Coordinate> CorpseSquares;
        public Horde HordeMode { get; set; }
        public CharacterStack PlayerOrder { get; set; }
        public CharacterStack ZedOrder { get; set; }
        public List<Coordinate> TrapLocations; 
        #endregion

        #region Ctor and Run
        public Controller(int Width = 15, int Height = 15)
        {
            Field = new GameArea(Width, Height);
            Zeds = new List<Character>();
            Players = new List<Character>();
            CorpseSquares = new List<Coordinate>();
            TrapLocations = new List<Coordinate>();
            AI = new ZombieAI(true);
            HordeMode = new Horde(Width, Height);


        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Start of Turn Order");
                PlayerOrder = DetermineTurnOrder(Players);
                ZedOrder = DetermineTurnOrder(Zeds);
                //if (Players.Count() == 0)
                //{
                //    Console.WriteLine("Game over, zombies have taken over the world.");
                //    Environment.Exit(0);
                //}
                for (int i = 0; i < Players.Count(); i++)
                {
                    CurrentPlayer = PlayerOrder.Pop();

                    PlayNextTurn();
                }

                //if (Zeds.Count == 0)
                //{
                //    Console.WriteLine("Congratulations! The Zambies have been eliminated!\nYou live to fight another day!");
                //    Environment.Exit(0);
                //}

                for (int i = 0; i < Zeds.Count(); i++)
                {
                    Zed CurrentZombie = (Zed)ZedOrder.Pop();
                    CurrentZombie.HasAttacked = false;
                    CurrentPlayer = CurrentZombie;
                    PlayNextTurnAI();
                }
                Zeds.AddRange(Field.MakeReviveRolls(CorpseSquares));
                if (HordeMode.IsActive)
                {
                    foreach (Character C in HordeMode.NextSpawns())
                    {
                        List<Coordinate> ViableSquares = new List<Coordinate>();
                        foreach(SpawnZoneMarker SZM in HordeMode.SpawnMarkers)
                        {
                            ViableSquares.Add(Field.GetViableSquare(SZM.TopLeft, SZM.BottomRight));
                        }
                        AddCharacterToField(C, ViableSquares.ElementAt(DieRoll.RollOne(ViableSquares.Count)-1));
                    }
                }
            }
        }
        #endregion

        #region Adding Characters and Items

        /// <summary>
        /// Adds the given Character to the given Coordinate on the GameArea if the given Coordinate is Occupieable and empty.  
        /// If the given Coordiante is Closed or Occupied then will not add the given Character to the GameArea
        /// If no Coordiante is given will place the Character onto a random viable GridSquare in the GameArea
        /// </summary>
        /// <param name="C"></param>
        /// <param name="Location"></param>
        public void AddCharacterToField(Character C, Coordinate Location = null)
        {
            if (Location == null)
            {
                Location = Field.GetViableSquare();
            }
            Field.AddCharacterToSquare(C, Location);

            if (C.Location != null)
            {
                if (C is Player)
                {
                    Players.Add(C);
                }
                else
                {
                    Zeds.Add(C);
                }
            }
        }

        /// <summary>
        /// Adds the given Item to the given Coordinate on the GameArea if the given Coordinate is Occupieable and empty.  
        /// If the given Coordiante is Closed or Occupied then will not add the given Item to the GameArea
        /// If no Coordiante is given will place the Item onto a random viable GridSquare in the GameArea
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Location"></param>
        public void AddItemToField(Item I, Coordinate Location = null)
        {
            if (Location == null)
            {
                Location = Field.GetViableSquare();
            }
            Field.GetItemsInSquare(Location).Add(I);
        }

        // <summary>
        /// Adds the given Trap to the given Coordinate on the GameArea if the given Coordinate is Occupieable and empty.  
        /// If the given Coordiante is Closed or Occupied then will not add the given Item to the GameArea
        /// If no Coordiante is given will place the Item onto a random viable GridSquare in the GameArea
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Location"></param>
        public void AddTrapToField(Trap T, Coordinate Location = null)
        {
            if (Location == null)
            {
                Location = Field.
                    GetViableSquare();
            }
            Field.GridSquares[Location.X, Location.Y].ActiveTrap = T;
        }

        #endregion

        #region Turn Sorting

        /// <summary>
        /// Sets the PlayerOrders and ZedOrders to be new CharacterStacks for a new turn
        /// </summary>
        public void DetermineTurnOrder()
        {
            PlayerOrder = DetermineTurnOrder(Players);
            ZedOrder = DetermineTurnOrder(Zeds);
        }
        /// <summary>
        /// Returns a CharacterStack with all of the Character's in the given list ordered according to initiative rolls
        /// </summary>
        /// <param name="_characters"></param>
        /// <returns></returns>
        private CharacterStack DetermineTurnOrder(List<Character> _characters)
        {
            CharacterStack _turnOrder = new CharacterStack(_characters.Count);
            //int[] rolls = new int[_characters.Count];
            for (int i = 0; i < _characters.Count; i++)
            {
                _characters[i].Initiative = new DieRoll(1, 20).Roll();
            }
            //_characters.Sort(rolls);
            List<Character> SortedCharacters = _characters.OrderByDescending(c => c.Initiative).ToList();
            for (int i = 0; i < SortedCharacters.Count; i++)
            {
                Character c = SortedCharacters.ElementAt(i);

                c.CanParry = true;
                c.CanDodge = true;

                _turnOrder.Push(c);
            }
            return _turnOrder;
        }

        private int minValue(int[] values, int _ignoreIndex = -1)
        {
            int maxIndex = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > values[maxIndex] && i != _ignoreIndex)
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
        #endregion

        #region Turn Cycle
        private void PlayNextTurn()
        {
            //CurrentPlayer = GetNextCharacter();

            SquaresLeft = CurrentPlayer.squares();
            MaxSquares = SquaresLeft;
            Console.WriteLine(Field.ToString());

            while (SquaresLeft > 0)
            {
                Console.WriteLine("\r\nSquares Left: " + SquaresLeft);
                Console.WriteLine(CurrentPlayer.GetType().ToString());

                ActionTypes PlayerAction = GetActionFromPlayer(SquaresLeft);

                //Take Selected Action
                if (PlayerAction.Equals(ActionTypes.Move))
                {
                    Console.WriteLine("Move");
                    SquaresLeft = SquaresLeft - Move();
                    Console.WriteLine(Field.ToString());
                    Console.WriteLine();
                    for (int setOffTrap = 0; setOffTrap < TrapLocations.Count; setOffTrap++ )
                        if (CurrentPlayer.Location == TrapLocations[setOffTrap])
                        {
                            //int roll = new DieRoll(1, 6).Roll();
                            //CurrentPlayer.Equals(StatusEffect.Stunned);
                            //CurrentPlayer.Health -= roll;
                            //Console.WriteLine("You steped on a trap you now are Stunned and you took " + roll + " of damage");
                        }
                }
                else if (PlayerAction.Equals(ActionTypes.EndTurn))
                {
                    Console.WriteLine("Ended turn");
                    SquaresLeft -= MaxSquares;

                    //Status Effect Calculation


                    if (CurrentPlayer.Equals(StatusEffect.OnFire))
                    {
                        CurrentPlayer.Health -= new DieRoll(1, 4).Roll();
                    }
                    if (CurrentPlayer.Equals(StatusEffect.Crippled))
                    {
                        SquaresLeft = MaxSquares / 2;
                    }
                    if (CurrentPlayer.Equals(StatusEffect.Stunned))
                    {
                        SquaresLeft = 0;
                    }
                    if (CurrentPlayer.Equals(StatusEffect.Infected))
                    {
                        int check = new DieRoll(1, 20).Roll();
                        if (check <= 5)
                        {
                            Field.KillCharacter(CurrentPlayer);
                            PlayerOrder.RemoveCharacter(CurrentPlayer);
                            Field.AddCharacterToSquare(ZedFactory.RandomSpecial(), CurrentPlayer.Location);
                        }
                    }
                    if (CurrentPlayer.Equals(StatusEffect.ArmourBroken))
                    {
                        CurrentPlayer.SDC = CurrentPlayer.MaxSDC - CurrentPlayer.MaxSDC;
                    }
                }
                else if (PlayerAction.Equals(ActionTypes.Equip))
                {
                    Console.WriteLine("Equip");
                    Equip();
                }
                else if (PlayerAction.Equals(ActionTypes.SetTrap))
                {
                    Console.WriteLine("Place a trap");
                    TrapLocations.Add(CurrentPlayer.Location);

                    //AddTrapToField(CurrentPlayer.Items.Equals(Trap), TrapLocation);
                }
                else if (PlayerAction.Equals(ActionTypes.Reload))
                {
                    Console.WriteLine("Reload");
                    Reload();
                }
                else if (PlayerAction.Equals(ActionTypes.DropItem))
                {
                    Console.WriteLine("Drop item");
                    DropItem();
                    SquaresLeft--;
                }
                else if (PlayerAction.Equals(ActionTypes.GiveItem))
                {
                    Console.WriteLine("Give item");
                    GiveItem();
                    SquaresLeft -= 3;
                }

                else if (PlayerAction.Equals(ActionTypes.Heal))
                {
                    Console.WriteLine("Heal");

                    SquaresLeft -= 3;
                }
                else if (PlayerAction.Equals(ActionTypes.Revive))
                {
                    Console.WriteLine("Revive");
                    Revive();
                    SquaresLeft -= MaxSquares / 2;
                }
                else if (PlayerAction.Equals(ActionTypes.Shout))
                {
                    Console.WriteLine("Shout");

                    SquaresLeft -= 1;
                }
                else if (PlayerAction.Equals(ActionTypes.MakeTrap))
                {
                    Console.WriteLine("Make Trap");
                    //CurrentPlayer.addTrap();
                    SquaresLeft -= 3;
                }
                else if (PlayerAction.Equals(ActionTypes.FixWeapon))
                {
                    Console.WriteLine("Fix Your Equiped Weapon");

                    SquaresLeft -= 4;
                }
                else if (PlayerAction.Equals(ActionTypes.PickUpItem))
                {
                    Console.WriteLine("Pick up item");
                    PickUpItem();
                    SquaresLeft -= 2;
                }
                else if (PlayerAction.Equals(ActionTypes.LevelUp))
                {
                    Console.WriteLine("Level Up");
                    CurrentPlayer.LevelUp();
                }
                else if (PlayerAction.Equals(ActionTypes.LevelDown))
                {
                    Console.WriteLine("Level Down");
                    CurrentPlayer.LevelDown();
                }
                else if (PlayerAction.Equals(ActionTypes.CharacterScreen))
                {
                    CharacterScreen();
                }
                else if (PlayerAction.Equals(ActionTypes.ZedScreen))
                {
                    Console.WriteLine(ZedNames());
                }
                else if (PlayerAction.Equals(ActionTypes.MeleeAttack))
                {
                    Console.WriteLine("Melee attack");
                    List<Character> PossibleVictims = Field.AdjacentCharacters(CurrentPlayer, false);
                    Character Victim = PossibleVictims.ElementAt(GetPlayerAttackChoice(PossibleVictims));
                    MeleeAttack(Victim);
                    if (CurrentPlayer is Fighter)
                    {
                        SquaresLeft -= (int)MaxSquares / 4;
                    }
                    else
                    {
                        SquaresLeft -= (int)MaxSquares / 2;
                    }
                    if (SquaresLeft > 0)
                    {
                        Console.WriteLine("\r\n" + Field.ToString());
                    }
                }
                else if ((PlayerAction.Equals(ActionTypes.UnaimedRangedAttack)
                    || PlayerAction.Equals(ActionTypes.AimedRangedAttack))
                    && CurrentPlayer is Player)
                {
                    Console.WriteLine("Ranged attack");
                    RangedAttack(PlayerAction);
                    SquaresLeft -= (int)MaxSquares / 2;
                    Console.WriteLine(Field.ToString());
                }
                else if (PlayerAction.Equals(ActionTypes.Trade))
                {
                    Console.WriteLine("Begin Trade");
                    BeginTrade();
                }

                KillDeadCharacters();
            }
        }

        private void PlayNextTurnAI()
        {
            SquaresLeft = CurrentPlayer.squares();
            MaxSquares = SquaresLeft;
            Console.WriteLine(Field.ToString());

            List<ActionTypes> PossibleActions = GetPossibleActions(SquaresLeft);
            ActionTypes BestAction = AI.DecideAction(PossibleActions, CurrentPlayer, MaxSquares, SquaresLeft, Field, Players, Zeds);

            if (BestAction.Equals(ActionTypes.Move))
            {
                Coordinate BestMove = AI.DetermineMove((Zed)CurrentPlayer, MaxSquares, SquaresLeft, Field, Players, Zeds);
                Field.MoveCharacterToSquare(CurrentPlayer, BestMove);

            }
            else
            {
                MeleeAttack(AI.DetermineAttack((Zed)CurrentPlayer, MaxSquares, SquaresLeft, Field, Players, Zeds));
            }
        }

        private ActionTypes GetActionFromPlayer(int SquaresLeft)
        {
            //Gets the actions and the respective string
            List<ActionTypes> PossibleActions = GetPossibleActions(SquaresLeft);
            List<string> PossibleActionStrings = ActionsToStrings(PossibleActions);

            //Gets the user's menu choice
            int UserChoiceIndex = CIO.PromptForMenuSelection(PossibleActionStrings, false);

            return PossibleActions.ElementAt(UserChoiceIndex);
        }

        //Returns a list of all of the actions that the current character can perform this turn
        private List<ActionTypes> GetPossibleActions(int SquaresLeft)
        {
            List<ActionTypes> PossibleActions = new List<ActionTypes>();
            PossibleActions.Add(ActionTypes.CharacterScreen);
            PossibleActions.Add(ActionTypes.EndTurn);
            PossibleActions.Add(ActionTypes.LevelUp);
            PossibleActions.Add(ActionTypes.LevelDown);
            PossibleActions.Add(ActionTypes.ZedScreen);
            //Checks for possible moves
            if (Field.PossibleMovesForCharacter(CurrentPlayer, SquaresLeft).Any())
            {
                //Only sees if the character is not trapped. This method (GetPossibleActions)
                //assumes that if it is being called, the character must have some squares left
                PossibleActions.Add(ActionTypes.Move);
            }
            //Checks for the ability to melee attack
            if ((SquaresLeft * 2) >= MaxSquares
                && Field.AdjacentCharacters(CurrentPlayer, false).Any())
            {
                if (CurrentPlayer is Player
                    && ((Player)CurrentPlayer).EquippedWeaponType().Equals("Melee"))
                {
                    PossibleActions.Add(ActionTypes.MeleeAttack);
                }
                else
                {
                    PossibleActions.Add(ActionTypes.MeleeAttack);
                }
            }

            //Options for Players
            if (CurrentPlayer is Player)
            {
                //Checks for the ability to equip a weapon
                Player Current = (Player)CurrentPlayer;
                if (Current.HasWeapon())
                {
                    PossibleActions.Add(ActionTypes.Equip);
                }
                //Checks for the ability to Reload a firearm
                if (Current.EquippedWeaponType().Equals("Ranged"))
                {
                    RangedWeapon _weapon = Current.EquippedWeapon as RangedWeapon;
                    if (_weapon.CurrentClip.Amount() < _weapon.CurrentClip.ClipSize && Current.HasAmmo())
                    {
                        PossibleActions.Add(ActionTypes.Reload);
                    }
                }
                PossibleActions.Add(ActionTypes.SetTrap);
                if (Current.GetType() == typeof(Medic))
                {
                    PossibleActions.Add(ActionTypes.Heal);
                    PossibleActions.Add(ActionTypes.Revive);
                }
                if (Current.GetType() == typeof(Engineer))
                {
                    PossibleActions.Add(ActionTypes.FixWeapon);
                    PossibleActions.Add(ActionTypes.MakeTrap);
                }
                if (Current.GetType() == typeof(Bruiser))
                {
                    PossibleActions.Add(ActionTypes.Shout);
                }
                //Checks for the ability to pick up an item
                if (SquaresLeft >= 2
                    && Field.GetItemsInSquare(CurrentPlayer.Location).Any())
                {
                    PossibleActions.Add(ActionTypes.PickUpItem);
                }
                //Checks for the ability to drop an item
                if (Current.Items.Any())
                {
                    PossibleActions.Add(ActionTypes.DropItem);
                    //Checks for the ability to give an item to a friendly type
                    if (SquaresLeft >= 3)
                    {
                        List<Character> Neighbors = Field.AdjacentCharacters(CurrentPlayer, true);
                        if (Neighbors.Any())
                        {
                            PossibleActions.Add(ActionTypes.GiveItem);
                            //Checks for the ability to initate trade with a Trader
                            foreach (Character C in Neighbors)
                            {
                                if (C is Trader)
                                {
                                    PossibleActions.Add(ActionTypes.Trade);
                                }
                            }
                        }
                    }
                }
                //Checks for the ability to make a ranged attack
                if (Current is Player
                    && (SquaresLeft * 2) >= MaxSquares
                    && Current.EquippedWeaponType().Equals("Ranged")
                    && Current.CanShoot()
                    && Field.PossibleRangedTargets(Current, Zeds).Any())
                {
                    PossibleActions.Add(ActionTypes.UnaimedRangedAttack);
                    //Checks for the ability to make an aimed shot
                    if (SquaresLeft == MaxSquares)
                    {
                        PossibleActions.Add(ActionTypes.AimedRangedAttack);
                    }
                }
                //Testing Ranged Weapons
                //Console.WriteLine(Current.GetType() == typeof(Player));
                //Console.WriteLine((SquaresLeft * 2) >= MaxSquares);
                //Console.WriteLine(Current.EquippedWeaponType().Equals("Ranged"));
                //Console.WriteLine(Current.CanShoot());
                Field.PossibleRangedTargets(Current, Zeds).Any();
            }

            return PossibleActions;
        }

        private List<String> ActionsToStrings(List<ActionTypes> Actions)
        {
            List<String> ActionStrings = new List<String>();
            //Turns each ActionTypes enum into a string and adds it to the list
            foreach (ActionTypes a in Actions)
            {
                ActionStrings.Add(a.ToString());
            }

            //Loops through each string
            for (int i = 0; i < Actions.Count(); i++)
            {
                //Makes a char array from the word and then loops through and finds the 
                //uppercase letters and puts a space in front of it so instead of
                //"AimedRangedAttack" we will see "Aimed Ranged Attack"
                char[] ActionChars = ActionStrings.ElementAt(i).ToCharArray();
                for (int j = 0; j < ActionChars.Length; j++)
                {
                    if (Char.IsUpper(ActionChars[j]))
                    {
                        ActionStrings.ElementAt(i).Insert(j, " ");
                    }
                }
            }
            return ActionStrings;
        }

        /// <summary>
        /// Loops through every Character on the GameArea and removes it from the GameArea and the appropriate List if the 
        /// Character is dead
        /// </summary>
        private void KillDeadCharacters()
        {
            List<int> KilledCharacters = new List<int>();
            for (int i = 0; i < Players.Count; i++)
            {
                Character P = Players.ElementAt(i);
                if (!P.isAlive)
                {
                    if (P is Player)
                    {
                        CorpseSquares.Add(P.Location);
                    }
                    Field.KillCharacter(P);
                    KilledCharacters.Add(i);
                    PlayerOrder.RemoveCharacter(P);
                }
            }
            for (int i = 0; i < KilledCharacters.Count; i++)
            {
                Players.RemoveAt(KilledCharacters.ElementAt(i));
            }
            KilledCharacters.Clear();

            for (int i = 0; i < Zeds.Count; i++)
            {
                Character Z = Zeds.ElementAt(i);
                if (!Z.isAlive)
                {
                    Field.KillCharacter(Z);
                    KilledCharacters.Add(i);
                    ZedOrder.RemoveCharacter(Z);
                }
            }
            for (int i = 0; i < KilledCharacters.Count; i++)
            {
                Zeds.RemoveAt(KilledCharacters.ElementAt(i));
            }
            //if (Players.Count() == 0)
            //{
            //    Console.WriteLine("Game Over.");
            //    Environment.Exit(0);
            //}
        }

        //Returns the next turn's character and removes it from the stack
        //private Character GetNextCharacter()
        //{
        //    return CharacterStack.pop();
        //}
        #endregion

        #region Actions
        /// <summary>
        /// Moves the character to their chosen location
        /// </summary>
        /// <returns>The number of squares their move cost them</returns>
        private int Move()
        {
            //Gets all of the moves that the player can make
            List<Coordinate> PossibleMoves = Field.PossibleMovesForCharacter(CurrentPlayer, SquaresLeft);
            //Prints the moves and gets the user's choice
            List<string> MoveStrings = new List<string>();
            for (int i = 0; i < PossibleMoves.Count(); i++)
            {
                MoveStrings.Add(PossibleMoves.ElementAt(i).ToString());
            }
            int PlayerChoice = CIO.PromptForMenuSelection(MoveStrings, false);
            //Gets the cost it will take to move there
            int MoveCost = Field.ShortestPathCost(CurrentPlayer, PossibleMoves.ElementAt(PlayerChoice));
            //Moves the player
            Field.MoveCharacterToSquare(CurrentPlayer, PossibleMoves.ElementAt(PlayerChoice));

            return MoveCost;
        }

        /// <summary>
        /// Prints the stats and inventory of the current player
        /// </summary>
        private void CharacterScreen()
        {
            if (CurrentPlayer is Player)
            {
                Console.WriteLine(CurrentPlayer.ToString());
            }
            else
            {
                Console.WriteLine(ZedNames());
            }

        }

        private void Equip()
        {
            List<Weapon> Weapons = ((Player)CurrentPlayer).GetWeapons();
            List<string> WeaponChoices = new List<string>();

            for (int i = 0; i < Weapons.Count(); i++)
            {
                WeaponChoices.Add(Weapons.ElementAt(i).Name);
            }

            int PlayerChoice = CIO.PromptForMenuSelection(WeaponChoices, false);
            Weapon ChosenWeapon = Weapons.ElementAt(PlayerChoice);
            Player Current = (Player)CurrentPlayer;
            Current.EquippedWeapon = ChosenWeapon;
        }

        /// <summary>
        /// Drops an item. Removes it from the player's inventory and puts it in that gridsquare
        /// </summary>
        private void DropItem()
        {
            Player Current = (Player)CurrentPlayer;
            List<string> ItemsToDrop = new List<string>();
            for (int i = 0; i < Current.Items.Count(); i++)
            {
                ItemsToDrop.Add(Current.Items.ElementAt(i).ToString());
            }

            int PlayerChoice = CIO.PromptForMenuSelection(ItemsToDrop, false);
            Item DropItem = Current.Items.ElementAt(PlayerChoice);
            Current.Items.Remove(DropItem);
            Field.PutItemInSquare(DropItem, CurrentPlayer.Location);
        }

        /// <summary>
        /// Gives an item to an adjacent player
        /// </summary>
        private void GiveItem()
        {
            List<Character> Friendlies = Field.AdjacentCharacters(CurrentPlayer, true);
            List<string> Choices = new List<string>();
            for (int i = 0; i < Friendlies.Count(); i++)
            {
                Player c = (Player)Friendlies.ElementAt(i);
                Choices.Add("Level " + c.Level + " at " + c.Location.ToString());
            }

            int PlayerChoice = CIO.PromptForMenuSelection(Choices, false);
            Player Friendly = (Player)Friendlies.ElementAt(PlayerChoice);
            Player Current = (Player)CurrentPlayer;

            List<string> ItemsToDrop = new List<string>();
            for (int i = 0; i < Current.Items.Count(); i++)
            {
                ItemsToDrop.Add(Current.Items.ElementAt(i).ToString());
            }
            ItemsToDrop.Add("Money");


            PlayerChoice = CIO.PromptForMenuSelection(ItemsToDrop, false);
            if (PlayerChoice == ItemsToDrop.Count - 1)
            {
                int Amount = CIO.PromptForInt("You have $" + CurrentPlayer.Money + ", how much would you like to give?", 0, CurrentPlayer.Money);
                CurrentPlayer.Money -= Amount;
                Friendly.Money += Amount;
            }
            else
            {
                Item GiveItem = Current.Items.ElementAt(PlayerChoice);
                Current.Items.Remove(GiveItem);
                Friendly.AddItem(GiveItem);
            }
        }

        /// <summary>
        /// Prompts the user for an option to pick up an item on the grid they're standing on
        /// </summary>
        private void PickUpItem()
        {
            List<Item> ItemsInSquare = Field.GetItemsInSquare(CurrentPlayer.Location);
            List<string> ItemNames = new List<string>();

            for (int i = 0; i < ItemsInSquare.Count(); i++)
            {
                ItemNames.Add(ItemsInSquare.ElementAt(i).Name);
            }

            int PlayerChoice = CIO.PromptForMenuSelection(ItemNames, false);
            Item PlayerChoiceItem = ItemsInSquare.ElementAt(PlayerChoice);
            if (PlayerChoiceItem.GetType() == typeof(Item))
            {
                ((Player)CurrentPlayer).Money += PlayerChoiceItem.Value;
            }
            else if (PlayerChoiceItem is Health)
            {
                Health HealthPack = (Health)PlayerChoiceItem;
                if (CurrentPlayer is Medic)
                {
                    ((Player)CurrentPlayer).AddItem(PlayerChoiceItem);
                }
                else
                {
                    CurrentPlayer.Health += HealthPack.AmountHealed.Roll();
                }
            }
            else
            {
                ((Player)CurrentPlayer).AddItem(PlayerChoiceItem);
            }

            Field.RemoveItemInSquare(PlayerChoiceItem, CurrentPlayer.Location);
        }

        /// <summary>
        /// Reloads the Players equipped weapon until the magazine is full or the Player runs out of ammo.
        /// </summary>
        private void Reload()
        {
            Player Current = CurrentPlayer as Player;
            RangedWeapon _weapon = Current.EquippedWeapon as RangedWeapon;
            for (int i = 0; i < CurrentPlayer.Items.Count; i++)
            {
                if (CurrentPlayer.Items.ElementAt(i) is Ammo && !_weapon.CurrentClip.IsFull())
                {
                    _weapon.CurrentClip.Push(CurrentPlayer.Items.ElementAt(i) as Ammo);
                    CurrentPlayer.Items.RemoveAt(i--);
                }
            }
            Console.WriteLine("Successful reload");
        }

        /// <summary>
        /// Performs a melee attack on a victim
        /// </summary>
        private void MeleeAttack(Character Victim)
        {
            int NaturalStrike = DieRoll.RollOne(20);
            int TotalStrike = NaturalStrike + CurrentPlayer.StrikeBonus();
            Console.WriteLine("Struck for " + TotalStrike);
            if ((TotalStrike > 4 && TotalStrike > Victim.ArmorRating) || NaturalStrike == 20)
            {
                Attack CharAttack = CurrentPlayer.MeleeAttack();

                Console.WriteLine("Attack Hit!");
                Console.WriteLine("Attacked for {0}", CharAttack.Damage);// + Damage);
                if (NaturalStrike == 20)
                {
                    CharAttack.Damage *= 2;
                    Console.WriteLine("Critical hit! Damage x2");
                }
                int NaturalDefense = DieRoll.RollOne(20);
                int TotalDefense = 0;
                bool AttemptedToDefend = false;
                //Parrying or dodging
                if (Victim.CanParry && !(CurrentPlayer is Tank))
                {
                    TotalDefense = NaturalDefense + Victim.toParry();
                    Victim.CanParry = false;
                    Console.WriteLine("Enemy parried for " + TotalDefense);
                    AttemptedToDefend = true;
                }
                else if (Victim.CanDodge)
                {
                    TotalDefense = NaturalDefense + Victim.toDodge();
                    Victim.CanDodge = false;
                    Victim.HasDodged = true;
                    Console.WriteLine("Enemy dodged for " + TotalDefense);
                    AttemptedToDefend = true;
                }



                //Checks for a botch on the defender's part
                if (NaturalDefense == 1)
                {
                    Console.WriteLine("Enemy rolled a botch! Attacker damage x2");
                    CharAttack.Damage *= 2;
                }
                double Times = DetermineMultiplier(CurrentPlayer, Victim);
                if (Times == 2)
                {
                    Console.WriteLine("Attack is twice as effective!");
                }
                else if (Times == 1.5)
                {
                    Console.WriteLine("Attack is 1.5 times as effective!");
                }
                else if (Times == .5)
                {
                    Console.WriteLine("Attack is half as effective!");
                }
                CharAttack.Damage = (int)(Times * CharAttack.Damage);

                //Battle
                if (CharAttack.Damage > TotalDefense)
                {
                    if (AttemptedToDefend && NaturalDefense == 20)
                    {
                        Console.WriteLine("Enemy rolled a natural 20 to defend! Attack failed.");
                    }
                    else
                    {
                        Console.WriteLine("Enemy was hit for {0} damage", CharAttack.Damage);
                        Victim.takeDamage(CharAttack);
                    }
                }
                else
                {
                    Console.WriteLine("Enemy defended successfully");
                }
            }
            else
            {
                CurrentPlayer.MeleeAttack();
                Console.WriteLine("Attack ineffective");
            }
        }

        /// <summary>
        /// Lists all of the possible victims in view of the player,
        /// and then preforms a RangedAttack (Aimed or Unaimed depending
        /// on the PlayerAction parameter) on the victim the player chooses
        /// </summary>
        /// <param name="PlayerAction"></param>
        private void RangedAttack(ActionTypes PlayerAction)
        {
            List<Character> PossibleVictims = Field.PossibleRangedTargets(CurrentPlayer, Zeds);
            Character Victim = PossibleVictims.ElementAt(GetPlayerAttackChoice(PossibleVictims));
            //Player Current = (Player)CurrentPlayer;
            Player Current = CurrentPlayer as Player;

            int Bonus = 0;
            if (PlayerAction == ActionTypes.AimedRangedAttack)
            {
                Bonus = 3;
            }

            int Strike = Current.toHitRanged(Bonus);
            Console.WriteLine("Rolled " + Strike);
            if (Strike > 4 && Strike > Victim.ArmorRating)
            {
                int Damage = Current.RangedAttack();
                int Defense = 0;
                //Parrying or dodging
                if (Victim.CanParry)
                {
                    Defense = Victim.toParry();
                }
                else if (Victim.CanDodge)
                {
                    Defense = Victim.toDodge();
                }

                //Battle
                if (Damage > Defense)
                {
                    Console.WriteLine("Enemy hit for {0} damage", Damage);
                    Victim.takeDamage(new Attack(Damage));
                }
                else
                {
                    Console.WriteLine("Enemy defended successfully");
                }
            }
            else
            {
                Current.RangedAttack();
                Console.WriteLine("Attack ineffective");
            }
        }

        /// <summary>
        /// Begins a Trade between the CurrentPlayer and an adjacent Trader
        /// </summary>
        private void BeginTrade()
        {
            //Gets the Trader that is next to the CurrentPlayer to begin Trading
            Trader Trader = null;
            foreach (Trader T in Field.AdjacentCharacters(CurrentPlayer, true))
            {
                Trader = T;
            }

            //Just in case there is no Trader next to the CurrentPlayer
            if (Trader != null)
            {
                Transaction Exchange = new Transaction((Player)CurrentPlayer, Trader);
                Console.WriteLine("Transaction Started");
                while (!Exchange.Done)
                {
                    Console.WriteLine("You have $" + (CurrentPlayer.Money + Exchange.BuyerMoneyChange));
                    int UserChoice = CIO.PromptForMenuSelection(new List<string>(new string[] { "Buy an item from the Trader", "Buy ammo From the Trader", 
                        "Sell an item to the Trader", "Sell ammo to the Trader", "End Transaction", "Cancel Transaction" }), false);
                    //Console.WriteLine("Input was : " + UserChoice);
                    if (UserChoice == 0)
                    {
                        BuyFromTrader(Exchange);
                    }
                    if (UserChoice == 1)
                    {
                        BuyAmmo(Exchange);
                    }
                    if (UserChoice == 2)
                    {
                        SellToTrader(Exchange);
                    }
                    if (UserChoice == 3)
                    {
                        SellAmmo(Exchange);
                    }
                    if (UserChoice == 4)
                    {
                        Exchange.FinishTransaction();
                    }
                    if (UserChoice == 5)
                    {
                        Exchange.CancelTransaction();
                    }
                }
            }
        }

        /// <summary>
        /// Allows the CurrentPlayer to buy an Item from the Trader
        /// </summary>
        /// <param name="T"></param>
        private void BuyFromTrader(Transaction Exchange)
        {
            Trader T = (Trader)Exchange.Seller;
            //Prints out the Items that can be bought from the Trader
            List<string> ItemsForSale = new List<string>();
            for (int i = 0; i < T.Items.Count; i++)
            {
                Item I = T.Items.ElementAt(i);
                ItemsForSale.Add(I.Name + " for sale at the price $" + T.PurchasePrice(I));
            }
            int UserChoice = CIO.PromptForMenuSelection(ItemsForSale, false);
            Item ChoosenItem = T.Items.ElementAt(UserChoice);
            int Price = T.PurchasePrice(ChoosenItem);
            if (CurrentPlayer.Money + Exchange.BuyerMoneyChange >= Price && CIO.PromptForBool("Are you sure you want to buy " + ChoosenItem.Name + " for $" + Price + ".", "Yes", "No"))
            {
                Exchange.PurchaseItem(ChoosenItem, Price);
                //CurrentPlayer.Items.Add(T.PurchaseItem(UserChoice));
            }
            else
            {
                Console.WriteLine("You cannot afford " + ChoosenItem.Name + " for $" + Price);
            }
        }

        /// <summary>
        /// Allows the CurrentPlayer to sell an Item to the Trader
        /// </summary>
        /// <param name="T"></param>
        private void SellToTrader(Transaction Exchange)
        {
            Trader T = (Trader)Exchange.Seller;
            List<string> ItemsToSell = new List<string>();
            for (int i = 0; i < CurrentPlayer.Items.Count; i++)
            {
                Item I = CurrentPlayer.Items.ElementAt(i);
                ItemsToSell.Add("You can sell " + I.Name + " for $" + T.SellPrice(I));
            }
            int UserChoice = CIO.PromptForMenuSelection(ItemsToSell, false);
            Item ChoosenItem = CurrentPlayer.Items.ElementAt(UserChoice);
            int Price = T.SellPrice(ChoosenItem);
            if (CIO.PromptForBool("Are you sure you want to sell " + ChoosenItem.Name + " for $" + Price, "Yes", "No"))
            {
                Exchange.SellItem(ChoosenItem, Price);
                //CurrentPlayer.Items.RemoveAt(UserChoice);
                //CurrentPlayer.Money += T.SellItem(ChoosenItem);
            }
        }

        /// <summary>
        /// Helper method to allow the CurrentPlayer to purchase ammo from a Trader
        /// </summary>
        /// <param name="Exchange"></param>
        private void BuyAmmo(Transaction Exchange)
        {
            Trader T = (Trader)Exchange.Seller;

            int UserChoice = CIO.PromptForMenuSelection(new List<string>(new string[] { "Handgun for $1 a round", "Rifle for $2 a round", "Shotgun for $2 a round" }), false);
            AmmoType ChosenType = AmmoType.Handgun;
            switch (UserChoice)
            {
                case 0: ChosenType = AmmoType.Handgun; break;
                case 1: ChosenType = AmmoType.Rifle; break;
                case 2: ChosenType = AmmoType.Shotgun; break;
            }

            int Amount = CIO.PromptForInt("How many bullets would you like to purchase", 0, 100);
            int Price = T.PurchaseAmmoCost(ChosenType, Amount);
            if (CurrentPlayer.Money + Exchange.BuyerMoneyChange >= Price && CIO.PromptForBool("Are you sure that you would like to purchase " + Amount + " for $" + Price + "?", "Yes", "No"))
            {
                Exchange.BuyerMoneyChange -= Price;
                Exchange.SellerMoneyChange += Price;
                foreach (Ammo A in T.BuyAmmo(ChosenType, Amount))
                {
                    Exchange.SellingItems.Add(A);
                }
            }
        }

        private string ZedNames()
        {
            string nameNumber = "";
            for (int i = 0; i < Zeds.Count(); i++)
            {
                string name = "";
                if(Zeds[i].GetType() == typeof(Sloucher))
                {
                    name = "Sloucher";
                }
                else if (Zeds[i].GetType() == typeof(FastAttack))
                {
                    name = "Fast Attack";
                }
                else if (Zeds[i].GetType() == typeof(Tank))
                {
                    name = "Tank";
                }
                else if (Zeds[i].GetType() == typeof(Shank))
                {
                    name = "Shank";
                }
                nameNumber += "Name: " + name + (i + 1) + "\r\n" + Zeds[i].ToString() + "\r\n";
            }
            return nameNumber;
        }

        private void Revive()
        {
            List<Character> FriendliesCorpses = Field.AdjacentCharacters(CurrentPlayer, true);
            List<string> Choices = new List<string>();
            for (int i = 0; i < FriendliesCorpses.Count(); i++)
            {
                Player c = (Player)FriendliesCorpses.ElementAt(i);
                Choices.Add("Level " + c.Level + " at " + c.Location.ToString());
            }

            int PlayerChoice = CIO.PromptForMenuSelection(Choices, false);
            Player Friendly = (Player)FriendliesCorpses.ElementAt(PlayerChoice);
            Player Current = (Player)CurrentPlayer;

            List<string> ItemsToDrop = new List<string>();
            for (int i = 0; i < Current.Items.Count(); i++)
            {
                ItemsToDrop.Add(Current.Items.ElementAt(i).ToString());
            }

            PlayerChoice = CIO.PromptForMenuSelection(ItemsToDrop, false);
            Item GiveItem = Current.Items.ElementAt(PlayerChoice);

            Current.Items.Remove(GiveItem);
            int rev = new DieRoll(1, 20).Roll(); ;
            if (rev >= 14)
            {
                Friendly.isAlive = true;
                Console.WriteLine("You Succesfully Revived Your Ally!");
            }
            else
            {
                Console.WriteLine("Sorry the procedure failed.");
            }
        }

        private void SellAmmo(Transaction Exchange)
        {
            Trader T = (Trader)Exchange.Seller;
            List<Ammo> HandgunAmmo = new List<Ammo>();
            List<Ammo> RifleAmmo = new List<Ammo>();
            List<Ammo> ShotgunAmmo = new List<Ammo>();
            foreach (Item I in CurrentPlayer.Items)
            {
                if (I is Ammo)
                {
                    Ammo A = (Ammo)I;
                    switch (A.AmmoType)
                    {
                        case AmmoType.Handgun: HandgunAmmo.Add(A); break;
                        case AmmoType.Rifle: RifleAmmo.Add(A); break;
                        case AmmoType.Shotgun: ShotgunAmmo.Add(A); break;
                    }
                }
            }
            List<string> Choice = new List<string>(new string[] { "You can sell " + HandgunAmmo.Count + " rounds of handgun ammo for $1 each",
                "You can sell " + RifleAmmo.Count + " rounds of handgun ammo for $2 each", "You can sell " + ShotgunAmmo.Count + " rounds of handgun ammo for $2 each"});
            int UserChoice = CIO.PromptForMenuSelection(Choice, false);
            List<Ammo> ChoosenAmmo = null;
            switch (UserChoice)
            {
                case 0: ChoosenAmmo = HandgunAmmo; break;
                case 1: ChoosenAmmo = RifleAmmo; break;
                case 2: ChoosenAmmo = ShotgunAmmo; break;
            }
            if (ChoosenAmmo.Count > 0)
            {
                AmmoType ChoosenType = ChoosenAmmo.ElementAt(0).AmmoType;
                int Amount = 0;
                bool IllegalAmount = true;
                while (IllegalAmount)
                {
                    Amount = CIO.PromptForInt("How many rounds would you like to sell?", 0, T.SellAmmoLimit(ChoosenType));
                    IllegalAmount = Amount >= ChoosenAmmo.Count;
                    if (IllegalAmount)
                    {
                        Console.WriteLine("You don't have that many bullets");
                    }
                }
                Item AmmoValue = new Item();
                AmmoValue.Value = (new Ammo(ChoosenType)).Value * Amount;
                int Price = T.SellPrice(AmmoValue);
                for (int i = 0; i < Amount; i++)
                {
                    Exchange.SellItem(ChoosenAmmo.ElementAt(i), Price);
                }
            }

        }

        /// <summary>
        /// Returns the index of the array of characters that the user chose to attack
        /// </summary>
        /// <param name="PossibleVictims"></param>
        /// <returns></returns>
        private int GetPlayerAttackChoice(List<Character> PossibleVictims)
        {
            List<string> VictimChoices = new List<string>();

            for (int i = 0; i < PossibleVictims.Count(); i++)
            {
                Character Victim = PossibleVictims.ElementAt(i);
                VictimChoices.Add("Level " + Victim.Level + " at (" + Victim.Location.X + ", " + Victim.Location.Y + ")");
            }

            return CIO.PromptForMenuSelection(VictimChoices, false);
        }
        public void Save(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Field);
            formatter.Serialize(stream, Zeds);
            formatter.Serialize(stream, Players);
            formatter.Serialize(stream, CorpseSquares);
            formatter.Serialize(stream, TrapLocations);
            formatter.Serialize(stream, AI);
            formatter.Serialize(stream, HordeMode);
            stream.Dispose();
        }
        #endregion

        #region Logic Helper Methods

        private double DetermineMultiplier(Character Attacker, Character Defender)
        {
            double Multiplier = 1;

            if (Attacker is Player)
            {
                Player PlayerAttacker = (Player)Attacker;
                if (PlayerAttacker.EquippedWeapon != null)
                {
                    Multiplier = Defender.DetermineWeaponEffectiveness(PlayerAttacker.EquippedWeapon);
                }
            }

            return Multiplier;
        }
        #endregion
    }
}
