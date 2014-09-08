using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Items
{
    [Serializable()]
	public class SparePart : Item
	{
        private DieRoll _AmountHealed;
		public DieRoll AmountHealed 
        {
            get { return _AmountHealed; }
            set
            {
                _AmountHealed = value;
                DetermineValue();
            }
        }

        private void DetermineValue()
        {
            if (AmountHealed != null)
            {
                //Sets the base value to be based on Spare Parts repair dice
                Value = AmountHealed.NumberOfDice * ((AmountHealed.SidesPerDie / 2)) + 1;

                //Prevents Repairing from being very expensive
                Value /= 5;

                //Adds price based on any multipliers or modifyers in the AmountHealded Dice
                Value = (Value * AmountHealed.Multiplier) + (AmountHealed.Modifyer * 5);
            }
        }
	}
}
