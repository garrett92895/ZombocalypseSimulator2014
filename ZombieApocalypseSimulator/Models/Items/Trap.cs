using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
    [Serializable()]
	public class Trap : Item
	{
        private DieRoll _Damage;
		public DieRoll Damage 
        {
            get { return _Damage; }
            set
            {
                _Damage = value;
                DetermineValue();
            }
        }

        private StatusEffect _StatusEffect;
		public StatusEffect StatusEffect 
        {
            get { return _StatusEffect; }
            set
            {
                _StatusEffect = value;
                DetermineValue();
            }
        }

        private void DetermineValue()
        {
            if (Damage != null)
            {
                //Sets the base value to be based on the traps damage
                Value = Damage.NumberOfDice * ((Damage.SidesPerDie / 2)) + 1;

                //Adds price based on any multipliers or modifyers in the Damage Dice
                Value = (Value * Damage.Multiplier) + (Damage.Modifyer * 5);

                //Adds half the base value if the trap inflicts a Status Effect
                Value += ((StatusEffect < 0) ? 0 : Value / 2);
            }
        }
	}
}
