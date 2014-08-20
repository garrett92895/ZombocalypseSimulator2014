using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classess
{
    class FastAttack: Zed
    {
        public override int toDodgeRangedAttack()
        {
            return toDodge() - 6;
        }
    }
}
