using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class Trap : Item
	{
		public string Damage { get; set; }
		public StatusEffect StatusEffect { get; set; }
	}
}
