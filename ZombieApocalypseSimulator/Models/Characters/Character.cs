using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters
{
    public abstract class Character : INotifyPropertyChanged, IComparable, IComparable<Character>
    {

        public string Name { get; set; }

        private int _MaxHealth;
        public int MaxHealth 
        {
            get 
            {
                return _MaxHealth;
            }
            set
            {
                _MaxHealth = value;

                NotifyPropertyChanged("MaxHealth");
            }
        }

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
                NotifyPropertyChanged("Health");
            }
        }

        protected bool _CanParry;
        protected bool _HasDodged;
        public bool CanParry;
        

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

       
        public virtual bool HasDodged
        {
            get
            {
                return _HasDodged;
            }
            set
            {
                _HasDodged = value;
            }
        }

        private int _ArmorRating;
        public int ArmorRating 
        {
            get
            {
                return _ArmorRating;
            }
            set
            {
                _ArmorRating = value;
                NotifyPropertyChanged("ArmorRating");
            }
        }

        private int _IntelligenceQuotient;
        public int IntelligenceQuotient
        {
            get
            {
                return _IntelligenceQuotient;
            }
            set
            {
                _IntelligenceQuotient = value;
                NotifyPropertyChanged("IntelligenceQuotient");
            }
        }

        private int _MentalEndurance;
        public int MentalEndurance
        {
            get
            {
                return _MentalEndurance;
            }
            set
            {
                _MentalEndurance = value;
                NotifyPropertyChanged("MentalEndurance");
            }
        }

        private int _MentalAffinity;
        public int MentalAffinity
        {
            get
            {
                return _MentalAffinity;
            }
            set
            {
                _MentalAffinity = value;
                NotifyPropertyChanged("MentalAffinity");
            }
        }
        
        private int _PhysicalStrength;
        public int PhysicalStrength
        {
            get
            {
                return _PhysicalStrength;
            }
            set
            {
                _PhysicalStrength = value;
                NotifyPropertyChanged("PhysicalStrength");
            }
        }

        private int _PhysicalEndurance;
        public int PhysicalEndurance
        {
            get
            {
                return _PhysicalEndurance;
            }
            set
            {
                _PhysicalEndurance = value;
                NotifyPropertyChanged("PhysicalEndurance");
            }
        }

        private int _PhysicalProwess;
        public int PhysicalProwess
        {
            get
            {
                return _PhysicalProwess;
            }
            set
            {
                _PhysicalProwess = value;
                NotifyPropertyChanged("PhysicalProwess");
            }
        }

        private int _PhysicalBeauty;
        public int PhysicalBeauty
        {
            get
            {
                return _PhysicalBeauty;
            }
            set
            {
                _PhysicalBeauty = value;
                NotifyPropertyChanged("PhysicalBeauty");
            }
        }

        private int _Speed;
        public int Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
                NotifyPropertyChanged("Speed");
            }
        }

        private int _Level = 1;
        public int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                _Level = value;
                NotifyPropertyChanged("Level");
            }
        }

        public ObservableCollection<Item> Items { get; set; }
        public int Money { get; set; }
        public Coordinate Location { get; set; }
        public bool isAlive { get; set; }

        private int _MaxSDC;
        public int MaxSDC 
        {
            get
            {
                return _MaxSDC;
            }
            set
            {
                _MaxSDC = value;

                NotifyPropertyChanged("MaxSDC");
            }
        }

        private int _SDC = 0;
        public int SDC
        {
            get
            {
                return _SDC;
            }
            set
            {
                _SDC = value;

                NotifyPropertyChanged("SDC");
            }
        }

        private int _initiative { get; set; }
        public int Initiative
        {
            get
            {
                return _initiative;
            }
            set
            {
                _initiative = value;
            }
        }

        public void SetLife()
        {
            Health = rollHP();
            MaxHealth = Health;
            SDC = rollsdc();
            MaxSDC = SDC;
            isAlive = true;
        }

        public virtual void takeDamage(Attack attack)
        {
            int dam = attack.Damage;
            if (!attack.IsPiercing)
            {
                while (dam != 0 && SDC != 0)
                {
                    dam--;
                    SDC--;
                }
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
            if(HasDodged)
            {
                movement -= 3;
                HasDodged = false;
            }
            return movement;
        }

        public virtual int StrikeBonus()
        {
            return bonusPP();
        }

        public virtual int toDodgeRangedAttacks()
        {
            return 0;
        }

        public virtual int toDodge()
        {
            return bonusPP();
        }

        public virtual int toParry()
        {
            return bonusPP();
        }

        public abstract Attack MeleeAttack();
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
            return bonus + Level;
        }

        public abstract bool HasWeapon();

        public abstract double DetermineWeaponEffectiveness(Weapon weapon);


        public override string ToString()
        {
            string s = "";
            s += "Class: "+this.GetType().Name;
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
            s += "\r\nMoney: " + Money;

            return s;
        }

        public byte LevelUp()
        {
            byte bonus = 0;
            byte level = (byte)Level;
            Level++;
            MaxHealth += new DieRoll(1, 6).Roll();
            MaxSDC += new DieRoll(1, 4).Roll();
            IntelligenceQuotient += new DieRoll(1, 3).Roll();
            MentalEndurance += new DieRoll(1, 3).Roll();
            MentalAffinity += new DieRoll(1, 3).Roll();
            PhysicalStrength += new DieRoll(1, 3).Roll();
            PhysicalProwess += new DieRoll(1, 3).Roll();
            PhysicalBeauty += new DieRoll(1, 3).Roll();
            PhysicalEndurance += new DieRoll(1, 3).Roll();
            return bonus;
        }
        public byte LevelDown()
        {
            byte bonus = 0;
            byte level = (byte)Level;
            Level--;
            MaxHealth -= new DieRoll(1, 6).Roll();
            MaxSDC -= new DieRoll(1, 4).Roll(); ;
            IntelligenceQuotient -= new DieRoll(1, 3).Roll();
            MentalEndurance -= new DieRoll(1, 3).Roll();
            MentalAffinity -= new DieRoll(1, 3).Roll();
            PhysicalStrength -= new DieRoll(1, 3).Roll();
            PhysicalProwess -= new DieRoll(1, 3).Roll();
            PhysicalBeauty -= new DieRoll(1, 3).Roll();
            PhysicalEndurance -= new DieRoll(1, 3).Roll();
            return bonus;
        }

        /// <summary>
        /// Notifies any events in PropertyChanged that a specific property has been changed
        /// </summary>
        /// <param name="Info"></param>
        private void NotifyPropertyChanged(String Info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Character other)
        {
            throw new NotImplementedException();
        }
    }
}