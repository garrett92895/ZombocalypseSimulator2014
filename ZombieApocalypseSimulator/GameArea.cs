using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator
{
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
            Width = NewHeight;
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
            return ViableSquares.ElementAt(Rand.Next(ViableSquares.Count - 1)).Coordinate;
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
                for (int i = 0; i < NeighborCoordinates.Count(); i++ )
                {
                    Coordinate CurrentCoordinate = NeighborCoordinates.ElementAt(i);
                    if(CurrentCoordinate.X >= 0 && CurrentCoordinate.X < Width
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
        /// Changes the GridSquare that the Character knows they occupy and the OccupyingCharacter of the Target GridSquare.
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
                Console.WriteLine(ex); ;
            }
        }

        public void KillCharacter(Character C)
        {
            if (C.GetType() == typeof(Player))
            {
                GetItemsInSquare(C.Location).Add(new Corpse((Player)C));
            }

            GetGridSquareAt(C.Location).OccupyingCharacter = null;
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
            Type OriginalType = null;
            if(C.GetType() == typeof(Player))
            {
                OriginalType = typeof(Player);
            }
            else
            {
                OriginalType = typeof(Zed);
            }
            return AvailableMoves(C.Location, SquaresLeft, OriginalType, DiagonalsCostTwo, new List<Coordinate>());
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
        private List<Coordinate> AvailableMoves(Coordinate Location, int SquaresLeft, Type OriginalType, bool DiagonalsCostTwo, List<Coordinate> AlreadyChecked)
        {
            List<Coordinate> ViableMoves = new List<Coordinate>();

            //Checks to prevent recursive calling in certain scenarios
            GridSquare CurrentSquare = GetGridSquareAt(Location);

            if (!CurrentSquare.IsOccupiable)
            {
                return ViableMoves;
            }

            //Makes sure not to move give one extra Diagonal movement
            if (DiagonalsCostTwo && SquaresLeft < 0)
            {
                return ViableMoves;
            }
            if (CurrentSquare.OccupyingCharacter == null)
            {
                ViableMoves.Add(Location);
            }
            //Prevents movement through squares occupied by enemy Characters
            else if ((CurrentSquare.OccupyingCharacter.GetType() != typeof(Player) && OriginalType == typeof(Player))
                || (CurrentSquare.OccupyingCharacter.GetType() == typeof(Player) && OriginalType != typeof(Player)))
            {
                return ViableMoves;
            }

            if (SquaresLeft < 1)
            {
                return ViableMoves;
            }

            List<Coordinate> Neighbors = new List<Coordinate>();
            foreach(GridSquare Square in GetAdjacentGridSquares(Location))
            {
                Neighbors.Add(Square.Coordinate);
            }

            //Removes Coordinates that have already been checked
            Neighbors = Neighbors.Except(AlreadyChecked, new LocationComparer()).ToList();
            AlreadyChecked.AddRange(Neighbors);
            for (int i = 0; i < Neighbors.Count(); i++)
            {
                Coordinate Neighbor = Neighbors.ElementAt(i);
                //Cost of the movement
                int MoveCost = 1;

                //True if the Movement from this GridSquare to the Neighbor is diagonal
                bool DiagonalMovement = Math.Abs(Location.X - Neighbor.X) == Math.Abs(Location.Y - Neighbor.Y);
                if (DiagonalMovement && DiagonalsCostTwo)
                {
                    MoveCost++;
                }
                ViableMoves.AddRange(AvailableMoves(Neighbor, SquaresLeft - MoveCost, OriginalType, !DiagonalsCostTwo, AlreadyChecked));
            }
            return ViableMoves.Distinct(new LocationComparer()).ToList();
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
                                if (C.GetType() == typeof(Player))
                                {
                                    if (Neighbor.GetType() == typeof(Player))
                                    {
                                        Adjacents.Add(Neighbor);
                                    }
                                }
                                else
                                {
                                    if (Neighbor.GetType() == typeof(Zed))
                                    {
                                        Adjacents.Add(Neighbor);
                                    }
                                }
                            }
                            else
                            {
                                if (C.GetType() == typeof(Player))
                                {
                                    if (Neighbor.GetType() == typeof(Zed))
                                    {
                                        Adjacents.Add(Neighbor);
                                    }
                                }
                                else
                                {
                                    if (Neighbor.GetType() == typeof(Player))
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

            foreach (Character Target in Targets)
            {
                //Sets up the two points to begin the algorithm
                Coordinate BeginCoor = new Coordinate(C.Location.X, C.Location.Y);
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
            //Finds out if the absolute value of the slope is 0 or 1
            bool steep = Math.Abs(EndCoor.Y - BeginCoor.Y) > Math.Abs(EndCoor.X - BeginCoor.X);

            //Massage the data to get it how we want it
            if (steep)
            {
                int Temp = BeginCoor.Y;
                BeginCoor.Y = BeginCoor.X;
                BeginCoor.X = BeginCoor.Y;
                Temp = EndCoor.Y;
                EndCoor.Y = EndCoor.X;
                EndCoor.X = Temp;
            }
            if (BeginCoor.X > EndCoor.X)
            {
                Coordinate Temp = BeginCoor;
                BeginCoor = EndCoor;
                EndCoor = Temp;
            }

            //Prepares variables needed for path
            int deltax = EndCoor.X - BeginCoor.X;
            int deltay = Math.Abs(EndCoor.Y - BeginCoor.Y);
            int error = deltax / 2;
            int yIncrement;
            if (BeginCoor.Y < EndCoor.Y)
            {
                yIncrement = 1;
            }
            else
            {
                yIncrement = -1;
            }
            int y = BeginCoor.Y;

            //Iterates over every GridSquare in the line of sight to see if it is Closes or Occupied
            for (int x = BeginCoor.X; x <= EndCoor.X; x++)
            {
                Coordinate NextCoor;
                if (steep)
                {
                    NextCoor = new Coordinate(y, x);
                }
                else
                {
                    NextCoor = new Coordinate(x, y);
                }
                GridSquare Examine = GetGridSquareAt(NextCoor);
                if (!Examine.IsOccupiable || Examine.OccupyingCharacter != null)
                {
                    return false;
                }
                error -= deltay;
                if (error < 0)
                {
                    y += yIncrement;
                    error += deltax;
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
            return (int)(Math.Pow(Location.X - Destination.X, 2) + Math.Pow(Location.Y - Destination.Y, 2));
        }
        #endregion

        /// <summary>
        /// Resets the Board so that it contains only new squares that are empty and will be arranged in a number 
        /// according to Height and Width
        /// </summary>
        public void ClearBoard()
        {
            GridSquares = new GridSquare[Height, Width];

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

            string ReturnValue = "";

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    ReturnValue += GridSquares[i, j].ToString() + "|";
                }
                ReturnValue += "\n";
            }
            return ReturnValue;
        }
    }
}