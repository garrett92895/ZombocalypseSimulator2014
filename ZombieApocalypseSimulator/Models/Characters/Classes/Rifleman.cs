using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Rifleman : Player
    {
        private readonly byte RiflemanMaxAmmo = 200;

        public byte bonusToAttack()
        {
            byte bonus = 0;
            string weaponType = EquippedWeaponType();
            if (weaponType == "Ranged")
            {
                bonus = 1;
            }
            return bonus;
        }

        public override int RangedAttack()
        {
            byte hit = (byte)(Dice.Roll(1, 20));
            hit += bonusToAttack();
            return hit;
        }
        private void maxAmmo()
        {

        }

    }
}