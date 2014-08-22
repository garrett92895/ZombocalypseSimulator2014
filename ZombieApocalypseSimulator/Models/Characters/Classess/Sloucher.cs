using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classess
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
            return DieRoll.RollOne(6) + base.bonusPS();
		}

        public override int toParry()
        {
            return DieRoll.RollOne(20) + base.bonusPP();
        }
		public override int MeleeAttack()
		{
            return DieRoll.RollOne(6) + bonusPS();
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
