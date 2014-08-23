using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Bruiser : Player
    {
        public override int rollsdc()
        {
            int armor = (12 + Dice.Roll(2, 6));
            return armor;
        }

        public bool Shout()
        {
            bool isEffective = false;
            int shoutRoll = Dice.Roll(1, 30);
            if (shoutRoll <= MentalAffinity)
            {
                isEffective = true;
            }
            return isEffective;
        }
    }
}