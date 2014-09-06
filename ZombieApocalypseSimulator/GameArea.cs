using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator
{
    enum Direction{ UP, RIGHT, DOWN, LEFT, NONE }

    public class GameArea
    {
        #region Properties
        /// <summary>
        /// Determines the Height of GridSquares
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Determines the Width of GridSquares
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 2D Array of GridSquares
        /// </summary>
        public GridSquare[,] GridSquares { get; set; }
        #endregion
        Random Rand;
        public GameArea(int NewHeight = 10, int NewWidth = 10)
        {
            Height = NewHeight;
            Width = NewWidth;
            ClearBoard();
            Rand = new Random();
        }

        #region GridSquare Operations
        /// <summary>
        /// Returns a Randomly Selected Coordinate from a rectangle formed with the given TopLeft and TopRight.  
        /// If TopLeft is not specified will defualt the value to the GameArea's Top Left GridSquare.  
        /// If BottomRight is not specified will default the value to the GameArea's Bottom Right GridSquare.  
        /// Will return null if no viable GridSquare could be found in the specified area
        /// </summary>
        /// <param name="TopLeft"></param>
        /// <param name="BottomRight"></param>
        /// <returns></returns>
        public Coordinate GetViableSquare(Coordinate TopLeft = null, Coordinate BottomRight = null)
        {
            //Sets the Coordinates to their default values covering the entire board
            if (TopLeft == null)
            {
                TopLeft = new Coordinate(0, 0);
            }
            if (BottomRight == null)
            {
                BottomRight = new Coordinate(Width - 1, Height - 1);
            }

            //Gets a List of all the GridSquares that are not closed and not occupied
            List<GridSquare> ViableSquares = new List<GridSquare>();
            for (int i = TopLeft.X; i <= BottomRight.X; i++)
            {
                for (int j = TopLeft.Y; j <= BottomRight.Y; j++)
                {
                    Coordinate TargetLoc = new Coordinate(i, j);
                    GridSquare Target = GetGridSquareAt(TargetLoc);
                    if (Target.IsOccupiable && Target.OccupyingCharacter == null)
                    {
                        ViableSquares.Add(Target);
                    }
                }
            }

            //Returns a null Coordinate if no viable squares were found
            if (ViableSquares.Count <= 0)
            {
                return null;
            }
            //Returns a Randomly selected GridSquare from the ViableSquares
            return ViableSquares.ElementAt(DieRoll.RollOne(ViableSquares.Count) - 1).Coordinate;
        }

        /// <summary>
        /// Toggles the IsOccupiable flag on the given GridSquare
        /// </summary>
        /// <param name="Target"></param>
        public void ToggleGridSquareClosed(Coordinate Location)
        {
            try
            {
                GridSquare Target = GetGridSquareAt(Location);
                Target.IsOccupiable = !Target.IsOccupiable;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex);
            }

        }

        /// <summary>
        /// Returns the GridSquare at the given x and y in GridSquares.
        /// Throws an ArgumentOutOfRangeException if either the x or the y is out the range of GridSquares
        /// </summary>
        /// <param name="TargetX"></param>
        /// <param name="TargetY"></param>
        /// <returns></returns>
        public GridSquare GetGridSquareAt(Coordinate Location)
        {
            if (Location.X >= 0 && Location.X < Width && Location.Y >= 0 && Location.Y < Height)
            {
                return GridSquares[Location.X, Location.Y];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

        }

        /// <summary>
        /// Returns a List of GridSquares that are adjacent to the given Location
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        private List<GridSquare> GetAdjacentGridSquares(Coordinate Location)
        {
            //Easier to reference x and y
            int tX = Location.X;
            int tY = Location.Y;
            List<GridSquare> NeighborSquares = new List<GridSquare>();
            List<Coordinate> NeighborCoordinates = new List<Coordinate>()
                {
                    new Coordinate(tX - 1, tY - 1),
                    new Coordinate(tX, tY - 1),
                    new Coordinate(tX - 1, tY),
                    new Coordinate(tX + 1, tY + 1),
                    new Coordinate(tX, tY + 1),
                    new Coordinate(tX + 1, tY),
                    new Coordinate(tX - 1, tY + 1),
                    new Coordinate(tX + 1, tY - 1)
                };
            for (int i = 0; i < NeighborCoordinates.Count(); i++)
            {
                Coordinate CurrentCoordinate = NeighborCoordinates.ElementAt(i);
                if (CurrentCoordinate.X >= 0 && CurrentCoordinate.X < Width
                    && CurrentCoordinate.Y >= 0 && CurrentCoordinate.Y < Height)
                {
                    NeighborSquares.Add(GetGridSquareAt(CurrentCoordinate));
                }
            }
            return NeighborSquares;
        }
        #endregion

        #region Items Operations
        /// <summary>
        /// Returns the List of Items located on the GridSquare at the given locations.  
        /// If the GridSquare does not exist (i.e. locations are off the board), will return 
        /// null.
        /// </summary>
        /// <param name="TargetX"></param>
        /// <param name="TargetY"></param>
        /// <returns></returns>
        public List<Item> GetItemsInSquare(Coordinate Location)
        {
            List<Item> Items = new List<Item>();
            try
            {
                GridSquare Square = GetGridSquareAt(Location);
                Items = Square.ItemList;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex);
            }
            return Items;
        }

        /// <summary>
        /// Puts an item in the given location
        /// </summary>
        /// <param name="DroppedItem"></param>
        /// <param name="Location"></param>
        public void PutItemInSquare(Item DroppedItem, Coordinate Location)
        {
            GridSquares[Location.X, Location.Y].ItemList.Add(DroppedItem);
        }

        public void RemoveItemInSquare(Item RemoveItem, Coordinate Location)
        {
            GridSquare Square = GridSquares[Location.X, Location.Y];
            Square.ItemList.Remove(RemoveItem);
        }
        #endregion

        #region Character Operations
        /// <summary>
        /// Iterates over every square of the board starting at (0,0) and returns true the moment an Non-Closed UnOccupied GridSquare is found
        /// If no such squares are found will return false
        /// </summary>
        /// <returns></returns>
        public bool CanAddCharacter()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Coordinate Loc = new Coordinate(i, j);
                    GridSquare Target = GetGridSquareAt(Loc);
                    if (Target.IsOccupiable && Target.OccupyingCharacter == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Adds the given Character to the given GridSquare only if the given GridSquare is Occupiable and does not already contain a character.  
        /// Sets the Character's Coordinates to the GridSquare it was added to.
        /// </summary>
        /// <param name="C"></param>
        /// <param name="Target"></param>
        public void AddCharacterToSquare(Character C, Coordinate Destination)
        {
            try
            {
                GridSquare Target = GetGridSquareAt(Destination);
                if (Target.OccupyingCharacter == null
                    && Target.IsOccupiable)
                {
                    C.Location = Target.Coordinate;
                    Target.OccupyingCharacter = C;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Removes a Given Character from the GridSquare it is currently occupying and adds it to 
        /// the Target GridSquare.  If the Target GridSquare is not occupiable then will throw
        /// </summary>
        /// <param name="C"></param>
        /// <param name="Target"></param>
        public void MoveCharacterToSquare(Character C, Coordinate Destination)
        {
            try
            {
                GridSquare Target = GetGridSquareAt(Destination);
                if (Target.OccupyingCharacter == null
                    && Target.IsOccupiable)
                {
                    RemoveCharacterFromSquare(C);
                    AddCharacterToSquare(C, Destination);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Removes the given Character from the GridSquare and adds a Corpse Item to the GridSquare with the same properties as the killed Character
        /// Returns the Corpse of the killed Character
        /// </summary>
        /// <param name="C"></param>
        public void KillCharacter(Character C)
        {
            GridSquare Target = GetGridSquareAt(C.Location);

            if (C is Player)
            {
                Corpse Body = new Corpse(C);
                Target.ItemList.Add(Body);
            }
            else
            {
                if (C.Items != null)
                {
                    Target.ItemList.AddRange(C.Items);
                }
            }

            Target.OccupyingCharacter = null;
        }

        /// <summary>
        /// Removes the given Character from the GridSquare that they occupy and the 
        /// GridSquare that the Character knows where they are at will be set to null
        /// </summary>
        /// <param name="C"></param>
        /// <param name="Target"></param>
        public void RemoveCharacterFromSquare(Character C)
        {
            GetGridSquareAt(C.Location).OccupyingCharacter = null;
        }
        #endregion

        #region Method for Possible Moves, Targets, etc
        /// <summary>
        /// Returns a List containing all the Coordinates that a Character that starts at the given Location with the given number of SquaresLeft
        /// can move to.  Will not count GridSquares that are occupied by other Characters but will allow movement through them if the OccupyingCharacter's 
        /// type is the same as OriginalType
        /// </summary>
        /// <param name="TargetX"></param>
        /// <param name="TargetY"></param>
        /// <returns></returns>
        public List<Coordinate> PossibleMovesForCharacter(Character C, int SquaresLeft, bool DiagonalsCostTwo = false)
        {
            List<Coordinate> Moves = new List<Coordinate>();
            Type OriginalType = null;
            if (C is Player)
            {
                OriginalType = typeof(Player);
            }
            else
            {
                OriginalType = typeof(Zed);
            }

            //foreach (GridSquare GS in GetAdjacentGridSquares(C.Location))
            //{
            //    //True if the Movement from this GridSquare to the Neighbor is diagonal
            //    bool DiagonalMovement = Math.Abs(C.Location.X - GS.Coordinate.X) == Math.Abs(C.Location.Y - GS.Coordinate.Y);
            //    if (DiagonalMovement)
            //    {
            //        Moves.AddRange(AvailableMoves(GS.Coordinate, SquaresLeft - 1, OriginalType, true, new List<Coordinate>()));
            //    }
            //    else
            //    {
            //        if (GS.Coordinate.X == 3 && GS.Coordinate.Y == 2)
            //        {

            //        }
            //        Moves.AddRange(AvailableMoves(GS.Coordinate, SquaresLeft - 1, OriginalType, false, new List<Coordinate>()));
            //    }
            //}
            
            //Moves.Distinct(new LocationComparer());
            return AvailableMoves(C.Location, SquaresLeft, OriginalType);
        }

        /// <summary>
        /// Super Recursive Fun Time method that finds out how many moves are available
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="SquaresLeft"></param>
        /// <param name="OriginalType"></param>
        /// <param name="DiagonalsCostTwo"></param>
        /// <param name="AlreadyChecked"></param>
        /// <returns></returns>
        private List<Coordinate> AvailableMoves(Coordinate Location, int SquaresLeft, Type OriginalType)
        {
            List<Coordinate> ViableMoves = new List<Coordinate>();
            LocationComparer LocComp = new LocationComparer();

            //The GridSquares that will be checked in the next 'step'
            List<Coordinate> NextCoors = new List<Coordinate>();
            //The GridSquares that will be checked in this 'step'
            List<Coordinate> CurrentCoors = new List<Coordinate>();
            CurrentCoors.Add(Location);
            
            //Takes one 'step' in each direction until out of squares
            while (SquaresLeft >= 0)
            {
                foreach (Coordinate C in CurrentCoors)
                {
                    List<Coordinate> Neighbors = new List<Coordinate>();
                    foreach (GridSquare GS in GetAdjacentGridSquares(C))
                    {
                        if (!ViableMoves.Contains(GS.Coordinate, LocComp) && !(NextCoors.Contains(GS.Coordinate, LocComp)))
                        {
                            Neighbors.Add(GS.Coordinate);
                        }
                    }
                    //Removes squares that do not need to be checked
                    //Neighbors = Neighbors.Except(PreviousCoors, new LocationComparer()).ToList();
                    //Neighbors = Neighbors.Except(CurrentCoors, new LocationComparer()).ToList();

                    foreach (Coordinate Neighbor in Neighbors)
                    {
                        if (Neighbor.X == 0 && Neighbor.Y == 0)
                        {

                        }
                        GridSquare CurrentSquare = GetGridSquareAt(Neighbor);

                        //Checks if the GridSquare is a viable move and if it should be added to NextCoors
                        if (CurrentSquare.IsOccupiable && CurrentSquare.OccupyingCharacter == null && SquaresLeft - 1 >= 0)
                        {
                            ViableMoves.Add(Neighbor);
                            NextCoors.Add(Neighbor);
                        }
                        //Allows movement through squares occupied by friendly characters
                        else if (SquaresLeft - 1 >= 0 && ((CurrentSquare.OccupyingCharacter is Player && OriginalType == typeof(Player))
                                || (CurrentSquare.OccupyingCharacter is Zed && OriginalType == typeof(Zed))))
                        {
                            NextCoors.Add(Neighbor);
                        }
                    }
                }
                SquaresLeft--;
                CurrentCoors = NextCoors;
                NextCoors = new List<Coordinate>();
            }

            ViableMoves.Distinct(new LocationComparer());
            return ViableMoves;
            //List<Coordinate> ViableMoves = new List<Coordinate>();
            ////Checks to prevent recursive calling in certain scenarios
            //GridSquare CurrentSquare = GetGridSquareAt(Location);

            //if (!CurrentSquare.IsOccupiable)
            //{
            //    return ViableMoves;
            //}
            
            //if (CurrentSquare.OccupyingCharacter == null)
            //{
            //    ViableMoves.Add(Location);
            //}

            //if (SquaresLeft < 1)
            //{
            //    return ViableMoves;
            //}
            ////Prevents movement through squares occupied by enemy Characters
            //else if ((CurrentSquare.OccupyingCharacter is Zed && OriginalType == typeof(Player))
            //    || (CurrentSquare.OccupyingCharacter is Player && OriginalType == typeof(Zed)))
            //{
            //    return ViableMoves;
            //}

            //List<Coordinate> Neighbors = new List<Coordinate>();
            //foreach(GridSquare GS in GetAdjacentGridSquares(Location))
            //{
            //    Neighbors.Add(GS.Coordinate);
            //}

            //Neighbors = Neighbors.Except(AlreadyChecked, new LocationComparer()).ToList();

            ////AlreadyChecked.AddRange(Neighbors);
            //for (int i = 0; i < Neighbors.Count(); i++)
            //{
            //    Coordinate Neighbor = Neighbors.ElementAt(i);

            //    //if (Neighbor.X == 8 && Neighbor.Y == 2 && Location.X == 7 && Location.Y == 3)
            //    //{
            //    //    //Console.WriteLine("Location is : " + Location);
            //    //    //Console.WriteLine(AlreadyChecked.Contains(Neih);
            //    //    //Console.WriteLine(SquaresLeft);
            //    //}

            //    int MoveCost = 1;

            //    //True if the Movement from this GridSquare to the Neighbor is diagonal
            //    bool DiagonalMovement = Math.Abs(Location.X - Neighbor.X) == Math.Abs(Location.Y - Neighbor.Y);
            //    if (DiagonalMovement)
            //    {
            //        //Doesn't make a diagonal move if it can't make it
            //        if (DiagonalsCostTwo && SquaresLeft > 1)
            //        {
            //            MoveCost++;
            //            ViableMoves.AddRange(AvailableMoves(Neighbor, SquaresLeft - MoveCost, OriginalType, !DiagonalsCostTwo, AlreadyChecked));
            //        }
            //        else if(!DiagonalsCostTwo)
            //        {
            //            ViableMoves.AddRange(AvailableMoves(Neighbor, SquaresLeft - MoveCost, OriginalType, !DiagonalsCostTwo, AlreadyChecked));
            //        }
            //    }
            //    else
            //    {
            //        ViableMoves.AddRange(AvailableMoves(Neighbor, SquaresLeft - MoveCost, OriginalType, DiagonalsCostTwo, AlreadyChecked));
            //    }
                
            //}
            ////AlreadyChecked.Add(Location);

            //return ViableMoves.Distinct(new LocationComparer()).ToList();
        }

        /// <summary>
        /// Checks each of the given Coordinates in CorpseLocations and makes a revive roll for each corpse in the Location. 
        /// Will return a List of the new Zeds that have been created from the revive rolls
        /// </summary>
        /// <param name="CorpseLocations"></param>
        /// <returns></returns>
        public List<Zed> MakeReviveRolls(List<Coordinate> CorpseLocations)
        {

            List<Zed> NewZombies = new List<Zed>();

            List<Coordinate> CoordinatesToRemove = new List<Coordinate>();

            //Goes through each Coordinate in CorpseLocations and attempts to revive the Corpses in each
            foreach (Coordinate C in CorpseLocations)
            {
                GridSquare Target = GetGridSquareAt(C);


                int CorpseIndex = 0; //Finds the first Corpse in Target's ItemList
                int CorpseCount = 0; // Keeps track of the number of Corpses in this GridSquare
                bool Revive = false;
                foreach (Corpse Body in Target.ItemList)
                {
                    if (Body.RollRevive() && !Revive)
                    {
                        Revive = true;
                        CorpseIndex = Target.ItemList.IndexOf(Body);
                    }
                    CorpseCount++;
                }

                //Adds a Zombie and removes a Corpse from the GridSquare if a Zombie has been revived
                if (Revive)
                {
                    if (Target.OccupyingCharacter == null)
                    {
                        Zed Z = ((Corpse)Target.ItemList.ElementAt(CorpseIndex)).SpawnZed();
                        Target.ItemList.RemoveAt(CorpseIndex);
                        AddCharacterToSquare(Z, Target.Coordinate);
                        NewZombies.Add(Z);
                        CorpseCount--;
                    }
                }

                //Removes Coordinate C from the CorpseLocations if Target has no more Corpses in it
                if (CorpseCount < 1)
                {
                    CoordinatesToRemove.Add(C);
                }
            }

            CorpseLocations = CorpseLocations.Except(CoordinatesToRemove, new LocationComparer()).ToList();
            return NewZombies;
        }

        /// <summary>
        /// Checks to see if the occupying Character of the given GridSquare has any adjacent Characters that are friends or enemies.
        /// Will return true the moment a friendly or enemey type is found depending on what is being looked for.  Otherwise, will check all
        /// adjacent squares and if none of the desired type is found will return false.
        /// </summary>
        /// <param name="C"></param>
        /// <param name="friend"></param>
        /// <returns></returns>
        public List<Character> AdjacentCharacters(Character C, bool friend)
        {
            List<Character> Adjacents = new List<Character>();
            try
            {
                GridSquare Target = GetGridSquareAt(C.Location);
                if (Target.OccupyingCharacter == null)
                {
                    return Adjacents;
                }

                int tX = Target.Coordinate.X;
                int tY = Target.Coordinate.Y;

                List<Coordinate> NeighborCoordinates = new List<Coordinate>()
                {
                    new Coordinate(tX - 1, tY - 1),
                    new Coordinate(tX, tY - 1),
                    new Coordinate(tX - 1, tY),
                    new Coordinate(tX + 1, tY + 1),
                    new Coordinate(tX, tY + 1),
                    new Coordinate(tX + 1, tY),
                    new Coordinate(tX - 1, tY + 1),
                    new Coordinate(tX + 1, tY - 1)
                };

                for (int i = 0; i < NeighborCoordinates.Count(); i++)
                {
                    Coordinate CurrentCoordinate = NeighborCoordinates.ElementAt(i);
                    if (CurrentCoordinate.X >= 0 && CurrentCoordinate.X < Width
                        && CurrentCoordinate.Y >= 0 && CurrentCoordinate.Y < Height)
                    {
                        Character Neighbor = GetGridSquareAt(CurrentCoordinate).OccupyingCharacter;
                        if (Neighbor != null)
                        {
                            if (friend)
                            {
                                if (C is Player)
                                {
                                    if (!(Neighbor is Zed))
                                    {
                                        Adjacents.Add(Neighbor);
                                    }
                                }
                                else
                                {
                                    if (Neighbor is Zed)
                                    {
                                        Adjacents.Add(Neighbor);
                                    }
                                }
                            }
                            else
                            {
                                if (C is Player)
                                {
                                    if (!(Neighbor is Zed))
                                    {
                                        Adjacents.Add(Neighbor);
                                    }
                                }
                                else
                                {
                                    if (Neighbor is Player)
                                    {
                                        Adjacents.Add(Neighbor);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex);
            }

            return Adjacents;
        }

        /// <summary>
        /// Returns a List of Characters from Targets that Character C has line of sight to.  
        /// If none of the Targets are withing C's line of sight will return an empty list.
        /// </summary>
        /// <param name="C"></param>
        /// <param name="Targets"></param>
        /// <returns></returns>
        public List<Character> PossibleRangedTargets(Character C, List<Character> Targets)
        {

            List<Character> LegalTargets = new List<Character>();
            Coordinate BeginCoor = new Coordinate(C.Location.X, C.Location.Y);

            foreach (Character Target in Targets)
            {
                //Sets up the two points to begin the algorithm
                Coordinate EndCoor = new Coordinate(Target.Location.X, Target.Location.Y);

                if (HasLineOfSight(BeginCoor, EndCoor))
                {
                    LegalTargets.Add(Target);
                }
            }

            return LegalTargets;
        }

        /// <summary>
        /// Goes through the line of sight that connects the two given Coordinates.  
        /// Returns false if at any time a GridSquare is found that is Closed or Occupied.  
        /// Returns true if no GridSquare is found to be Closed or Occupied.
        /// </summary>
        /// <param name="BeginCoor"></param>
        /// <param name="EndCoor"></param>
        /// <returns></returns>
        private bool HasLineOfSight(Coordinate BeginCoor, Coordinate EndCoor)
        {
            int x0 = BeginCoor.Y;
            int y0 = BeginCoor.X;
            int x1 = EndCoor.Y;
            int y1 = EndCoor.X;
            ////Finds out if the absolute value of the slope is 0 or 1
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            //Massage the data to get it how we want it
            if (steep)
            {
                int Temp = y0;
                y0 = x0;
                x0 = Temp;
                Temp = y1;
                y1 = x1;
                x1 = Temp;
            }

            if (x0 > x1)
            {
                int Temp = x0;
                x0 = x1;
                x1 = Temp;
                Temp = y0;
                y0 = y1;
                y1 = Temp;
            }

            //Prepares variables needed for path
            int deltax = x1 - x0;
            int deltay = Math.Abs(y1 - y0);
            int error = deltax / 2;
            int yIncrement;
            if (y0 < y1)
            {
                yIncrement = 1;
            }
            else
            {
                yIncrement = -1;
            }
            int y = y0;

            LocationComparer CoorCompare = new LocationComparer();

            //Iterates over every GridSquare in the line of sight to see if it is Closed or Occupied
            for (int x = x0 + 1; x < x1; x++)
            {
                error -= deltay;
                if (error < 0)
                {
                    y += yIncrement;
                    error += deltax;
                }
                Coordinate NextCoor;
                if (steep)
                {
                    NextCoor = new Coordinate(x, y);
                }
                else
                {
                    NextCoor = new Coordinate(y, x);
                }

                try
                {
                    GridSquare Examine = GetGridSquareAt(NextCoor);
                    if (!Examine.IsOccupiable || Examine.OccupyingCharacter != null)
                    {
                        return false;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    //Do nothing
                }
            }
            return true;
        }

        /// <summary>
        /// Finds the cost of of the shortest path from the player's location to their destination
        /// </summary>
        /// <param name="CurrentPlayer"></param>
        /// <param name="Destination"></param>
        /// <returns></returns>
        public int ShortestPathCost(Character CurrentPlayer, Coordinate Destination)
        {
            Coordinate Location = CurrentPlayer.Location;
            return PathCost(Location, Destination);            
        }

        public int PathCost(Coordinate Location, Coordinate Destination)
        {
            return (int)(Math.Pow(Location.X - Destination.X, 2) + Math.Pow(Location.Y - Destination.Y, 2));
        }
        #endregion

        /// <summary>
        /// Resets the Board so that it contains only new squares that are empty and will be arranged in a number 
        /// according to Height and Width
        /// </summary>
        public void ClearBoard()
        {
            GridSquares = new GridSquare[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    GridSquares[i, j] = new GridSquare(i, j);
                }
            }
        }

        /// <summary>
        /// Returns a string that will represent the board as a series of the string representations of the GridSquares
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            string ReturnValue = "   0 1 2 3 4 5 6 7 8 9\n";

            for (int i = 0; i < Height; i++)
            {
                if(i < 10)
                {
                    ReturnValue += i + " |";                   
                }
                else
                {
                    ReturnValue += i + "|";
                }
                for (int j = 0; j < Width; j++)
                {
                    ReturnValue += GridSquares[j, i].ToString() + "|";
                }
                ReturnValue += "\n";
            }
            return ReturnValue;
        }
    }
}