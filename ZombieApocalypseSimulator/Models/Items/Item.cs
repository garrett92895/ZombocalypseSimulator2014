using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieApocalypseSimulator.Models.Items
{
    [Serializable()]
	public class Item
	{

		public string Name { get; set; }
		public string Description { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Name;
        }

	}
}
