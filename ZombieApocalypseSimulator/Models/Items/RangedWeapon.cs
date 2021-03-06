﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Items
{
    [Serializable()]
	public class RangedWeapon : Weapon
	{
		public RangedWeaponType RangedWeaponType { get; set; }
		public Magazine CurrentClip { get; set; }

		public override int UseWeapon()
		{
			if (CurrentClip.HasNext())
			{

                CurrentClip.Pop().IsUsed = true;

				return base.UseWeapon();
			}
			else
			{
				return 0;
			}
		}

        public override string ToString()
        {
            string ReturnString = base.ToString() + "\n Rounds in magazine: ";
            if (CurrentClip != null)
            {
                ReturnString += CurrentClip.Amount();
            }
            return ReturnString;
        }
	}
}
