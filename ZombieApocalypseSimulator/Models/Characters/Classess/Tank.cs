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
        Random ran = new Random();
        private byte minPhysicalStrength = 30;
        private byte maxPhysicalStrength = 46;
        private byte minPhysicalProwess = 2;
        private int maxPhysicalProwess = 8;
        private int minSpeed = 7;
        private int maxSpeed = 11;

        public Tank()
        {
            IntelligenceQuotient = 1;
            MentalEndurance = 1;
            MentalAffinity = 1;
            PhysicalStrength = ran.Next(minPhysicalStrength, maxPhysicalStrength);
            PhysicalEndurance = (byte)(ran.Next(16, 22));
            PhysicalProwess = ran.Next(minPhysicalProwess, maxPhysicalProwess);
            
            Speed = ran.Next(minSpeed, maxSpeed);
            base.SetLife();
            ArmorRating = 14;
            CanParry = true;
            CanDodge = true;
        }

        public override int meleeattack()
        {
            byte dam = (byte)Dice.Roll(3, 6);
            return dam;
        }

        public override int toHitMelee()
        {
            int hit = (Dice.Roll(1, 20) + 2) + bonusPP();
            return hit;
        }

        public override int toParry()
        {
            int parry = (Dice.Roll(1, 20) + 2) + bonusPP();
            return parry;
        }

        public override int toDodge()
        {
            int dodge = (Dice.Roll(1, 20) + 1) + bonusPP();
            return dodge;
        }

        public override int rollHP()
        {
            byte health = (byte)ran.Next(35, 51);
            return health;
        }

        public override int rollsdc()
        {
            byte armor = (byte)ran.Next(60, 81);
            return armor;
        }


    }
}
