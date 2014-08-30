using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Enums;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public abstract class Weapon : Item
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

		public bool IsEquiped { get; set; }

        private int _Condition;
		public int Condition 
        {
            get { return _Condition; } 
            set
            {
                _Condition = value;
                DetermineValue();
            }
        }

        private bool _IgnoresAR;
		public bool IgnoresAR 
        { 
            get { return _IgnoresAR; }
            set
            {
                _IgnoresAR = value;
                DetermineValue();
            }
        }

        private StatusEffect _Effect;
        public StatusEffect Effect 
        { 
            get { return _Effect; }
            set
            {
                _Effect = value;
                DetermineValue();
            }
        }

        public virtual int UseWeapon()
        {
            DieRoll ConditionDie = new DieRoll(2, 6);
            Condition -= ConditionDie.Roll();
            return Damage.Roll();
        }

        public override string ToString()
        {
            return base.ToString() + "\n Current Condition:" + Condition;
        }

        /// <summary>
        /// Determines the Value of this weapon based its Damage, Condition, IgnoresAR, and it Effect
        /// </summary>
        private void DetermineValue()
        {
            //Weapon only has a value if it has a damage value
            if (Damage != null)
            {
                //Sets the base value to be based on the weapons damage
                Value = Damage.NumberOfDice * ((Damage.SidesPerDie / 2)) + 1;

                //Adds half the base value if the weapon IgnoresAr or has a Status Effect, if both will double the base value
                Value += ((IgnoresAR) ? Value / 2 : 0) + ((Effect < 0) ? 0 : Value / 2);

                //The weapon's value is a percentage of its normal value based on its Condition
                Value = (int) (Value * (Condition / 100.0));
            }

        }

	}
}
