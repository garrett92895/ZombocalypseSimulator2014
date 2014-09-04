using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZombieApocalypseSimulator;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for TradeWindow.xaml
    /// </summary>
    public partial class TradeWindow : Window
    {
        public Transaction Exchange { get; set; }
        private bool SellerIsTrader;

        public int BuyerHandgunAmmo { get; private set; }
        public int BuyerRifleAmmo { get; private set; }
        public int BuyerShotgunAmmo { get; private set; }

        public TradeWindow(Player Buyer, Player Seller)
        {
            Exchange = new Transaction(Buyer, Seller);

            for(int i = 0; i < Buyer.Items.Count; i++)
            {
                if (Buyer.Items.ElementAt(i) is Ammo)
                {
                    Ammo A = (Ammo)Buyer.Items.ElementAt(i);
                    switch (A.AmmoType)
                    {
                        case AmmoType.Handgun: BuyerHandgunAmmo++; break;
                        case AmmoType.Rifle: BuyerRifleAmmo++; break;
                        case AmmoType.Shotgun: BuyerShotgunAmmo++; break;
                    }
                }
            }
            SellerIsTrader = Seller is Trader;
            this.DataContext = this;
            InitializeComponent();
            SetMoneyDisplay();
        }

        /// <summary>
        /// If an item in BuyerItems list is clicked, will move it to the SellingItems list in the Transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyerItems_Selected(object sender, RoutedEventArgs e)
        {
            
            Item I = (Item)BuyerItems.SelectedValue;
            if (I != null)
            {
                int Price = (SellerIsTrader) ? ((Trader)Exchange.Seller).SellPrice(I) : 0;
                Exchange.SellItem(I, Price);
                SetMoneyDisplay();
            }
        }

        /// <summary>
        /// If an item in ItemsToBeSold list is clicked, will move it to the Buyer's list in the Transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsToBeSold_Selected(object sender, RoutedEventArgs e)
        {
            Item I = (Item)ItemsToBeSold.SelectedValue;
            if (I != null)
            {
                int Price = (SellerIsTrader) ? ((Trader)Exchange.Seller).SellPrice(I) : 0;
                Exchange.UnSellItem(I, Price);
                SetMoneyDisplay();
            }
        }

        /// <summary>
        /// If an item in ItemsToBeBought list is clicked, will move it to the Seller's list in the Transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsToBeBought_Selected(object sender, RoutedEventArgs e)
        {
            Item I = (Item)ItemsToBeBought.SelectedValue;
            if (I != null)
            {
                int Price = (SellerIsTrader) ? ((Trader)Exchange.Seller).PurchasePrice(I) : 0;
                Exchange.UnPurchaseItem(I, Price);
                SetMoneyDisplay();
            }
        }

        /// <summary>
        /// If an item in SellerItems list is clicked, will move it to the BuyingItems list in the Transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellerItems_Selected(object sender, RoutedEventArgs e)
        {
            Item I = (Item)SellerItems.SelectedValue;
            if (I != null)
            {
                int Price = (SellerIsTrader) ? ((Trader)Exchange.Seller).PurchasePrice(I) : 0;
                Exchange.PurchaseItem(I, Price);
                SetMoneyDisplay();
            }
        }

        /// <summary>
        /// Sets the Money trade labels to the proper values
        /// </summary>
        private void SetMoneyDisplay()
        {
            BuyerMoney.Content = Exchange.Buyer.Money + Exchange.BuyerMoneyChange;
            BuyerMoneyChange.Content = Exchange.BuyerMoneyChange;
            if (Exchange.Seller.Money + Exchange.SellerMoneyChange > 0)
            {
                SellerMoney.Content = Exchange.Seller.Money + Exchange.SellerMoneyChange;
            }
            else
            {
                SellerMoney.Content = int.MaxValue;
            }
        }

        /// <summary>
        /// Cancels the Transaction, reseting everything to the way it was before
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetTrade_Click(object sender, RoutedEventArgs e)
        {
            Exchange.CancelTransaction();
            Exchange.Done = false;
            BuyingHandgunAmmo.Text = "0";
            BuyingRifleAmmo.Text = "0";
            BuyingShotgunAmmo.Text = "0";
            SellingHandgunAmmo.Text = "0";
            SellingRifleAmmo.Text = "0";
            SellingShotgunAmmo.Text = "0";
            SetMoneyDisplay();
        }

        /// <summary>
        /// Finalizes the trade and exits the trade screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishTrade_Click(object sender, RoutedEventArgs e)
        {
            Exchange.FinishTransaction();
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Exchange.CancelTransaction();
            base.OnClosing(e);
        }

        /// <summary>
        /// Purchases and UnPurchases Handgun Ammo from the Trader as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyingHandgunAmmo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Input = -1;
            int.TryParse(BuyingHandgunAmmo.Text, out Input);
            int Diff = Input - Exchange.SellingHandgunAmmo;
            if (Input < 0 || !BuyAmmoType(AmmoType.Handgun, Diff))
            {
                BuyingHandgunAmmo.Text = "0";
            }
            SetMoneyDisplay();
        }

        /// <summary>
        /// Sells and UnSells Handgun Ammo to the Trader as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellingHandgunAmmo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Input = -1;
            int.TryParse(SellingHandgunAmmo.Text, out Input);
            if (Input < 0)
            {
                SellingHandgunAmmo.Text = "0";
            }
            else if (Input > BuyerHandgunAmmo)
            {
                SellingHandgunAmmo.Text = BuyerHandgunAmmo.ToString();
            }
            else
            {
                int Diff = Input - Exchange.SellingHandgunAmmo;
                if (Diff <= BuyerHandgunAmmo)
                {
                    SellAmmoType(AmmoType.Handgun, Diff);
                }
            }
            SetMoneyDisplay();
        }

        /// <summary>
        /// Sells and UnSells Rifle Ammo to the Trader as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellingRifleAmmo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Input = -1;
            int.TryParse(SellingRifleAmmo.Text, out Input);
            if (Input < 0)
            {
                SellingRifleAmmo.Text = "0";
            }
            else if (Input > BuyerRifleAmmo)
            {
                SellingRifleAmmo.Text = BuyerRifleAmmo.ToString();
            }
            else
            {
                int Diff = Input - Exchange.SellingRifleAmmo;
                if (Diff <= BuyerRifleAmmo)
                {
                    SellAmmoType(AmmoType.Rifle, Diff);
                }
            }
            SetMoneyDisplay();
        }

        /// <summary>
        /// Purchases and UnPurchases Rifle Ammo from the Trader as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyingRifleAmmo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Input = -1;
            int.TryParse(BuyingRifleAmmo.Text, out Input);
            int Diff = Input - Exchange.SellingRifleAmmo;
            if (Input < 0 || !BuyAmmoType(AmmoType.Rifle, Diff))
            {
                BuyingRifleAmmo.Text = "0";
            }
            SetMoneyDisplay();
        }

        /// <summary>
        /// Sells and UnSells Rifle Ammo to the Trader as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellingShotgunAmmo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Input = -1;
            int.TryParse(SellingShotgunAmmo.Text, out Input);
            if (Input < 0)
            {
                SellingShotgunAmmo.Text = "0";
            }
            else if (Input > BuyerShotgunAmmo)
            {
                SellingShotgunAmmo.Text = BuyerShotgunAmmo.ToString();
            }
            else
            {
                int Diff = Input - Exchange.SellingShotgunAmmo;
                if (Diff <= BuyerShotgunAmmo)
                {
                    SellAmmoType(AmmoType.Shotgun, Diff);
                }
            }
            SetMoneyDisplay();
        }

        /// <summary>
        /// Purchases and UnPurchases Rifle Ammo from the Trader as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyingShotgunAmmo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Input = -1;
            int.TryParse(BuyingShotgunAmmo.Text, out Input);
            int Diff = Input - Exchange.SellingShotgunAmmo;
            if (Input < 0 || !BuyAmmoType(AmmoType.Shotgun, Diff))
            {
                BuyingShotgunAmmo.Text = "0";
            }
            SetMoneyDisplay();
        }

        /// <summary>
        /// Helper Method to facilitate purchasing and unpurchasing of given AmmoTypes
        /// Returns true if the amount of ammo was able to be succesfully purchased.
        /// Will return false if the Buyer does not have enough money to pay for the amount of ammo or if ammo was UnPurchased
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Input"></param>
        private bool BuyAmmoType(AmmoType TypeOfAmmo, int Diff)
        {
            Trader T = (Trader)Exchange.Seller;
            if (Diff > 0)
            {
                return Exchange.PurchaseAmmo(TypeOfAmmo, Diff, T.PurchaseAmmoCost(TypeOfAmmo, Diff));
            }
            else if (Diff < 0)
            {
                Exchange.UnPurchaseAmmo(TypeOfAmmo, Math.Abs(Diff), T.PurchaseAmmoCost(TypeOfAmmo, Math.Abs(Diff)));
            }
            return true;
        }

        /// <summary>
        /// Helper Method to facilitate selling and unselling of given AmmoTypes
        /// </summary>
        /// <param name="TypeOfAmmo"></param>
        /// <param name="Input"></param>
        private void SellAmmoType(AmmoType TypeOfAmmo, int Diff)
        {
            Trader T = (Trader)Exchange.Seller;
            if (Diff > 0)
            {
                Exchange.SellAmmo(TypeOfAmmo, Diff, T.PurchaseAmmoCost(TypeOfAmmo, Diff));
            }
            else if (Diff < 0)
            {
                Exchange.UnSellAmmo(TypeOfAmmo, Math.Abs(Diff), new Ammo(TypeOfAmmo).Value * Math.Abs(Diff));
            }
        }
    }
}
