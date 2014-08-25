using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
	public class DefaultItems
	{
		public static List<MeleeWeapon> MeleeWeaponList = new List<MeleeWeapon>
		{
			new MeleeWeapon { Condition = 100, Damage = "1d6", IsEquiped = false, MeleeWeaponType = MeleeWeaponType.Slash, Name = "Survival Knife", IgnoresAR = false},
			new MeleeWeapon { Condition = 100, Damage = "3d6", IsEquiped = false, MeleeWeaponType = MeleeWeaponType.Slash, Name = "Machete", IgnoresAR = true},
			new MeleeWeapon { Condition = 100, Damage = "2d8", IsEquiped = false, MeleeWeaponType = MeleeWeaponType.Pierce, Name = "Rapier", IgnoresAR = true},
			new MeleeWeapon { Condition = 100, Damage = "2d4", IsEquiped = false, MeleeWeaponType = MeleeWeaponType.Pierce, Name = "Shiv", IgnoresAR = true},
			new MeleeWeapon { Condition = 100, Damage = "2d6", IsEquiped = false, MeleeWeaponType = MeleeWeaponType.Blunt, Name = "Small Crowbar", IgnoresAR = false},
			new MeleeWeapon { Condition = 100, Damage = "2d8", IsEquiped = false, MeleeWeaponType = MeleeWeaponType.Blunt, Name = "Large Crowbar", IgnoresAR = false},
			new MeleeWeapon { Condition = 100, Damage = "2d12", IsEquiped = false, MeleeWeaponType = MeleeWeaponType.Blunt, Name = "12-pound sledgehammer", IgnoresAR = false}
		};

		public static List<RangedWeapon> RangedWeaponList = new List<RangedWeapon>
		{
            //new RangedWeapon { Condition = 100, Damage = "4d6", IsEquiped = false, RangedWeaponType = RangedWeaponType.Shotgun, Name = ".12-gauge Farmer", IgnoresAR = false, AmmoUsed = 1},
            //new RangedWeapon { Condition = 100, Damage = "4d6 x2", IsEquiped = false, RangedWeaponType = RangedWeaponType.Shotgun, Name = ".12-gauge Ronin", IgnoresAR = false, AmmoUsed = 2},
            //new RangedWeapon { Condition = 100, Damage = "4d6", IsEquiped = false, RangedWeaponType = RangedWeaponType.Shotgun, Name = ".12-gauge Slugger", IgnoresAR = false, AmmoUsed = 1},
            //new RangedWeapon { Condition = 100, Damage = "5d6 x2", IsEquiped = false, RangedWeaponType = RangedWeaponType.Shotgun, Name = "Swat-a-be Special", IgnoresAR = false, AmmoUsed = 2},
            //new RangedWeapon { Condition = 100, Damage = "4d6", IsEquiped = false, RangedWeaponType = RangedWeaponType.Rifle, Name = ".233 Militia", IgnoresAR = false, AmmoUsed = 1},
            //new RangedWeapon { Condition = 100, Damage = "4d6 x2", IsEquiped = false, RangedWeaponType = RangedWeaponType.Rifle, Name = ".556 Desert", IgnoresAR = false, AmmoUsed = 2},
            //new RangedWeapon { Condition = 100, Damage = "5d6", IsEquiped = false, RangedWeaponType = RangedWeaponType.Rifle, Name = ".762 Hunter", IgnoresAR = false, AmmoUsed = 1},
            //new RangedWeapon { Condition = 100, Damage = "6d6 +10", IsEquiped = false, RangedWeaponType = RangedWeaponType.Rifle, Name = ".50 Sniper", IgnoresAR = false, AmmoUsed = 4},
            //new RangedWeapon { Condition = 100, Damage = "2d4", IsEquiped = false, RangedWeaponType = RangedWeaponType.Handgun, Name = ".22 Sport", IgnoresAR = false, AmmoUsed = 1},
            //new RangedWeapon { Condition = 100, Damage = "3d6", IsEquiped = false, RangedWeaponType = RangedWeaponType.Handgun, Name = "9mm Rogue", IgnoresAR = false, AmmoUsed = 1},
            //new RangedWeapon { Condition = 100, Damage = "3d6 x2", IsEquiped = false, RangedWeaponType = RangedWeaponType.Handgun, Name = "9mm Gangsta", IgnoresAR = false, AmmoUsed = 2},
            //new RangedWeapon { Condition = 100, Damage = "4d6", IsEquiped = false, RangedWeaponType = RangedWeaponType.Handgun, Name = ".45 Defender", IgnoresAR = false, AmmoUsed = 1},
            //new RangedWeapon { Condition = 100, Damage = "5d6", IsEquiped = false, RangedWeaponType = RangedWeaponType.Handgun, Name = ".50 Israeli", IgnoresAR = false, AmmoUsed = 1}

		};

		public static List<Ammo> AmmoList = new List<Ammo>
		{
            //new Ammo { AmmoType = AmmoType.Handgun, Amount = 8, Name = "Small Handgun Clip" },
            //new Ammo { AmmoType = AmmoType.Handgun, Amount = 32, Name = "Large Handgun Clip" },
            //new Ammo { AmmoType = AmmoType.Rifle, Amount = 4, Name = "Small Rifle Clip" },
            //new Ammo { AmmoType = AmmoType.Rifle, Amount = 16, Name = "Large Rifle Clip" },
            //new Ammo { AmmoType = AmmoType.Shotgun, Amount = 5, Name = "Some Shotgun Shells" },
            //new Ammo { AmmoType = AmmoType.Shotgun, Amount = 20, Name = "A lot of Shotgun Shells" }

		};

		public static List<Health> HealthList = new List<Health>
		{
			new Health { AmountHealed = "1d6", Name = "A Small Health-kit"},
			new Health { AmountHealed = "2d6", Name = "A Medium Health-kit"},
			new Health { AmountHealed = "3d6", Name = "A Large Health-kit"}

		};

		public static List<SparePart> SparePartList = new List<SparePart>
		{
			new SparePart { AmountHealed = "1d6", Name = "A Wrench"},
			new SparePart { AmountHealed = "2d6", Name = "Some Spare Metal"},
			new SparePart { AmountHealed = "3d6", Name = "A Broken Weapon"},
			new SparePart { AmountHealed = "1d1", Name = "A Paperclip"}

		};

		public static List<Trap> TrapList = new List<Trap>
		{
			new Trap { Damage = "3d6", StatusEffect = StatusEffect.Stunned, Name = "Broken Glass"},
			new Trap { Damage = "1d2", StatusEffect = StatusEffect.Stunned, Name = "Legos"},
			new Trap { Damage = "2d6", StatusEffect = StatusEffect.Stunned, Name = "Asdf"},

		};

		//public static void Main()
		//{
		//	foreach (Weapon w in RangedWeaponList)
		//		Console.WriteLine(w.Damage);
		//}
	}
}
