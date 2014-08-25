using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Engineer : Player
    {
        public List<SparePart> Part = new List<SparePart>();
        public void GetParts()
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                if (Items.ElementAt(i).GetType() == typeof(SparePart))
                {
                    if (Part.Count < 11)
                    {
                        Part.Add((SparePart)Items.ElementAt(i));
                    }
                }
            }
        }

        public void AddTrap()
        {
            if (Part.Count > 3)
            {
                if (Items.Count() < ItemLimit)
                {
                    Items.Add(new Trap { Damage = "2d4", StatusEffect = StatusEffect.Stunned, Name = "Engineer Trap" });
                }
            }
        }

        public void FixWeapon(Weapon weapon)
        {
            if (Part.Count > 1)
            {
                double fix = (double)weapon.Condition;
                fix += weapon.Condition * .3;
            }
        }
    }
}