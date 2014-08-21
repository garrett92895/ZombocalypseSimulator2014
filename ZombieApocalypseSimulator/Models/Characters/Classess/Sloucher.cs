using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classess
{
    class Sloucher: Zed
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
			PhysicalStrength = rand.Next(11) + 20;
			PhysicalProwess = rand.Next(6) + 2;
			PhysicalEndurance = rand.Next(6) + 16;
			Speed = rand.Next(4) + 7;
			MaxHealth = rollHP();
			Health = MaxHealth;
			MaxSDC = rollsdc();
			sdc = MaxSDC;
        }

		public override int toHitMelee()
		{
			DieRoll Die = new DieRoll(1, 6);
			return Die.Roll() + base.bonusPS();
		}

        public override int toParry()
        {
            DieRoll Die = new DieRoll(1, 20);
            int hit = (int)(Die.Roll());
            return hit;
        }
		public override int MeleeAttack()
		{
			DieRoll Die = new DieRoll(1, 6);
			return Die.Roll() + base.bonusPS();
		}

		public override int rollHP()
		{
			return rand.Next(6) + 16;
		}

		public override int rollsdc()
		{
			return rand.Next(16) + 33;
		}
    }
}
