using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseSimulator
{
    public class Transaction
    {
        /// <summary>
        /// Items that are being sold to the Buyer
        /// </summary>
        public List<Item> SellingItems { get; set; }

        /// <summary>
        /// The amount of money the Seller gains or losses from the Transaction
        /// </summary>
        public int SellerMoneyChange { get; set; }

        /// <summary>
        /// Items that are being offered by the Buyer
        /// </summary>
        public List<Item> BuyingItems { get; set; }

        /// <summary>
        /// The amount of money the Buyer gains or losses from the Transaction
        /// </summary>
        public int BuyerMoneyChange { get; set;}

        /// <summary>
        /// Character that initiated the Transaction
        /// </summary>
        public Character Buyer { get; set; }

        /// <summary>
        /// Character that is selling to the Buyer
        /// </summary>
        public Character Seller { get; set; }

        //Flag which states when this Transaction is done
        public bool Done { get; set; }

        public Transaction(Character NewBuyer, Character NewSeller)
        {
            Buyer = NewBuyer;
            Seller = NewSeller;
            BuyingItems = new List<Item>();
            SellingItems = new List<Item>();
            BuyerMoneyChange = 0;
            SellerMoneyChange = 0;
            Done = false;
        }

        /// <summary>
        /// Removes the given Items from the Seller's List and Adds it to the SellingItems list
        /// Also adjusts SellerMoneyChange and BuyerMoneyChange based on the given Price
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Price"></param>
        public void PurchaseItem(Item I, int Price)
        {
            Seller.Items.Remove(I);
            SellerMoneyChange += Price;
            BuyerMoneyChange -= Price;
            SellingItems.Add(I);
        }

        public void SellItem(Item I, int Price)
        {
            Buyer.Items.Remove(I);
            BuyerMoneyChange += Price;
            SellerMoneyChange -= Price;
            BuyingItems.Add(I);
        }

        /// <summary>
        /// Finalizes the Transaction, exchanging the Items and Money that have been traded, and signals that this Transaction is Done
        /// </summary>
        public void FinishTransaction()
        {
            //Adds the Items bought from the Seller to the Buyer's Inventory
            foreach (Item I in SellingItems)
            {
                Buyer.Items.Add(I);
            }

            //Adds the Items sold to the Seller to the Seller's Inventory
            foreach (Item I in BuyingItems)
            {
                Buyer.Items.Remove(I);
            }
            
            //Money changes hands
            Buyer.Money  += BuyerMoneyChange;
            Seller.Money += SellerMoneyChange;
            Done = true;
        }

        /// <summary>
        /// Used to Cancel this Transaction, adds all the Items that were going to be traded back into each Player's Inventory
        /// </summary>
        public void CancelTransaction()
        {
            //Adds the Items that were going to be bought from the Seller back to the Seller's Inventory
            foreach (Item I in SellingItems)
            {
                if (!(I is Ammo))
                {
                    Seller.Items.Add(I);
                }
            }
            
            //Adds the Items that were going to be sold to the Seller back to the Buyer's Inventory
            foreach (Item I in BuyingItems)
            {
                Buyer.Items.Add(I);
            }
            Done = true;
        }
    }
}
