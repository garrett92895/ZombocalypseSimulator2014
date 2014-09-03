using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseSimulator
{
    public class Transaction
    {
        /// <summary>
        /// Items that are being sold to the Buyer
        /// </summary>
        public ObservableCollection<Item> SellingItems { get; set; }

        /// <summary>
        /// Handgun Ammo that is being sold to the Buyer
        /// </summary>
        public int SellingHandgunAmmo { get; set; }

        /// <summary>
        /// Rifle Ammo that is being sold to the Buyer
        /// </summary>
        public int SellingRifleAmmo { get; set; }

        /// <summary>
        /// Shotgun Ammo that is being sold to the Buyer
        /// </summary>
        public int SellingShotgunAmmo { get; set; }

        /// <summary>
        /// The amount of money the Seller gains or losses from the Transaction
        /// </summary>
        public int SellerMoneyChange { get; set; }

        /// <summary>
        /// Items that are being offered by the Buyer
        /// </summary>
        public ObservableCollection<Item> BuyingItems { get; set; }

        /// <summary>
        /// Handgun Ammo that is being offered by the Buyer
        /// </summary>
        public int BuyingHandgunAmmo { get; set; }

        /// <summary>
        /// Ammo that is being offered by the Buyer
        /// </summary>
        public int BuyingRifleAmmo { get; set; }

        /// <summary>
        /// Ammo that is being offered by the Buyer
        /// </summary>
        public int BuyingShotgunAmmo { get; set; }

        /// <summary>
        /// The amount of money the Buyer gains or losses from the Transaction
        /// </summary>
        public int BuyerMoneyChange { get; set;}

        /// <summary>
        /// Character that initiated the Transaction
        /// </summary>
        public Player Buyer { get; set; }

        /// <summary>
        /// Character that is selling to the Buyer
        /// </summary>
        public Player Seller { get; set; }

        //Flag which states when this Transaction is done
        public bool Done { get; set; }

        public Transaction(Player NewBuyer, Player NewSeller)
        {
            Buyer = NewBuyer;
            Seller = NewSeller;

            BuyingItems = new ObservableCollection<Item>();
            BuyingHandgunAmmo = 0;
            BuyingRifleAmmo = 0;
            BuyingShotgunAmmo = 0;

            SellingItems = new ObservableCollection<Item>();
            SellingHandgunAmmo = 0;
            SellingRifleAmmo = 0;
            SellingShotgunAmmo = 0;

            BuyerMoneyChange = 0;
            SellerMoneyChange = 0;
            Done = false;
        }

        /// <summary>
        /// Removes the given Items from the Seller's List and Adds it to the SellingItems list
        /// Also adjusts SellerMoneyChange and BuyerMoneyChange based on the given Price
        /// Returns true if the item was succesfully Purchased.
        /// Will return false if the Buyer does not have enough money to purchase the given Item at the given Price
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Price"></param>
        public void PurchaseItem(Item I, int Price)
        {
            if (Buyer.Money + (BuyerMoneyChange - Price) > 0)
            {
                Seller.Items.Remove(I);
                SellerMoneyChange += Price;
                BuyerMoneyChange -= Price;
                SellingItems.Add(I);
            }
        }

        /// <summary>
        /// Used whenever a specific items is no longer being purchased
        /// Adds the given Items to the Seller's List and removes it from the SellingItems list
        /// Also adjusts SellerMoneyChange and BuyerMoneyChange based on the given Price
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Price"></param>
        public void UnPurchaseItem(Item I, int Price)
        {
            if (SellingItems.Contains(I))
            {
                SellingItems.Remove(I);
                Seller.Items.Add(I);
                SellerMoneyChange -= Price;
                BuyerMoneyChange += Price;
            }
        }

        /// <summary>
        /// Used to purchase ammo from the Trader
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Amount"></param>
        /// <param name="Price"></param>
        public bool PurchaseAmmo(AmmoType TypeOfAmmo, int Amount, int Price)
        {
            if (Buyer.Money + (BuyerMoneyChange - Price) > 0)
            {
                switch (TypeOfAmmo)
                {
                    case AmmoType.Handgun: SellingHandgunAmmo += Amount; break;
                    case AmmoType.Rifle: SellingRifleAmmo += Amount; break;
                    case AmmoType.Shotgun: SellingShotgunAmmo += Amount; break;
                }
                BuyerMoneyChange -= Price;
                SellerMoneyChange += Price;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Used to cancel a purchase of ammo from the Trader
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Amount"></param>
        /// <param name="Price"></param>
        public void UnPurchaseAmmo(AmmoType TypeOfAmmo, int Amount, int Price)
        {
            switch (TypeOfAmmo)
            {
                case AmmoType.Handgun: SellingHandgunAmmo -= Amount; break;
                case AmmoType.Rifle: SellingRifleAmmo -= Amount; break;
                case AmmoType.Shotgun: SellingShotgunAmmo -= Amount; break;
            }
            BuyerMoneyChange += Price;
            SellerMoneyChange -= Price;
        }

        /// <summary>
        /// Removes the given Item from the Buyer's List and adds it to the BuyingItems list
        /// Also adjust SellerMoneyChange and BuyerMoneyChange based on the given price
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Price"></param>
        public void SellItem(Item I, int Price)
        {
            Buyer.Items.Remove(I);
            BuyerMoneyChange += Price;
            SellerMoneyChange -= Price;
            BuyingItems.Add(I);
        }

        /// <summary>
        /// Used whenever a specific items is no longer being sold
        /// Adds the given Items to the Buyer's List and removes it from the BuyingItems list
        /// Also adjusts SellerMoneyChange and BuyerMoneyChanged based on the given price
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Price"></param>
        public void UnSellItem(Item I, int Price)
        {
            if (BuyingItems.Contains(I))
            {
                BuyingItems.Remove(I);
                Buyer.Items.Add(I);
                BuyerMoneyChange -= Price;
                SellerMoneyChange += Price;
            }
        }

        /// <summary>
        /// Used to sell ammo to the Trader
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Amount"></param>
        public void SellAmmo(AmmoType TypeOfAmmo, int Amount, int Price)
        {
            switch (TypeOfAmmo)
            {
                case AmmoType.Handgun: BuyingHandgunAmmo += Amount; break;
                case AmmoType.Rifle: BuyingRifleAmmo += Amount; break;
                case AmmoType.Shotgun: BuyingShotgunAmmo += Amount; break;
            }
            BuyerMoneyChange += Price;
            SellerMoneyChange -= Price;
        }

        /// <summary>
        /// Used to cancel selling ammo to the Trader
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Amount"></param>
        /// <param name="Price"></param>
        public void UnSellAmmo(AmmoType TypeOfAmmo, int Amount, int Price)
        {
            switch (TypeOfAmmo)
            {
                case AmmoType.Handgun: BuyingHandgunAmmo -= Amount; break;
                case AmmoType.Rifle: BuyingRifleAmmo -= Amount; break;
                case AmmoType.Shotgun: BuyingShotgunAmmo -= Amount; break;
            }

            BuyerMoneyChange -= Price;
            SellerMoneyChange += Price;
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
            //Adds the Ammo bought to the Buyer's Inventory
            for (int i = 0; i < SellingHandgunAmmo; i++)
            {
                Buyer.Items.Add(new Ammo(AmmoType.Handgun));
            }
            for (int i = 0; i < SellingRifleAmmo; i++)
            {
                Buyer.Items.Add(new Ammo(AmmoType.Rifle));
            }
            for (int i = 0; i < SellingShotgunAmmo; i++)
            {
                Buyer.Items.Add(new Ammo(AmmoType.Shotgun));
            }

            //Adds the Items sold to the Seller to the Seller's Inventory
            foreach (Item I in BuyingItems)
            {
                Seller.Items.Add(I);
            }

            //Removes the Ammo sold from the Buyer's Inventory
            if (BuyingHandgunAmmo > 0 || BuyingRifleAmmo > 0 || BuyingShotgunAmmo > 0)
            {
                ObservableCollection<Ammo> RemovedAmmo = new ObservableCollection<Ammo>();
                foreach (Ammo A in Buyer.Items)
                {
                    if (A.AmmoType == AmmoType.Handgun && BuyingHandgunAmmo > 0)
                    {
                        RemovedAmmo.Add(A);
                    }
                    if (A.AmmoType == AmmoType.Rifle && BuyingRifleAmmo > 0)
                    {
                        RemovedAmmo.Add(A);
                    }
                    if (A.AmmoType == AmmoType.Shotgun && BuyingShotgunAmmo > 0)
                    {
                        RemovedAmmo.Add(A);
                    }
                }
                foreach (Ammo A in RemovedAmmo)
                {
                    Buyer.Items.Remove(A);
                }
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
                Seller.Items.Add(I);
            }
            SellingItems.Clear();
            
            //Adds the Items that were going to be sold to the Seller back to the Buyer's Inventory
            foreach (Item I in BuyingItems)
            {
                Buyer.Items.Add(I);
            }
            BuyingItems.Clear();

            BuyingHandgunAmmo = 0;
            BuyingRifleAmmo = 0;
            BuyingShotgunAmmo = 0;
            SellingHandgunAmmo = 0;
            SellingRifleAmmo = 0;
            SellingShotgunAmmo = 0;
            BuyerMoneyChange = 0;
            SellerMoneyChange = 0;
            Done = true;
        }
    }
}
