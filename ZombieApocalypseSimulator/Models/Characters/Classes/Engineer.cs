using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Engineer : Player
    {
        public List<SparePart> GetParts()
        {
            List<SparePart> Part = new List<SparePart>();
            for (int i = 0; i < Items.Count(); i++)
            {
                if (Items.ElementAt(i).GetType() == typeof(SparePart))
                {
                    Part.Add((SparePart)Items.ElementAt(i));
                }
            }
            return Part;
        }

        public void AddTrap()
        {
            if (Items.Count() < ItemLimit)
            {
                Items.Add(new Trap { Damage = "2d4", StatusEffect = "N/a", Name = "Engineer Trap" });
            }
        }

        public void FixWeapon(Weapon weapon)
        {
            //if (GetParts <= 11)
            //{

            //}
        }
    }
}