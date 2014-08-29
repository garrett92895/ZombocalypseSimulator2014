using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Enums;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class MeleeWeapon : Weapon
	{
		public MeleeWeaponType MeleeWeaponType { get; set; }

	}
}
