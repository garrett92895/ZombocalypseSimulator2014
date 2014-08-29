using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Factories;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator.Models.Characters.Classes
{
    public class Trader : Player
    {
        /// <summary>
        /// Determines what percentage that the value of items will be raised by when selling
        /// </summary>
        public int MarkUp { get; set; }

        /// <summary>
        /// Creates a Trader with the given amount of money and the given MarkUp when
        /// </summary>
        /// <param name="AmountOfMoney"></param>
        /// <param name="NewMarkUp"></param>
        public Trader(int AmountOfMoney = int.MaxValue, int NewMarkUp = 10)
        {
            Money = AmountOfMoney;
            MarkUp = NewMarkUp;
            Items = new List<Item>();
            Item LessThan = WeaponFactory.RandomWeapon();
            LessThan.Value = 50;
            Item JustRight = WeaponFactory.RandomWeapon();
            JustRight.Value = 100;
            Item TooMuch = WeaponFactory.RandomWeapon();
            TooMuch.Value = 500;
            Items.Add(LessThan);
            Items.Add(JustRight);
            Items.Add(TooMuch);
        }

        /// <summary>
        /// Method to use when an Item is being purchased from the Trader.  Will return the Item at the given Index and add the purchase value of  
        /// the item to the Merchant's Money
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Item PurchaseItem(int Index)
        {
            Item Purchased = Items.ElementAt(Index);
            Items.RemoveAt(Index);
            int PurchaseValue = PurchasePrice(Purchased);

            //Prevents overflow error when adding money
            if(PurchaseValue + Money < 0)
            {
                PurchaseValue = int.MaxValue-PurchaseValue;
            }
            Money -= PurchaseValue;
            return Purchased;
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
        /// Method to use when an Item is being sold to the Trader.  Will add the sold Item to the Trader's Inventory and will return the amount of money that 
        /// the Item can sell for according to SellPrice.  Subtracts the amount the Item sold for from the Trader's money.
        /// </summary>
        /// <param name="SellItem"></param>
        /// <returns></returns>
        public int SellItem(Item SellItem)
        {
            int SellValue = SellPrice(SellItem);
            Money -= SellValue;
            Items.Add(SellItem);
            return SellValue;
        }

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
