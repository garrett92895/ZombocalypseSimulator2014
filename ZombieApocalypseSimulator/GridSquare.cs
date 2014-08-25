using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator
{
    public enum Direction { UP, UP_RIGHT, RIGHT, DOWN_RIGHT, DOWN, DOWN_LEFT, LEFT, UP_LEFT };

    public class GridSquare : INotifyPropertyChanged
    {
        private Coordinate _Coordinate;
        /// <summary>
        /// The Coordinate that tracks the location this GridSquare represents
        /// </summary>
        public Coordinate Coordinate 
        {
            get { return _Coordinate; }
            set
            {
                _Coordinate = value;
                NotifyPropertyChanged("Coordinate");
            }
        }

        /// <summary>
        /// Constructor for GridSquare which requires a location in the form of an x and a y position.
        /// Also contains other parametaters that can allow a GridSquare to be created with an OccupyingCharacter, 
        /// an ItemList, an ActiveTrap, and a state of if the GridSquare is allowed to be occupied.
        /// The default values for the optional paramaters will create the GridSquare being completely empty, with 
        /// no OccupyingCharacter, no Items located in the GridSquare, and be occupiable
        /// </summary>
        /// <param name="NewX"></param>
        /// <param name="NewY"></param>
        /// <param name="NewCharacter"></param>
        /// <param name="NewItemList"></param>
        /// <param name="NewTrap"></param>
        /// <param name="NewOccupiable"></param>
        public GridSquare(int NewX, int NewY, Character NewCharacter = null, List<Item> NewItemList = null,
            Trap NewTrap = null, bool NewOccupiable = true)
        {
            Coordinate = new Coordinate(NewX, NewY);
            IsOccupiable = NewOccupiable;
            OccupyingCharacter = NewCharacter;
            if (NewItemList == null)
            {
                NewItemList = new List<Item>();
            }
            ItemList = NewItemList;
            ActiveTrap = NewTrap;
        }

        private Character _OccupyingCharacter;
        /// <summary>
        /// Stores a reference to a Character that occupies this GridSquare.
        /// Will be null if no Character is currently occupying this GridSquare.
        /// If this GridSquare is closed, meaning that the IsOccupiable property is set to false, then OccupyingCharacter will be reset to null.
        /// Will not allow the reference to be changed if this GridSquare is not occupiable.
        /// </summary>
        public Character OccupyingCharacter
        {
            get { return _OccupyingCharacter; }
            set
            {
                _OccupyingCharacter = value;
                NotifyPropertyChanged("OccupyingCharacter");
            }
        }

        private List<Item> _ItemList;
        /// <summary>
        /// Stores all of the Items that are located in this GridSquare in a List.
        /// If no items are located in this GridSquare than this will be an empty List, this should never be null.
        /// If this GridSquare is closed, meaning that the IsOccupiable property is set to false, then the ItemList will be reset to an empty ItemList.
        /// Will not allow for the ItemList to be set to a new List if this GridSquare is not occupiable.
        /// </summary>
        public List<Item> ItemList
        {
            get
            { return _ItemList; }
            set
            {
                _ItemList = value;
                NotifyPropertyChanged("ItemList");
            }
        }

        private Trap _ActiveTrap;
        /// <summary>
        /// Stores any Trap that has been set in this GridSquare as being active.  This is a Trap that is set and is waiting to be triggered by a specific scenario.
        /// Traps that are not going to be triggered from a scenario occuring are stored in the ItemList
        /// </summary>
        public Trap ActiveTrap
        {
            get { return _ActiveTrap; }
            set
            {
                if (IsOccupiable)
                {
                    _ActiveTrap = value;
                    NotifyPropertyChanged("ActiveTrap");
                }
            }
        }
        private bool _IsOccupiable;
        /// <summary>
        /// Determines if the GridSquare is 'closed' meaning that it cannot have a Character Occupying it or any items located inside it.
        /// To facilitate this wil set the OccupyingCharater to null and the ItemList to a new List of Items whenever this bool is set to false.
        /// </summary>
        public bool IsOccupiable
        {
            get { return _IsOccupiable; }
            set
            {
                _IsOccupiable = value;
                NotifyPropertyChanged("IsOccupiable");
            }
        }

        /// <summary>
        /// Returns a single character string based on the occupants of this GridSquare
        /// Returns C if the GridSquare is closed
        /// Returns P if the GridSquare has a Player Occupant
        /// Returns Z if the GridSquare has a Zed Occupant
        /// Returns I if the GridSquare does not have an Occupant but does have at least one Item Located in it
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!IsOccupiable)
            {
                return "C";
            }

            if (OccupyingCharacter is Player)
            {
                return "P";
            }

            else if (OccupyingCharacter is Sloucher)
            {
                return "s";
            }
            else if(OccupyingCharacter is Tank)
            {
                return "T";
            }
            else if (OccupyingCharacter is FastAttack)
            {
                return "F";
            }
            else if (OccupyingCharacter is Shank)
            {
                return "S";
            }

            if (ItemList.Count > 0)
            {
                return "I";
            }

            return " ";

        }

        /// <summary>
        /// Notifies any events in PropertyChanged that a specific property has been changed
        /// </summary>
        /// <param name="Info"></param>
        private void NotifyPropertyChanged(String Info)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
