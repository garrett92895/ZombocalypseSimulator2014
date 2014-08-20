using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;
using ZombieApocalypseSimulator.Models.Characters;


namespace ZombieApocalypseSimulator.Models.Characters
{
    public abstract class Zed : Character
    {
        public Zed()
        {
            IntelligenceQuotient = 1;
            MentalEndurance = 1;
            MentalAffinity = 1;
            PhysicalStrength = 0;
            PhysicalEndurance = 0;
            PhysicalProwess = 0;
            PhysicalBeauty = 1;
            Speed = 0;
            base.SetLife();
            ArmorRating = 14;
            CanParry = true;
            CanDodge = true;
        }
        public override string ToString()
        {
            string s = "";
            s += "";
            s += "\r\nHealth: " + Health + "/" + MaxHealth;
            s += "\r\nSDC: " + sdc + "/" + MaxSDC;
            s += "\r\nLevel: " + Level;
            s += "\r\nSpeed: " + Speed;
            s += "\r\nIQ: " + IntelligenceQuotient;
            s += "\r\nME: " + MentalEndurance;
            s += "\r\nMA: " + MentalAffinity;
            s += "\r\nPS: " + PhysicalStrength;
            s += "\r\nPP: " + PhysicalProwess;
            s += "\r\nPB: " + PhysicalBeauty;
            s += "\r\nPE: " + PhysicalEndurance;

            return s;
        }
    }
}