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
			PhysicalStrength = DieRoll.RollOne(6) + 18;
            PhysicalProwess = DieRoll.RollOne(6) + 9;
            PhysicalEndurance = DieRoll.RollOne(6) + 15;
            Speed = DieRoll.RollOne(9) + 11;
			MaxSDC = rollsdc();
			sdc = MaxSDC;
			MaxHealth = rollHP();
			Health = MaxHealth;
		}

		public override int MeleeAttack()
		{
			DieRoll DamageRoll = new DieRoll (2, 6, NewModifyer: base.bonusPS());
			return DamageRoll.Roll();
		}

		public override int toHitMelee()
		{
			return base.toHitMelee() + 3;
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
            return DieRoll.RollOne(6) + 14;
		}

		public override int rollsdc()
		{
            return DieRoll.RollOne(11) + 19;
		}


    }
}
