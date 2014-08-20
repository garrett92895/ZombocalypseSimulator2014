using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classess
{
    class Sloucher: Zed
    {
        public override bool CanDodge 
        { 
            get
            {
                return false;
            }
            set
            {
                _CanDodge = false;
            }
        }
        public Sloucher()
        {
            sdc = 0;
        }

        public override int toParry()
        {
            ZombieApocalypse.DieRoll Die = new ZombieApocalypse.DieRoll(1, 20);
            int hit = (int)(Die.Roll());
            return hit;
        }
    }
}
