using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class HalfZombie : Player
    {
        private bool turnningCheck(byte roll)
        {
            byte check = (byte)Dice.Roll(1, 20);
            if (roll == 2)
            {
                if (check <= 2)
                {
                    isAlive = false;
                }
            }
            else if (roll == 4)
            {
                if (check <= 4)
                {
                    isAlive = false;
                }
            }
            return isAlive;
        }


        public override Attack MeleeAttack()
        {
            int Damage = 0;
            if (EquippedWeapon.Condition > 10)
            {
                Damage = EquippedWeapon.UseWeapon();
                Damage += bonusPS();
                Damage += 1;
            }
            else
            {
                Damage = 0;
            }
            return new Attack(Damage, false);
        }
    }
}