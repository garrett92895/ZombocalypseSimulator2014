using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;

namespace ZombieApocalypseSimulator.Models.Characters.Classess
{
    class Tank:Zed
    {
        public Tank()
        {
			PhysicalStrength = rand.Next (16) + 30;
			PhysicalProwess = rand.Next (6) + 2;
			PhysicalEndurance = rand.Next (6) + 16;
            Speed = rand.Next(4) + 7;
			MaxSDC = rollsdc();
			sdc = MaxSDC;
			MaxHealth = rollHP();
			Health = MaxHealth;
            ArmorRating = 14;
            CanParry = true;
            CanDodge = true;
        }

        public override int MeleeAttack()
        {
			DieRoll Die = new DieRoll (3, 6);
			return Die.Roll() + base.bonusPS();
        }

        public override int toHitMelee()
        {
			int hit = base.toHitMelee() + 2;
            return hit;
        }

        public override int toParry()
        {
			return base.toParry() + 2;
        }

        public override int toDodge()
        {
			return base.toDodge() + 1;
        }

        public override int rollHP()
        {
			return rand.Next(16) + 35;
        }

        public override int rollsdc()
        {
			return rand.Next(21) + 60;
        }


    }
}
