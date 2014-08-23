using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Fighter : Player
    {
        public int Damage()
        {
            int damage = 0;
            if (EquippedWeaponType().Equals("Ranged"))
            {
                damage = RangedAttack();
                damage = 0;
            }
            else if (EquippedWeaponType().Equals("Melee"))
            {
                damage = MeleeAttack();
            }
            return damage;
        }
    }
}