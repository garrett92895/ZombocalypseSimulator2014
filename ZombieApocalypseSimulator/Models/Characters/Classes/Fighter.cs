using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    [Serializable()]
    public class Fighter : Player
    {
        public Fighter()
        {
            Name = "Phill";
            Items = new ObservableCollection<Item>();
            ItemLimit = 5;
            //Items.Add(new MeleeWeapon { Condition = 100, Damage = "2d6", IsEquiped = false, 
            //    MeleeWeaponType = MeleeWeaponType.Blunt, Name = "Small Crowbar", IgnoresAR = false });
            Items.Add(WeaponFactory.GetInstance("Small Crowbar|Melee|Blunt|100|2d6"));
            EquippedWeapon = (Weapon)Items.ElementAt(0);
            IntelligenceQuotient = rollAttributes();
            MentalEndurance = rollAttributes();
            MentalAffinity = rollAttributes();
            PhysicalStrength = rollAttributes();
            PhysicalEndurance = rollAttributes();
            PhysicalProwess = rollAttributes();
            PhysicalBeauty = rollAttributes();
            Speed = rollAttributes();
            ArmorRating = 0;
            base.SetLife();
        }

        public int Damage()
        {
            int damage = 0;
            if (EquippedWeaponType().Equals("Ranged"))
            {
                damage = RangedAttack();
                damage = 0;
            }
            else if (EquippedWeaponType().Equals("Melee"))
            {
                damage = MeleeAttack().Damage;
            }
            return damage;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}