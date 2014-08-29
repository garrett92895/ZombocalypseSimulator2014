using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Enums;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public abstract class Weapon : Item
	{
		public DieRoll Damage { get; set; }
		public bool IsEquiped { get; set; }
		public int Condition { get; set; }
		public bool IgnoresAR { get; set; }
        public StatusEffect Effect { get; set; }

        public virtual int UseWeapon()
        {
            DieRoll ConditionDie = new DieRoll(2, 6);
            Condition -= ConditionDie.Roll();
            return Damage.Roll();
        }

        public override string ToString()
        {
            return base.ToString() + "\n Current Condition:" + Condition;
        }

	}
}
