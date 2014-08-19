using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class Ammo : Item
	{
		public int Amount { get; set; }
		public AmmoType AmmoType { get; set; }

	}
}
