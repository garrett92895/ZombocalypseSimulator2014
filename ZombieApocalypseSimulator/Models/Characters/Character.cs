using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters
{
    public abstract class Character
    {

        public int MaxHealth { get; set; }
        private int _Health;
        public int Health
        {
            get
            {
                return _Health;
            }
            set
            {
                _Health = value;
                if (_Health <= 0)
                {
                    isAlive = false;
                }
                else
                {
                    isAlive = true;
                }
            }
        }

        protected bool _CanDodge;
        public virtual bool CanDodge
        {
            get
            {
                return _CanDodge;
            }
            set
            {
                _CanDodge = value;
            }
        }

        protected bool _CanParry;
        public virtual bool CanParry
        {
            get
            {
                return _CanParry;
            }
            set
            {
                _CanParry = value;
            }
        }
       
        public int ArmorRating { get; set; }
        public int IntelligenceQuotient { get; set; }
        public int MentalEndurance { get; set; }
        public int MentalAffinity { get; set; }
        public int PhysicalStrength { get; set; }
        public int PhysicalEndurance { get; set; }
        public int PhysicalProwess { get; set; }
        public int PhysicalBeauty { get; set; }
        public int Speed { get; set; }
        public int Level { get; set; }

        public List<Item> Items { get; set; }
        public Coordinate Location { get; set; }
        public bool isAlive { get; set; }

        public int MaxSDC { get; set; }
        public int sdc { get; set; }

        public void SetLife()
        {
            Health = rollHP();
            MaxHealth = Health;
            sdc = rollsdc();
            MaxSDC = sdc;
        }

        public virtual void takeDamage(int dam)
        {
            while (dam != 0 && sdc != 0)
            {
                dam--;
                sdc--;
            }
            Health -= dam;
        }

        public virtual int rollHP()
        {
            return DieRoll.RollOne(6) + PhysicalEndurance;
        }

        public virtual int rollsdc()
        {
            return DieRoll.RollOne(10) + 12;
        }
        public int squares()
        {
            int movement = ((Speed * 5) / 4);
            return movement;
        }

        public virtual int toHitMelee()
        {
            return DieRoll.RollOne(20) + bonusPP();
        }

        public virtual int toHitRanged(int OutsideBonus = 0)
        {
            return DieRoll.RollOne(20) + OutsideBonus;
        }

        public virtual int toDodgeRangedAttacks()
        {
            return 0;
        }

        public virtual int toDodge()
        {
            return DieRoll.RollOne(20) + bonusPP();
        }

        public virtual int toParry()
        {
            return DieRoll.RollOne(20) + bonusPP();
        }

        public abstract int MeleeAttack();
        public int bonusPS()
        {
            int bonus = PhysicalStrength - 15;
            if (bonus < 0)
            {
                bonus = 0;
            }
            return bonus;
        }

        public int bonusPP()
        {
            int bonus = (PhysicalProwess - 14)/2;
            if(bonus < 0)
            {
                bonus = 0;
            }
            return bonus;
        }
   
        public override string ToString()
        {
            string s = "";
            s += "Class: NotImplemented";
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