﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classess
{
    class Shank: Zed
    {
        
        //        public ZedTypes ZombieType { get; set; }
        //        private int minPhysicalStrength()
        //        {
        //            int psMin = 0;
        //            if (ZombieType == ZedTypes.Sloucher)
        //            {
        //                psMin = 11;
        //            }
        //            else if (ZombieType == ZedTypes.FastAttack || ZombieType == ZedTypes.Shank)
        //            {
        //                psMin = 19;
        //            }
        //            else if (ZombieType == ZedTypes.Tank)
        //            {
        //                psMin = 30;
        //            }

        //            return psMin;
        //        }

        //        private int maxPhysicalStrength()
        //        {
        //            int psMax = 0;
        //            if (ZombieType == ZedTypes.Sloucher)
        //            {
        //                psMax = 31;
        //            }
        //            else if (ZombieType == ZedTypes.FastAttack || ZombieType == ZedTypes.Shank)
        //            {
        //                psMax = 25;
        //            }
        //            else if (ZombieType == ZedTypes.Tank)
        //            {
        //                psMax = 46;
        //            }

        //            return psMax;
        //        }

        //        private int minPhysicalProwess()
        //        {
        //            int ppMin = 0;
        //            if (ZombieType == ZedTypes.Sloucher || ZombieType == ZedTypes.Tank)
        //            {
        //                ppMin = 2;
        //            }
        //            else if (ZombieType == ZedTypes.FastAttack)
        //            {
        //                ppMin = 8;
        //            }
        //            else if (ZombieType == ZedTypes.Shank)
        //            {
        //                ppMin = 10;
        //            }

        //            return ppMin;
        //        }

        //        private int maxPhysicalProwess()
        //        {
        //            int ppMax = 0;
        //            if (ZombieType == ZedTypes.Sloucher || ZombieType == ZedTypes.Tank)
        //            {
        //                ppMax = 8;
        //            }
        //            else if (ZombieType == ZedTypes.FastAttack)
        //            {
        //                ppMax = 14;
        //            }
        //            else if (ZombieType == ZedTypes.Shank)
        //            {
        //                ppMax = 16;
        //            }

        //            return ppMax;
        //        }

        //        private int minSpeed()
        //        {
        //            int speedMin = 0;
        //            if (ZombieType == ZedTypes.Sloucher || ZombieType == ZedTypes.Tank)
        //            {
        //                speedMin = 7;
        //            }
        //            else if (ZombieType == ZedTypes.FastAttack)
        //            {
        //                speedMin = 17;
        //            }
        //            else if (ZombieType == ZedTypes.Shank)
        //            {
        //                speedMin = 12;
        //            }

        //            return speedMin;
        //        }

        //        private int maxSpeed()
        //        {
        //            int speedMax = 0;
        //            if (ZombieType == ZedTypes.Sloucher || ZombieType == ZedTypes.Tank)
        //            {
        //                speedMax = 11;
        //            }
        //            else if (ZombieType == ZedTypes.FastAttack)
        //            {
        //                speedMax = 24;
        //            }
        //            else if (ZombieType == ZedTypes.Shank)
        //            {
        //                speedMax = 21;
        //            }

        //            return speedMax;
        //        }

        //        public Zed(ZedTypes e)
        //        {
        //            ZombieType = e;
        //            IntelligenceQuotient = 1;
        //            MentalEndurance = 1;
        //            MentalAffinity = 1;
        //            PhysicalStrength = (byte)(ran.Next(minPhysicalStrength(), maxPhysicalStrength()));
        //            PhysicalEndurance = (byte)(ran.Next(16, 22));
        //            PhysicalProwess = (byte)(ran.Next(minPhysicalProwess(), maxPhysicalProwess()));
        //            PhysicalBeauty = 1;
        //            Speed = (byte)(ran.Next(minSpeed(), maxSpeed()));
        //            base.SetLife();
        //            ArmorRating = 14;
        //            CanParry = true;
        //            CanDodge = true;
        //        }

        //        public override int MeleeAttack()
        //        {
        //            byte dam = 0;
        //            if (ZombieType == ZedTypes.Sloucher || ZombieType == ZedTypes.FastAttack)
        //            {
        //                dam = (byte)Dice.Roll(1, 6);
        //            }
        //            else if (ZombieType == ZedTypes.Tank)
        //            {
        //                dam = (byte)Dice.Roll(3, 6);
        //            }
        //            else if (ZombieType == ZedTypes.Shank)
        //            {
        //                dam = (byte)Dice.Roll(2, 6);
        //            }
        //            dam += (byte)bonusPS();
        //            return dam;
        //        }

        //        public override int toHitMelee()
        //        {
        //            int hit = 0;
        //            if (ZombieType != ZedTypes.Shank)
        //            {
        //                hit = (Dice.Roll(1, 20) + 2);
        //            }
        //            else if (ZombieType == ZedTypes.Shank)
        //            {
        //                hit = (Dice.Roll(1, 20) + 3);
        //            }
        //            hit += bonusPP();
        //            return hit;
        //        }

        //        public override int toParry()
        //        {
        //            int parry = 0;
        //            if(ZombieType == ZedTypes.Sloucher)
        //            {
        //                parry = (Dice.Roll(1, 20));
        //            }
        //            else if(ZombieType == ZedTypes.FastAttack || ZombieType == ZedTypes.Tank)
        //            {
        //                parry = (Dice.Roll(1, 20) + 2);
        //            }
        //            else if(ZombieType == ZedTypes.Shank)
        //            {
        //                parry = (Dice.Roll(1, 20) + 3);
        //            }
        //            parry += bonusPP();
        //            return parry;
        //        }

        //        public override int toDodge()
        //        {
        //            int dodge = 0;
        //            if (ZombieType == ZedTypes.Tank)
        //            {
        //                dodge = (Dice.Roll(1, 20) + 1);
        //            }
        //            else if (ZombieType == ZedTypes.FastAttack)
        //            {
        //                dodge = (Dice.Roll(1, 20) + 2);
        //            }
        //            else if (ZombieType == ZedTypes.Shank)
        //            {
        //                dodge = (Dice.Roll(1, 20) + 3);
        //            }
        //            dodge += bonusPP();
        //            return dodge;
        //        }

//        public override int toDodgeRangedAttack()
//        {
//           return toDodge() - 6;
//        }
		public override int MeleeAttack()
		{
			return 0;
		}
    }
}
