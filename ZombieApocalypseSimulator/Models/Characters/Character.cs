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
        private int _Health;
        protected bool _CanDodge;
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
        public int MaxHealth { get; set; }
        public int Health 
        { 
            get
            {
                return _Health;
            }
            set
            {
                _Health = value;
                if(_Health <= 0)
                {
                    isAlive = false;
                }
                else
                {
                    isAlive = true;
                }
            }
        }
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
            if(sdc != 0)
            {
                while(dam != 0 && sdc != 0)
                {
                    dam--;
                    sdc--;
                }
                Health -= dam;
            }
        }

        public virtual int rollHP()
        {
            int hp = (Dice.Roll(1, 6) + PhysicalEndurance);
            return hp;
        }

        public virtual int rollsdc()
        {
            int armor = (12 + Dice.Roll(1, 10));
            return armor;
        }
        public int squares()
        {
            byte movement = (byte)((Speed * 5) / 4);
            return movement;
        }

        public virtual int toHitMelee()
        {
            return (Dice.Roll(1, 20)) + bonusPP();
        }
        public virtual int toHitRanged()
        {
            byte hit = (byte)(Dice.Roll(1, 20));
            return hit;
        }
        public virtual int toHitRanged(int OutsideBonus)
        {
            byte hit = (byte)(Dice.Roll(1, 20) + OutsideBonus);
            return hit;
        }

        public virtual int toDodgeRangedAttacks()
        {
            return 0;
        }

        public virtual int toDodge()
        {
            byte hit = (byte)(Dice.Roll(1, 20) + bonusPP());
            return hit;
        }

        public virtual int toParry()
        {
            int hit = (int)(Dice.Roll(1, 20) + bonusPP());
            return hit;
        }

        public abstract int MeleeAttack();
        public int bonusPS()
        {
            byte bonus = 0;
            switch (PhysicalStrength)
            {
                case 16:
                    bonus = 1;
                    break;
                case 17:
                    bonus = 2;
                    break;
                case 18:
                    bonus = 3;
                    break;
                case 19:
                    bonus = 4;
                    break;
                case 20:
                    bonus = 5;
                    break;
                case 21:
                    bonus = 6;
                    break;
                case 22:
                    bonus = 7;
                    break;
                case 23:
                    bonus = 8;
                    break;
                case 24:
                    bonus = 9;
                    break;
                case 25:
                    bonus = 10;
                    break;
                case 26:
                    bonus = 11;
                    break;
                case 27:
                    bonus = 12;
                    break;
                case 28:
                    bonus = 13;
                    break;
                case 29:
                    bonus = 14;
                    break;
                case 30:
                    bonus = 15;
                    break;
                case 31:
                    bonus = 16;
                    break;
                case 32:
                    bonus = 17;
                    break;
                case 33:
                    bonus = 18;
                    break;
                case 34:
                    bonus = 19;
                    break;
                case 35:
                    bonus = 20;
                    break;
                case 36:
                    bonus = 21;
                    break;
                case 37:
                    bonus = 22;
                    break;
                case 38:
                    bonus = 23;
                    break;
                case 39:
                    bonus = 24;
                    break;
                case 40:
                    bonus = 25;
                    break;
                case 41:
                    bonus = 26;
                    break;
                case 42:
                    bonus = 27;
                    break;
                case 43:
                    bonus = 28;
                    break;
                case 44:
                    bonus = 29;
                    break;
                case 45:
                    bonus = 30;
                    break;
            };
            return bonus;
        }

        public int bonusPP()
        {
            byte bonus = 0;
            if (PhysicalProwess == 16 || PhysicalProwess == 17)
            {
                bonus = 1;
            }
            else if (PhysicalProwess == 18 || PhysicalProwess == 19)
            {
                bonus = 2;
            }
            else if (PhysicalProwess == 20 || PhysicalProwess == 21)
            {
                bonus = 3;
            }
            else if (PhysicalProwess == 22 || PhysicalProwess == 23)
            {
                bonus = 4;
            }
            else if (PhysicalProwess == 24 || PhysicalProwess == 25)
            {
                bonus = 5;
            }
            else if (PhysicalProwess == 26 || PhysicalProwess == 27)
            {
                bonus = 6;
            }
            else if (PhysicalProwess == 28 || PhysicalProwess == 29)
            {
                bonus = 7;
            }
            else if (PhysicalProwess == 30)
            {
                bonus = 8;
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