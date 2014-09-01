using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class Trap : Item
	{
		public DieRoll Damage { get; set; }
		public StatusEffect StatusEffect { get; set; }
	}
}
