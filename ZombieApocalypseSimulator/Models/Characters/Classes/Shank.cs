using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Shank: Zed
    {
		public Shank()
		{
			PhysicalStrength = DieRoll.RollOne(6) + 18;
            PhysicalProwess = DieRoll.RollOne(6) + 9;
            PhysicalEndurance = DieRoll.RollOne(6) + 15;
            Speed = DieRoll.RollOne(9) + 11;
			MaxSDC = rollsdc();
			SDC = MaxSDC;
			MaxHealth = rollHP();
			Health = MaxHealth;
		}

		public override Attack MeleeAttack()
		{
            DieRoll DamageRoll = new DieRoll (2, 6, NewModifyer: base.bonusPS());
			return new Attack(DamageRoll.Roll(), true);
		}

        //public override int toHitMelee()
        //{
        //    return base.toHitMelee() + 3;
        //}

		public override int toParry()
		{
			return base.toParry() + 3;
		}

		public override int toDodge()
		{
			return base.toDodge() + 3;
		}

		public override int rollHP()
		{
            return DieRoll.RollOne(6) + 14;
		}

		public override int rollsdc()
		{
            return DieRoll.RollOne(11) + 19;
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
                    Multiplier = 2;
                }
                else if (meleeWeapon.MeleeWeaponType.Equals(MeleeWeaponType.Slash))
                {
                    Multiplier = .5;
                }
            }

            return Multiplier;
        }
    }
}
