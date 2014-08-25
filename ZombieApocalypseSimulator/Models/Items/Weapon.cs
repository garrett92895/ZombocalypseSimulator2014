using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypse;

namespace ZombieApocalypseSimulator.Models.Items
{
	public abstract class Weapon : Item
	{
		public string Damage { get; set; }
		public bool IsEquiped { get; set; }
		public int Condition { get; set; }
		public bool IgnoresAR { get; set; }

		public virtual int UseWeapon()
		{
			Condition -= Dice.Roll(2, 6, 1, 1);
			
			return Dice.Roll(Damage);

		}

        public override string ToString()
        {
            string s = base.ToString();
            s += " (" + Condition + ")";
            return s;
        }
	}
}
