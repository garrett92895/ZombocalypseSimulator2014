using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Enums;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class Ammo : Item
	{
        public bool IsUsed { get; set; }
		public AmmoType AmmoType { get; set; }

        public Ammo()
        {
            IsUsed = false;
            AmmoType = Enums.AmmoType.Handgun;
            Value = 1;
            this.Name = Enums.AmmoType.Handgun.GetType().Name + " Ammo";
        }

        public Ammo(AmmoType Type)
        {
            IsUsed = false;
            this.AmmoType = Type;
            if (AmmoType == AmmoType.Rifle || AmmoType == AmmoType.Shotgun)
            {
                Value = 2;
            }
            else
            {
                Value = 1;
            }
            this.Name = Type.GetType().Name + " Ammo";
        }

	}
}
