using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Items
{
    [Serializable()]
	public class Health : Item
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

        public Health(int NumberOfDice = 1, int SidesPerDie = 6)
        {
            AmountHealed = new DieRoll(NumberOfDice, SidesPerDie);
            Value = NumberOfDice * ((SidesPerDie) / 2) + 1;
            Name = "Health Pack: " + AmountHealed.NumberOfDice + "d" + AmountHealed.SidesPerDie;
        }

        private void DetermineValue()
        {
            //Weapon only has a value if it has a damage value
            if (AmountHealed != null)
            {
                //Sets the base value to be based on health packs healing dice
                Value = AmountHealed.NumberOfDice * ((AmountHealed.SidesPerDie / 2)) + 1;

                //Adds price based on any multipliers or modifyers in the AmountHealded Dice
                Value = (Value * AmountHealed.Multiplier) + (AmountHealed.Modifyer * 5);
            }
        }
	}
}
