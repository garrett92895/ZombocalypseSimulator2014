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
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        public AddPlayerWindow()
        {
            InitializeComponent();

            NewPlayerControl.c = new Fighter();
            NewPlayerControl.Level_Up_Button.Click += Level_Up_Button_Click;
            NewPlayerControl.Level_Down_Button.Click += Level_Down_Button_Click;
            NewPlayerControl.AddItemButton.Click += AddItemButton_Click;

            List<String> classes = new List<string>
            {
                "Bruiser", "Engineer", "Fighter", "Half-Zombie", "Medic", "Rifleman" 
            };

            PlayerClass.ItemsSource = classes;

            PlayerClass.SelectedIndex = 0;

        }


        private void Level_Up_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPlayerControl.c.LevelUp();
        }

        private void Level_Down_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPlayerControl.c.LevelDown();
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
                NewPlayerControl.c.Items.Add(i);

        }


        private void PlayerClass_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            switch (PlayerClass.SelectedIndex)
            {
                case 0:
                    NewPlayerControl.c = new Bruiser
                    {
                        ArmorRating = NewPlayerControl.c.ArmorRating,
                        Health = NewPlayerControl.c.Health,
                        IntelligenceQuotient = NewPlayerControl.c.IntelligenceQuotient,
                        Items = NewPlayerControl.c.Items,
                        Level = NewPlayerControl.c.Level,
                        MaxHealth = NewPlayerControl.c.MaxHealth,
                        MaxSDC = NewPlayerControl.c.MaxSDC,
                        MentalAffinity = NewPlayerControl.c.MentalAffinity,
                        MentalEndurance = NewPlayerControl.c.MentalEndurance,
                        Money = NewPlayerControl.c.Money,
                        Name = NewPlayerControl.c.Name,
                        PhysicalBeauty = NewPlayerControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewPlayerControl.c.PhysicalEndurance,
                        PhysicalProwess = NewPlayerControl.c.PhysicalProwess,
                        PhysicalStrength = NewPlayerControl.c.PhysicalStrength,
                        SDC = NewPlayerControl.c.SDC,
                        Speed = NewPlayerControl.c.Speed
                        
                    };
                    break;
                case 1:
                    NewPlayerControl.c = new Engineer
                    {
                        ArmorRating = NewPlayerControl.c.ArmorRating,
                        Health = NewPlayerControl.c.Health,
                        IntelligenceQuotient = NewPlayerControl.c.IntelligenceQuotient,
                        Items = NewPlayerControl.c.Items,
                        Level = NewPlayerControl.c.Level,
                        MaxHealth = NewPlayerControl.c.MaxHealth,
                        MaxSDC = NewPlayerControl.c.MaxSDC,
                        MentalAffinity = NewPlayerControl.c.MentalAffinity,
                        MentalEndurance = NewPlayerControl.c.MentalEndurance,
                        Money = NewPlayerControl.c.Money,
                        Name = NewPlayerControl.c.Name,
                        PhysicalBeauty = NewPlayerControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewPlayerControl.c.PhysicalEndurance,
                        PhysicalProwess = NewPlayerControl.c.PhysicalProwess,
                        PhysicalStrength = NewPlayerControl.c.PhysicalStrength,
                        SDC = NewPlayerControl.c.SDC,
                        Speed = NewPlayerControl.c.Speed

                    };
                    break;
                case 2:
                    NewPlayerControl.c = new Fighter
                    {
                        ArmorRating = NewPlayerControl.c.ArmorRating,
                        Health = NewPlayerControl.c.Health,
                        IntelligenceQuotient = NewPlayerControl.c.IntelligenceQuotient,
                        Items = NewPlayerControl.c.Items,
                        Level = NewPlayerControl.c.Level,
                        MaxHealth = NewPlayerControl.c.MaxHealth,
                        MaxSDC = NewPlayerControl.c.MaxSDC,
                        MentalAffinity = NewPlayerControl.c.MentalAffinity,
                        MentalEndurance = NewPlayerControl.c.MentalEndurance,
                        Money = NewPlayerControl.c.Money,
                        Name = NewPlayerControl.c.Name,
                        PhysicalBeauty = NewPlayerControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewPlayerControl.c.PhysicalEndurance,
                        PhysicalProwess = NewPlayerControl.c.PhysicalProwess,
                        PhysicalStrength = NewPlayerControl.c.PhysicalStrength,
                        SDC = NewPlayerControl.c.SDC,
                        Speed = NewPlayerControl.c.Speed

                    };
                    break;
                case 3:
                    NewPlayerControl.c = new HalfZombie
                    {
                        ArmorRating = NewPlayerControl.c.ArmorRating,
                        Health = NewPlayerControl.c.Health,
                        IntelligenceQuotient = NewPlayerControl.c.IntelligenceQuotient,
                        Items = NewPlayerControl.c.Items,
                        Level = NewPlayerControl.c.Level,
                        MaxHealth = NewPlayerControl.c.MaxHealth,
                        MaxSDC = NewPlayerControl.c.MaxSDC,
                        MentalAffinity = NewPlayerControl.c.MentalAffinity,
                        MentalEndurance = NewPlayerControl.c.MentalEndurance,
                        Money = NewPlayerControl.c.Money,
                        Name = NewPlayerControl.c.Name,
                        PhysicalBeauty = NewPlayerControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewPlayerControl.c.PhysicalEndurance,
                        PhysicalProwess = NewPlayerControl.c.PhysicalProwess,
                        PhysicalStrength = NewPlayerControl.c.PhysicalStrength,
                        SDC = NewPlayerControl.c.SDC,
                        Speed = NewPlayerControl.c.Speed

                    };
                    break;
                case 4:
                    NewPlayerControl.c = new Medic
                    {
                        ArmorRating = NewPlayerControl.c.ArmorRating,
                        Health = NewPlayerControl.c.Health,
                        IntelligenceQuotient = NewPlayerControl.c.IntelligenceQuotient,
                        Items = NewPlayerControl.c.Items,
                        Level = NewPlayerControl.c.Level,
                        MaxHealth = NewPlayerControl.c.MaxHealth,
                        MaxSDC = NewPlayerControl.c.MaxSDC,
                        MentalAffinity = NewPlayerControl.c.MentalAffinity,
                        MentalEndurance = NewPlayerControl.c.MentalEndurance,
                        Money = NewPlayerControl.c.Money,
                        Name = NewPlayerControl.c.Name,
                        PhysicalBeauty = NewPlayerControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewPlayerControl.c.PhysicalEndurance,
                        PhysicalProwess = NewPlayerControl.c.PhysicalProwess,
                        PhysicalStrength = NewPlayerControl.c.PhysicalStrength,
                        SDC = NewPlayerControl.c.SDC,
                        Speed = NewPlayerControl.c.Speed

                    };
                    break;
                case 5:
                    NewPlayerControl.c = new Rifleman
                    {
                        ArmorRating = NewPlayerControl.c.ArmorRating,
                        Health = NewPlayerControl.c.Health,
                        IntelligenceQuotient = NewPlayerControl.c.IntelligenceQuotient,
                        Items = NewPlayerControl.c.Items,
                        Level = NewPlayerControl.c.Level,
                        MaxHealth = NewPlayerControl.c.MaxHealth,
                        MaxSDC = NewPlayerControl.c.MaxSDC,
                        MentalAffinity = NewPlayerControl.c.MentalAffinity,
                        MentalEndurance = NewPlayerControl.c.MentalEndurance,
                        Money = NewPlayerControl.c.Money,
                        Name = NewPlayerControl.c.Name,
                        PhysicalBeauty = NewPlayerControl.c.PhysicalBeauty,
                        PhysicalEndurance = NewPlayerControl.c.PhysicalEndurance,
                        PhysicalProwess = NewPlayerControl.c.PhysicalProwess,
                        PhysicalStrength = NewPlayerControl.c.PhysicalStrength,
                        SDC = NewPlayerControl.c.SDC,
                        Speed = NewPlayerControl.c.Speed

                    };
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.NewCharacter = NewPlayerControl.c;
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
