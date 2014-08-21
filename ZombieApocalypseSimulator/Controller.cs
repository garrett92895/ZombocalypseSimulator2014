using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZombieApocalypse;
using ZombieApocalypseSimulator.Models;
using ZombieApocalypseSimulator.Models.Characters;
using CSC160_ConsoleMenu;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters.Classess;

namespace ZombieApocalypseSimulator
{
    public enum ActionTypes
    {
        CharacterScreen,
        EndTurn,
        Move,
        Equip,
        MeleeAttack,
        AimedRangedAttack,
        UnaimedRangedAttack,
        PickUpItem,
        DropItem,
        GiveItem
    }

    public class Controller
    {
        #region Props and Backing Fields
        public GameArea Field { get; set; }
        private Character CurrentPlayer;
        private int MaxSquares;
        private int SquaresLeft;
        public List<Character> Zeds;
        public List<Character> Players;
        public List<Coordinate> CorpseSquares;
        public CharacterStack PlayerOrder;
        public CharacterStack ZedOrder;
        #endregion

        #region Ctor and Run
        public Controller()
        {
            Field = new GameArea(15, 15);
            Zeds = new List<Character>();
            Players = new List<Character>();
            CorpseSquares = new List<Coordinate>();
            MakeZombies(40);
            MakePlayer();
        }

        public void Run()
        {
            while (true)
            {
                PlayerOrder = DetermineTurnOrder(Players);
                ZedOrder = DetermineTurnOrder(Zeds);

                for (int i = 0; i < Players.Count(); i++)
                {
                    CurrentPlayer = PlayerOrder.Pop();
                    PlayNextTurn();
                }

                for (int i = 0; i < Zeds.Count(); i++)
                {
                    CurrentPlayer = ZedOrder.Pop();
                    PlayNextTurn();
                }
            }
        }


        //Places zombies in the GameArea. Takes in an integer to determine how many zombies to place
        //It might be smart to check that there will be enough room on the board to support the requested
        //number of zombies
        private void MakeZombies(int NumOfZombies)
        {
            Random rand = new Random();
            for (int i = 0; i < NumOfZombies; i++)
            {
                Zed zambie = ZedFactory.GetInstance("Sloucher");
                Coordinate Location = Field.GetViableSquare();
                Field.AddCharacterToSquare(zambie, Location);
                Zeds.Add(zambie);
                zambie.Location = Location;
            }
        }

        //Makes players and places them on the GameArea. This can either be done automatically or
        //with user input
        private void MakePlayer()
        {
            Player p = new Player();
            Coordinate Location = Field.GetViableSquare();
            Field.AddCharacterToSquare(p, Location);
            Players.Add(p);
            p.Location = Location;
        }
        #endregion

