using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Sloucher: Zed
    {
        public override bool CanDodge 
        { 
            get
            {
                return false;
            }
            set
            {
                _CanDodge = false;
            }
        }
        public override bool HasDodged
        {
            get
            {
                return false;
            }
            set
            {
                base.HasDodged = false;
            }
        }

        public Sloucher()
        {
            PhysicalStrength = DieRoll.RollOne(11) + 19;
            PhysicalProwess = DieRoll.RollOne(6) + 1;
            PhysicalEndurance = DieRoll.RollOne(6) + 15;
            Speed = DieRoll.RollOne(4) + 6;
			MaxHealth = rollHP();
			Health = MaxHealth;
			MaxSDC = rollsdc();
			sdc = MaxSDC;
        }

		public override int toHitMelee()
		{
            return base.toHitMelee() + 2;
		}

        public override int toParry()
        {
            return DieRoll.RollOne(20);
        }
		public override int MeleeAttack()
		{
            DieRoll Die = new DieRoll(1, 6);
            return Die.Roll() + base.bonusPS();
		}

		public override int rollHP()
		{
            return DieRoll.RollOne(6) + 15;
		}

		public override int rollsdc()
		{
            return DieRoll.RollOne(16) + 32;
		}
    }
}
