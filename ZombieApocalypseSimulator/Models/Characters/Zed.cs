﻿using System;
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
            string s = "";
            s += "";
            s += "\r\nHealth: " + Health + "/" + MaxHealth;
            s += "\r\nSDC: " + SDC + "/" + MaxSDC;
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