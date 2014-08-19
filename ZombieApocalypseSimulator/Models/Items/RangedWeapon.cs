using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class RangedWeapon : Weapon
	{
		public RangedWeaponType RangedWeaponType { get; set; }
		public int AmmoUsed { get; set; }
		public Ammo CurrentClip { get; set; }

		public override int UseWeapon()
		{
			if (CurrentClip.Amount >= AmmoUsed)
			{

				CurrentClip.Amount -= AmmoUsed;

				return base.UseWeapon();
			}
			else
			{
				return 0;
			}
		}
	}
}
