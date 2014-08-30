using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class Health : Item
	{
		public DieRoll AmountHealed { get; set; }

        public Health(int NumberOfDice = 1, int SidesPerDie = 6)
        {
            AmountHealed = new DieRoll(NumberOfDice, SidesPerDie);
            Value = NumberOfDice * ((SidesPerDie) / 2) + 1;
        }
	}
}
