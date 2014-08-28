using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;


namespace ZombieApocalypseSimulator.Models.Characters
{
    public abstract class Zed : Character
    {
        public bool HasAttacked { get; set; }
        public Zed()
        {
            IntelligenceQuotient = 1;
            MentalAffinity = 1;
            MentalEndurance = 1;
            ArmorRating = 14;
        }

        public override bool HasWeapon()
        {
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}