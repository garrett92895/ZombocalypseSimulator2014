using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Factories
{
    public static class WeaponFactory
    {
        public static Random Rand = new Random();

        public static Weapon RandomWeapon()
        {
            //Weapon _weapon;
            int WeapType = Rand.Next(2);
            if (WeapType == 0)
            {
                MeleeWeapon _weapon = new MeleeWeapon();
                int MeleeType = Rand.Next(3);
                if (MeleeType == 0)
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Blunt;
                }
                else if (MeleeType == 1)
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Pierce;
                }
                else
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Slash;
                }
                _weapon.Condition = Rand.Next(90);
                _weapon.Damage = Rand.Next(1,5)+"d"+Rand.Next(1,12);

                return _weapon;
            }
            else
            {
                RangedWeapon _weapon = new RangedWeapon();
                int RangedType = Rand.Next(3);
                if (RangedType == 0)
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Rifle;
                }
                else if (RangedType == 1)
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Handgun;
                }
                else
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Shotgun;
                }
                _weapon.Condition = Rand.Next(60);
                _weapon.Damage = Rand.Next(1, 7) + "d" + Rand.Next(1, 6);

                return _weapon;
            }
        }


        /*
         * The format for the weapons factory is as follows:
         *  "Range|Type|Condition|Damage|(Ranged Only) Ammo Count"
         *  So if i wanted a shotgun with four rounds I would pass in:
         *  "Ranged|Shotgun|80|3d6|4 " 
         *  Or if I wanted a shiv
         *  "Melee|Pierce|20|1d20 "
         */
        public static Weapon GetInstance(string weapontype)
        {
            //Weapon _weapon;
            string[] SplitParams = new string[1];
            SplitParams[0] = "|";
            string[] WeaponParams = weapontype.Split(SplitParams, StringSplitOptions.None);
            if (WeaponParams[0].Equals("Melee"))
            {
                MeleeWeapon _weapon = new MeleeWeapon();
                if(WeaponParams[1].Equals(MeleeWeaponType.Blunt.GetType()))
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Blunt;
                }
                else if (WeaponParams[1].Equals(MeleeWeaponType.Pierce.GetType()))
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Pierce;
                }
                else
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Slash;
                }
                _weapon.Condition = Int32.Parse(WeaponParams[2]);
                _weapon.Damage = WeaponParams[3];
                return _weapon;
            }
            else
            {
                RangedWeapon _weapon = new RangedWeapon();
                if (WeaponParams[1].Equals(MeleeWeaponType.Blunt.GetType()))
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Rifle;
                }
                else if (WeaponParams[1].Equals(MeleeWeaponType.Pierce.GetType()))
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Handgun;
                }
                else
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Shotgun;
                }
                _weapon.Condition = Int32.Parse(WeaponParams[2]);
                _weapon.Damage = WeaponParams[3];
                _weapon.CurrentClip = new Magazine(Int32.Parse(WeaponParams[4]));
                return _weapon;
            }
        }

    }
}
