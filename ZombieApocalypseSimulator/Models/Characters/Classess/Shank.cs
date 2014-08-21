using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classess
{
    class Shank: Zed
    {
		public Shank()
		{
			PhysicalStrength = rand.Next (6) + 19;
			PhysicalProwess = rand.Next (6) + 10;
			PhysicalEndurance = rand.Next (6) + 16;
			Speed = rand.Next(9) + 12;
			MaxSDC = rollsdc();
			sdc = MaxSDC;
			MaxHealth = rollHP();
			Health = MaxHealth;
		}

		public override int MeleeAttack()
		{
			DieRoll Die = new DieRoll (2, 6);
			return Die.Roll() + base.bonusPS();
		}

		public override int toHitMelee()
		{
			int hit = base.toHitMelee() + 3;
			return hit;
		}

		public override int toParry()
		{
			return base.toParry() + 3;
		}

		public override int toDodge()
		{
			return base.toDodge() + 3;
		}

		public override int rollHP()
		{
			return rand.Next(6) + 15;
		}

		public override int rollsdc()
		{
			return rand.Next(11) + 20;
		}


    }
}
