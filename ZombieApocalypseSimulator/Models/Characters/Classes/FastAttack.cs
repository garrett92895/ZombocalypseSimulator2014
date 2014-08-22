using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class FastAttack: Zed
    {
       public FastAttack()
       {

            PhysicalStrength = DieRoll.RollOne(6) + 18;
            PhysicalProwess = DieRoll.RollOne(6) + 7;
            PhysicalEndurance = DieRoll.RollOne(6) + 15;
            Speed = DieRoll.RollOne(7) + 16;
			MaxSDC = rollsdc();
			sdc = MaxSDC;
			MaxHealth = rollHP();
			Health = MaxHealth;
        }

        public override int MeleeAttack()
        {
            DieRoll Die = new DieRoll(1, 6);
			return Die.Roll() + 3 + base.bonusPS();
        }

        public override int toHitMelee()
        {
            return base.toHitMelee() + 2;
        }

        public override int toParry()
        {
			return base.toParry() + 2;
        }

        public override int toDodge()
        {
			return base.toDodge() + 2;
        }

        public override int rollHP()
        {
            //Ensures the DieRoll will return a value between 35-50
            return DieRoll.RollOne(6) + 5;
        }

        public override int rollsdc()
        {
            //Ensures the DieRoll will return a value between 60-80
            return DieRoll.RollOne(16) + 32;
        }

    }
}
