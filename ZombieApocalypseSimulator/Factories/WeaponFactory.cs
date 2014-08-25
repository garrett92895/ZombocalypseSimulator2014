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
                    _weapon.Name = "Bone-Crusher";
                }
                else if (MeleeType == 1)
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Pierce;
                    _weapon.Name = "Pokey-Staby";
                }
                else
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Slash;
                    _weapon.Name = "Swiper";
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
                    _weapon.Name = "Hunting Rifle";
                }
                else if (RangedType == 1)
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Handgun;
                    _weapon.Name = "Colt M1911";
                }
                else
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Shotgun;
                    _weapon.Name = "Boomstick";
                }
                _weapon.Condition = Rand.Next(60);
                _weapon.Damage = Rand.Next(1, 7) + "d" + Rand.Next(1, 6);

                return _weapon;
            }
        }
        
        /*
         * The format for the weapons factory is as follows:
         *  "Name|Range|Type|Condition|Damage|(Ranged Only) Ammo Count"
         *  So if i wanted a shotgun with four rounds I would pass in:
         *  "Shotty|Ranged|Shotgun|80|3d6|4 " 
         *  Or if I wanted a shiv
         *  "Shiv|Melee|Pierce|20|1d20 "
         */
        public static Weapon GetInstance(string weapontype)
        {
            //Weapon _weapon;
            string[] SplitParams = new string[1];
            SplitParams[0] = "|";
            string[] WeaponParams = weapontype.Split(SplitParams, StringSplitOptions.None);
            if (WeaponParams[1].Equals("Melee"))
            {
                MeleeWeapon _weapon = new MeleeWeapon();
                _weapon.Name = WeaponParams[0];
                if(WeaponParams[2].Equals(MeleeWeaponType.Blunt.GetType()))
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Blunt;
                    
                }
                else if (WeaponParams[2].Equals(MeleeWeaponType.Pierce.GetType()))
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Pierce;
                    
                }
                else
                {
                    _weapon.MeleeWeaponType = MeleeWeaponType.Slash;
                    
                }
                _weapon.Condition = Int32.Parse(WeaponParams[3]);
                _weapon.Damage = WeaponParams[4];
                return _weapon;
            }
            else
            {                
                RangedWeapon _weapon = new RangedWeapon();
                _weapon.Name = WeaponParams[0];
                if (WeaponParams[2].Equals(RangedWeaponType.Rifle.GetType()))
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Rifle;
                }
                else if (WeaponParams[2].Equals(RangedWeaponType.Handgun.GetType()))
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Handgun;
                }
                else
                {
                    _weapon.RangedWeaponType = RangedWeaponType.Shotgun;
                }
                _weapon.Condition = Int32.Parse(WeaponParams[3]);
                _weapon.Damage = WeaponParams[4];
                _weapon.CurrentClip = new Magazine(Int32.Parse(WeaponParams[5]));
                return _weapon;
            }
        }

    }
}
