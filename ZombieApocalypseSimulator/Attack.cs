using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator
{
    public class Attack
    {
        public Weapon AttackerWeapon { get; set; }
        public int Damage { get; set; }
        public bool IsPiercing { get; set; }

        public Attack(int damage, bool isPiercing = false)
        {
            this.Damage = damage;
            this.IsPiercing = isPiercing;
        }
    }
}
