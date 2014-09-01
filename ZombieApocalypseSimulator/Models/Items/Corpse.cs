using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Characters;

namespace ZombieApocalypseSimulator.Models.Items
{
    public class Corpse : Item
    {
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
        public Coordinate Location { get; set; }
        public int MaxSDC { get; set; }
        public int MaxHealth { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        private int TurnCounter;
        private DieRoll ReviveRoll;

        public Corpse(Character C)
        {
            ArmorRating = C.ArmorRating;
            IntelligenceQuotient = C.IntelligenceQuotient;
            MentalEndurance = C.MentalEndurance;
            MentalAffinity = C.MentalAffinity;
            PhysicalStrength = C.PhysicalStrength;
            PhysicalEndurance = C.PhysicalEndurance;
            PhysicalProwess = C.PhysicalProwess;
            PhysicalBeauty = C.PhysicalBeauty;
            Speed = C.Speed;
            Location = C.Location;
            MaxSDC = C.MaxSDC;
            MaxHealth = C.MaxHealth;
            if(C.GetType() == typeof(Player))
            {
                Items = ((Player)C).Items;
            }
            else
            {
                Items = new ObservableCollection<Item>();
            }
            TurnCounter = 0;
            ReviveRoll = new DieRoll(1, 100);
        }

        /// <summary>
        /// Rolls to see if this Corpse should become a Zombie
        /// </summary>
        /// <returns></returns>
        public bool RollRevive()
        {
            TurnCounter++;
            int Roll = ReviveRoll.Roll();
            return Roll < (5 * TurnCounter);
        }

        /// <summary>
        /// Makes a random type of Zed with the same properties as this corpse
        /// </summary>
        /// <returns></returns>
        public Zed SpawnZed()
        {
            Zed Z = ZedFactory.RandomSpecial();
            Z.Level = Level;
            Z.Location = Location;
            Z.MaxHealth = MaxHealth;
            Z.Health = Z.MaxHealth;
            Z.MaxSDC = MaxSDC;
            Z.PhysicalBeauty = PhysicalBeauty - 2;
            Z.PhysicalEndurance = PhysicalEndurance;
            Z.PhysicalProwess = PhysicalProwess;
            Z.PhysicalStrength = PhysicalStrength;
            Z.Speed = Speed;

            return Z;
        }

    }
}