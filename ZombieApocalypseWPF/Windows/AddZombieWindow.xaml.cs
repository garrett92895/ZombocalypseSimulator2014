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
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddZombieWindow.xaml
    /// </summary>
    public partial class AddZombieWindow : Window
    {
        public AddZombieWindow()
        {
            InitializeComponent();

            NewZombieControl.c = new Tank();

            NewZombieControl.Level_Up_Button.Click += Level_Up_Button_Click;
            NewZombieControl.Level_Down_Button.Click += Level_Down_Button_Click;
            NewZombieControl.AddItemButton.Click += AddItemButton_Click;

            List<String> classes = new List<string>
            {
                "Tank", "Fast-Attack", "Sloucher", "Shank"
            };

            ZombieClass.ItemsSource = classes;

            ZombieClass.SelectedIndex = 0;

        }

        private void Level_Up_Button_Click(object sender, RoutedEventArgs e)
         {
             NewZombieControl.c.LevelUp();
         }

        private void Level_Down_Button_Click(object sender, RoutedEventArgs e)
         {
             NewZombieControl.c.LevelDown();
         }

        private Item AddItem()
        {
            AddItemWindow aiw = new AddItemWindow();
            aiw.ShowDialog();
            Item i = MainWindow.newItem;
            MainWindow.newItem = null;
            return i;
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            Item i = AddItem();

            if (i != null)
                NewZombieControl.c.Items.Add(i);

        }

        private void ZombieClass_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            switch (ZombieClass.SelectedIndex)
            {
                case 0:
                    NewZombieControl.c = new Tank
                    {
                        ArmorRating = NewZombieControl.c.ArmorRating,
                        Health = NewZombieControl.c.Health,
                        IntelligenceQuotient = NewZombieControl.c.IntelligenceQuotient,
                        Items = NewZombieControl.c.Items,
                        Level = NewZombieControl.c.Level,
                        MaxHealth = NewZombieControl.c.MaxHealth,
                        MaxSDC = NewZombieControl.c.MaxSDC,
                        MentalAffinity = NewZombieControl.c.MentalAffinity,
                        MentalEndurance = NewZombieControl.c.MentalEndurance,
                        Money = NewZombieControl.c.Money,
                        Name = NewZombieControl.c.Name,
                        PhysicalBeauty = NewZombieControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewZombieControl.c.PhysicalEndurance,
                        PhysicalProwess = NewZombieControl.c.PhysicalProwess,
                        PhysicalStrength = NewZombieControl.c.PhysicalStrength,
                        SDC = NewZombieControl.c.SDC,
                        Speed = NewZombieControl.c.Speed
                        
                    };
                    break;
                case 1:
                    NewZombieControl.c = new FastAttack
                    {
                        ArmorRating = NewZombieControl.c.ArmorRating,
                        Health = NewZombieControl.c.Health,
                        IntelligenceQuotient = NewZombieControl.c.IntelligenceQuotient,
                        Items = NewZombieControl.c.Items,
                        Level = NewZombieControl.c.Level,
                        MaxHealth = NewZombieControl.c.MaxHealth,
                        MaxSDC = NewZombieControl.c.MaxSDC,
                        MentalAffinity = NewZombieControl.c.MentalAffinity,
                        MentalEndurance = NewZombieControl.c.MentalEndurance,
                        Money = NewZombieControl.c.Money,
                        Name = NewZombieControl.c.Name,
                        PhysicalBeauty = NewZombieControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewZombieControl.c.PhysicalEndurance,
                        PhysicalProwess = NewZombieControl.c.PhysicalProwess,
                        PhysicalStrength = NewZombieControl.c.PhysicalStrength,
                        SDC = NewZombieControl.c.SDC,
                        Speed = NewZombieControl.c.Speed

                    };
                    break;
                case 2:
                    NewZombieControl.c = new Sloucher
                    {
                        ArmorRating = NewZombieControl.c.ArmorRating,
                        Health = NewZombieControl.c.Health,
                        IntelligenceQuotient = NewZombieControl.c.IntelligenceQuotient,
                        Items = NewZombieControl.c.Items,
                        Level = NewZombieControl.c.Level,
                        MaxHealth = NewZombieControl.c.MaxHealth,
                        MaxSDC = NewZombieControl.c.MaxSDC,
                        MentalAffinity = NewZombieControl.c.MentalAffinity,
                        MentalEndurance = NewZombieControl.c.MentalEndurance,
                        Money = NewZombieControl.c.Money,
                        Name = NewZombieControl.c.Name,
                        PhysicalBeauty = NewZombieControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewZombieControl.c.PhysicalEndurance,
                        PhysicalProwess = NewZombieControl.c.PhysicalProwess,
                        PhysicalStrength = NewZombieControl.c.PhysicalStrength,
                        SDC = NewZombieControl.c.SDC,
                        Speed = NewZombieControl.c.Speed

                    };
                    break;
                case 3:
                    NewZombieControl.c = new Shank
                    {
                        ArmorRating = NewZombieControl.c.ArmorRating,
                        Health = NewZombieControl.c.Health,
                        IntelligenceQuotient = NewZombieControl.c.IntelligenceQuotient,
                        Items = NewZombieControl.c.Items,
                        Level = NewZombieControl.c.Level,
                        MaxHealth = NewZombieControl.c.MaxHealth,
                        MaxSDC = NewZombieControl.c.MaxSDC,
                        MentalAffinity = NewZombieControl.c.MentalAffinity,
                        MentalEndurance = NewZombieControl.c.MentalEndurance,
                        Money = NewZombieControl.c.Money,
                        Name = NewZombieControl.c.Name,
                        PhysicalBeauty = NewZombieControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewZombieControl.c.PhysicalEndurance,
                        PhysicalProwess = NewZombieControl.c.PhysicalProwess,
                        PhysicalStrength = NewZombieControl.c.PhysicalStrength,
                        SDC = NewZombieControl.c.SDC,
                        Speed = NewZombieControl.c.Speed

                    };
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.NewCharacter = NewZombieControl.c;
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
