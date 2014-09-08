using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    [Serializable()]
    public class Tank : Zed
    {
        public Tank()
        {

            PhysicalStrength = DieRoll.RollOne(16) + 29;
            PhysicalProwess = DieRoll.RollOne(6) + 1;
            PhysicalEndurance = DieRoll.RollOne(6) + 15;
            Speed = DieRoll.RollOne(6) + 1;
			MaxSDC = rollsdc();
			SDC = MaxSDC;
			MaxHealth = rollHP();
			Health = MaxHealth;
        }

        public override Attack MeleeAttack()
        {
			DieRoll Die = new DieRoll (3, 6, NewModifyer: base.bonusPP());
			return new Attack(Die.Roll());
        }

        //public override int toHitMelee()
        //{
        //    return base.toHitMelee() + 2;
        //}

        public override int toParry()
        {
			return base.toParry() + 2;
        }

        public override int toDodge()
        {
			return base.toDodge() + 1;
        }

        public override int rollHP()
        {
            //Ensures the DieRoll will return a value between 35-50
            return DieRoll.RollOne(16) + 34;
        }

        public override int rollsdc()
        {
            //Ensures the DieRoll will return a value between 60-80
            return DieRoll.RollOne(21) + 59;
        }

        public override double DetermineWeaponEffectiveness(Weapon weapon)
        {
            double Multiplier = 1;
            Type WeaponType = weapon.GetType();

            if (WeaponType == typeof(MeleeWeapon))
            {
                MeleeWeapon meleeWeapon = (MeleeWeapon)weapon;
                if (meleeWeapon.MeleeWeaponType.Equals(MeleeWeaponType.Blunt))
                {
                    Multiplier = .5;
                }
                else if (meleeWeapon.MeleeWeaponType.Equals(MeleeWeaponType.Slash))
                {
                    Multiplier = 2;
                }
            }

            return Multiplier;
        }
    }
}