        #region Turn Sorting
        private CharacterStack DetermineTurnOrder(List<Character> _characters)
        {
            CharacterStack _turnOrder = new CharacterStack(_characters.Count);
            int[] rolls = new int[_characters.Count];
            for (int i = 0; i < _characters.Count; i++)
            {                
                if (_characters[i].GetType() == typeof(Shank))
                {
                    rolls[i] = Dice.Roll(1,20,1,-2);
                }
                else rolls[i] = Dice.Roll(1, 20);
            }
            int LastIndex = -1;
            for (int i = 0; i < _characters.Count; i++)
            {
                LastIndex = minValue(rolls, LastIndex);
                Character c = _characters[LastIndex];
                
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
                }
                else if (PlayerAction.Equals(ActionTypes.EndTurn))
                {
                    Console.WriteLine("Ended turn");
                    SquaresLeft -= MaxSquares;
                }
                else if (PlayerAction.Equals(ActionTypes.Equip))
                {
                    Console.WriteLine("Equip");
                    Equip();
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
                else if (PlayerAction.Equals(ActionTypes.PickUpItem))
                {
                    Console.WriteLine("Pick up item");
                    PickUpItem();
                    SquaresLeft -= 2;
                }
                else if (PlayerAction.Equals(ActionTypes.CharacterScreen))
                {
                    CharacterScreen();
                }
                else if (PlayerAction.Equals(ActionTypes.MeleeAttack))
                {
                    Console.WriteLine("Melee attack");
                    MeleeAttack();
                    SquaresLeft -= (int)MaxSquares / 2;
                    if (SquaresLeft > 0)
                    {
                        Console.WriteLine("\r\n" + Field.ToString());
                    }
                }
                else if ((PlayerAction.Equals(ActionTypes.UnaimedRangedAttack)
                    || PlayerAction.Equals(ActionTypes.AimedRangedAttack))
                    && CurrentPlayer.GetType() == typeof(Player))
                {
                    Console.WriteLine("Ranged attack");
                    RangedAttack(PlayerAction);
                    SquaresLeft -= (int)MaxSquares / 2;
                    Console.WriteLine(Field.ToString());
                }

                KillDeadCharacters();
            }

            Zeds.AddRange(Field.MakeReviveRolls(CorpseSquares));
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
                if (CurrentPlayer.GetType() == typeof(Player)
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
            if (CurrentPlayer.GetType() == typeof(Player))
            {
                Player Current = (Player)CurrentPlayer;
                if (Current.HasWeapon())
                {
                    PossibleActions.Add(ActionTypes.Equip);
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
                    if (SquaresLeft >= 3
                        && Field.AdjacentCharacters(CurrentPlayer, true).Any())
                    {
                        PossibleActions.Add(ActionTypes.GiveItem);
                    }
                }
                //Checks for the ability to make a ranged attack
                if (Current.GetType() == typeof(Player)
                    && (SquaresLeft * 2) >= MaxSquares
                    && Current.EquippedWeaponType().Equals("Ranged")
                    && Current.CanShoot()
                    && Field.PossibleRangedTargets(Current, Zeds).Any())
                {
                    PossibleActions.Add(ActionTypes.UnaimedRangedAttack);
                    //Checks for the ability to make and aimed shot
                    if (SquaresLeft == MaxSquares)
                    {
                        PossibleActions.Add(ActionTypes.AimedRangedAttack);
                    }
                }
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
            foreach (Character P in Players)
            {
                if (!P.isAlive)
                {
                    Field.KillCharacter(P);
                    Players.Remove(P);
                    PlayerOrder.RemoveCharacter(P);
                    CorpseSquares.Add(P.Location);
                }
            }

            foreach (Character Z in Zeds)
            {
                if (!Z.isAlive)
                {
                    Field.KillCharacter(Z);
                    Zeds.Remove(Z);
                    ZedOrder.RemoveCharacter(Z);
                }
            }
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
            Console.WriteLine(CurrentPlayer.ToString());
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

            PlayerChoice = CIO.PromptForMenuSelection(ItemsToDrop, false);
            Item GiveItem = Current.Items.ElementAt(PlayerChoice);

            Current.Items.Remove(GiveItem);
            Friendly.AddItem(GiveItem);
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
            ((Player)CurrentPlayer).AddItem(PlayerChoiceItem);
            Field.RemoveItemInSquare(PlayerChoiceItem, CurrentPlayer.Location);
        }

        /// <summary>
        /// Performs a melee attack on a victim
        /// </summary>
        private void MeleeAttack()
        {
            List<Character> PossibleVictims = Field.AdjacentCharacters(CurrentPlayer, false);
            Character Victim = PossibleVictims.ElementAt(GetPlayerAttackChoice(PossibleVictims));

            int Strike = CurrentPlayer.toHitMelee();
            Console.WriteLine("Struck for " + Strike);
            if (Strike > 4 && Strike > Victim.ArmorRating)
            {
                int Damage = CurrentPlayer.MeleeAttack();
                Console.WriteLine("Attack Hit!");
                Console.WriteLine("Attacked for " + Damage);
                int Defense = 0;
                //Parrying or dodging
                if (Victim.CanParry)
                {
                    Defense = Victim.toParry();
                    Victim.CanParry = false;
                    Console.WriteLine("Enemy parried for " + Defense);
                }
                else if (Victim.CanDodge)
                {
                    Defense = Victim.toDodge();
                    Victim.CanDodge = false;
                    Console.WriteLine("Enemy dodged for " + Defense);
                }

                //Battle
                if (Damage > Defense)
                {
                    Console.WriteLine("Enemy was hit for {0} damage", Damage);
                    Victim.takeDamage(Damage);
                }
                else
                {
                    Console.WriteLine("Enemy defended successfully");
                }
            }
            else
            {
                Console.WriteLine("Attack ineffective");
            }
        }

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

            int Strike = CurrentPlayer.toHitRanged(Bonus);
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
                    Victim.takeDamage(Damage);
                }
                else
                {
                    Console.WriteLine("Enemy defended successfully");
                }
            }
            else
            {
                Console.WriteLine("Attack ineffective");
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

        #endregion
    }
}


