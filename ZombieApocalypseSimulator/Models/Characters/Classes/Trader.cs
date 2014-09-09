using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    [Serializable()]
    public class Trader : Player
    {
        /// <summary>
        /// Determines what percentage that the value of items will be raised by when selling
        /// </summary>
        public int MarkUp { get; set; }

        public int HandgunAmmo { get; set; }
        public int RifleAmmo { get; set; }
        public int ShotgunAmmo { get; set; }

        /// <summary>
        /// Creates a Trader with the given amount of money and the given MarkUp when
        /// </summary>
        /// <param name="AmountOfMoney"></param>
        /// <param name="NewMarkUp"></param>
        public Trader(int AmountOfMoney = int.MaxValue, int NewMarkUp = 10, int NewHandgunAmmo = 100, int NewRifleAmmo = 100, int NewShotgunAmmo = 100)
        {
            Money = AmountOfMoney;
            MarkUp = NewMarkUp;
            Items = new ObservableCollection<Item>();
            HandgunAmmo = NewHandgunAmmo;
            RifleAmmo = NewRifleAmmo;
            ShotgunAmmo = NewShotgunAmmo;

            for (int i = 0; i < 5; i++)
            {
                Items.Add(WeaponFactory.RandomWeapon());
            }
        }

        /// <summary>
        /// Method to use when an Item is being purchased from the Trader.  Will return the Item at the given Index and add the purchase value of  
        /// the item to the Merchant's Money
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        //public Item PurchaseItem(int Index)
        //{
        //    Item Purchased = Items.ElementAt(Index);
        //    Items.RemoveAt(Index);
        //    int PurchaseValue = PurchasePrice(Purchased);

        //    //Prevents overflow error when adding money
        //    if(PurchaseValue + Money < 0)
        //    {
        //        PurchaseValue = int.MaxValue-PurchaseValue;
        //    }
        //    Money -= PurchaseValue;
        //    return Purchased;
        //}

        /// <summary>
        /// Method to be used when Ammo is being purchased from the Trader.
        /// Returns a List of Ammo that contains the Amount specified of the AmmoType specified
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Amount"></param>
        public List<Ammo> BuyAmmo(AmmoType TypeOfAmmo, int Amount)
        {
            List<Ammo> Ammos = new List<Ammo>();

            //Makes sure not to give more ammo than the merchant has
            //int HoldingAmmo = 0;
            //switch (TypeOfAmmo)
            //{
            //    case AmmoType.Handgun: HoldingAmmo = HandgunAmmo; break;

            //    case AmmoType.Rifle: HoldingAmmo = RifleAmmo; break;

            //    case AmmoType.Shotgun: HoldingAmmo = ShotgunAmmo; break;
            //}

            while (Amount > 0)
            {
                Ammo Round = new Ammo(TypeOfAmmo);
                Money += Round.Value;
                Ammos.Add(Round);
                Amount--;
            }

            //Adjusts the merchants ammo stockpile based on how much was sold
            //switch (TypeOfAmmo)
            //{
            //    case AmmoType.Handgun: HandgunAmmo = HoldingAmmo; break;

            //    case AmmoType.Rifle: RifleAmmo = HoldingAmmo; break;

            //    case AmmoType.Shotgun: ShotgunAmmo = HoldingAmmo; break;
            //}
            return Ammos;
        }

        /// <summary>
        /// Method to be used when Ammo is being sold to the Trader.
        /// Takes away the given amount of money used to purchase the Ammo
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Amount"></param>
        public int PurchaseAmmoCost(AmmoType TypeOfAmmo, int Amount)
        {
            int Total = new Ammo(TypeOfAmmo).Value * Amount;
            return Total + (Total/MarkUp);
        }

        /// <summary>
        /// Returns the amount of money that must be paid to purchase the given item
        /// </summary>
        /// <param name="Prospect"></param>
        /// <returns></returns>
        public int PurchasePrice(Item Prospect)
        {
            return Prospect.Value + (Prospect.Value / MarkUp);
        }

        /// <summary>
        /// Returns the amount of money that the Trader can pay for the item.  
        /// Will be the value of the Item unless the Trader has less Money than the value of the Item.  
        /// If that is the case the offer will be the amount of money that the Trader has left (this can return zero 
        /// if the Trader has no money left).
        /// </summary>
        /// <param name="Sell"></param>
        /// <returns></returns>
        public int SellPrice(Item Sell)
        {
            int SellValue = Sell.Value;
            if (SellValue > Money)
            {
                SellValue = Money;
            }
            return SellValue;
        }

        /// <summary>
        /// Returns the maximum number of rounds of the given Type that the Trader can buy.
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <returns></returns>
        public int SellAmmoLimit(AmmoType TypeOfAmmo)
        {
            return Money/(new Ammo(TypeOfAmmo).Value);
        }

        /// <summary>
        /// Method to use when an Item is being sold to the Trader.  Will add the sold Item to the Trader's Inventory and will return the amount of money that 
        /// the Item can sell for according to SellPrice.  Subtracts the amount the Item sold for from the Trader's money.
        /// </summary>
        /// <param name="SellItem"></param>
        /// <returns></returns>
        //public int SellItem(Item SellItem)
        //{
        //    int SellValue = SellPrice(SellItem);
        //    Money -= SellValue;
        //    Items.Add(SellItem);
        //    return SellValue;
        //}


        /// <summary>
        /// The Trader can not attack and will always return 0 an attack with 0 damage as a result
        /// </summary>
        /// <returns></returns>
        public override Attack MeleeAttack()
        {
            return new Attack(0);
        }

        /// <summary>
        /// The Trader can not attack and will always return false as a result
        /// </summary>
        /// <returns></returns>

        public override bool HasWeapon()
        {
            return false;
        }


        /// <summary>
        /// The Trader cannot attack and will always return 0 as a result.
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public override double DetermineWeaponEffectiveness(Weapon weapon)
        {
            return 0;
        }
    }
}
