using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;
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
        private int TurnCounter;
        private DieRoll ReviveRoll;

        public Corpse(Player C)
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
            TurnCounter = 0;
            ReviveRoll = new DieRoll(1, 100);
        }

        public bool RollRevive()
        {
            TurnCounter++;
            int Roll = ReviveRoll.Roll();
            return Roll < (5 * TurnCounter);
        }

    }
}
