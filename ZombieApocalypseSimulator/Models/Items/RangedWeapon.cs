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
		public Magazine CurrentClip { get; set; }

		public override int UseWeapon()
		{
			if (CurrentClip.HasNext())
			{

                CurrentClip.Pop().IsUsed = true;

				return base.UseWeapon();
			}
			else
			{
				return 0;
			}
		}
	}
}
