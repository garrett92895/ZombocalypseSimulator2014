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
using ZombieApocalypseSimulator.Models.Enums;
using ZombieApocalypseSimulator.Models.Items;
using ZombieApocalypseSimulator.Models.Items.Enums;

namespace ZombieApocalypseWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        Item I;
        List<string> ItemTypes = new List<string>
        {
            
               "Ranged Weapon","Melee Weapon","Health","SparePart","Trap"
            
        };

        public AddItemWindow()
        {
            InitializeComponent();
            ItemType.ItemsSource = ItemTypes;
        }

        

        private void ItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExtraFields.Children.Clear();

            switch ((string)ItemType.SelectedValue)
            {
                case "Ranged Weapon" :

                    ExtraFields.Children.Add(new Label { Content = "Condition: ", HorizontalAlignment = HorizontalAlignment.Right});
                    ExtraFields.Children.Add(new TextBox {  });

                    ExtraFields.Children.Add(new Label { Content = "Damage: ", HorizontalAlignment = HorizontalAlignment.Right});
                    ExtraFields.Children.Add(new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new Label { Content = "d" });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new Label { Content = "x" });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new Label { Content = "+" });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });

                    ExtraFields.Children.Add(new Label { Content = "Ignores Armour: ", HorizontalAlignment = HorizontalAlignment.Right });
                    ExtraFields.Children.Add(new CheckBox { VerticalAlignment = VerticalAlignment.Center });

                    ExtraFields.Children.Add(new Label { Content = "Ranged Weapon Type: ", HorizontalAlignment = HorizontalAlignment.Right });
                    List<RangedWeaponType> rwts = new List<RangedWeaponType> { RangedWeaponType.Handgun, RangedWeaponType.Shotgun, RangedWeaponType.Rifle  };
                    ExtraFields.Children.Add(new ComboBox { ItemsSource = rwts});
                    

                    break;
                case "Melee Weapon" :
                    
                    ExtraFields.Children.Add(new Label { Content = "Condition: ", HorizontalAlignment = HorizontalAlignment.Right});
                    ExtraFields.Children.Add(new TextBox {  });

                    ExtraFields.Children.Add(new Label { Content = "Damage: ", HorizontalAlignment = HorizontalAlignment.Right});
                    ExtraFields.Children.Add(new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    });

                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new Label { Content = "d" });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new Label { Content = "x" });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new Label { Content = "+" });
                    ((StackPanel)ExtraFields.Children[3]).Children.Add(new TextBox { });

                    ExtraFields.Children.Add(new Label { Content = "Ignores Armour: ", HorizontalAlignment = HorizontalAlignment.Right });
                    ExtraFields.Children.Add(new CheckBox { VerticalAlignment = VerticalAlignment.Center });

                    ExtraFields.Children.Add(new Label { Content = "Melee Weapon Type: ", HorizontalAlignment = HorizontalAlignment.Right });
                    List<MeleeWeaponType> mwts = new List<MeleeWeaponType> { MeleeWeaponType.Blunt, MeleeWeaponType.Pierce, MeleeWeaponType.Slash  };
                    ExtraFields.Children.Add(new ComboBox { ItemsSource = mwts});
                    break;

                case "Health" :
                    
                    ExtraFields.Children.Add(new Label { Content = "HP healed: ", HorizontalAlignment = HorizontalAlignment.Right});
                    ExtraFields.Children.Add(new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    });

                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "d" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "x" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "+" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });

                    break;

                case "SparePart" :
                    
                    ExtraFields.Children.Add(new Label { Content = "Condition healed: ", HorizontalAlignment = HorizontalAlignment.Right});
                    ExtraFields.Children.Add(new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    });

                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "d" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "x" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "+" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });

                    break;
                case "Trap" :
                    
                    ExtraFields.Children.Add(new Label { Content = "Damage: ", HorizontalAlignment = HorizontalAlignment.Right});
                    ExtraFields.Children.Add(new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    });

                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "d" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "x" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new Label { Content = "+" });
                    ((StackPanel)ExtraFields.Children[1]).Children.Add(new TextBox { });

                    ComboBox cb = new ComboBox();
                    cb.ItemsSource = new List<StatusEffect> { StatusEffect.ArmourBroken, StatusEffect.Bleed, StatusEffect.Crippled, StatusEffect.Infected, StatusEffect.OnFire,
                        StatusEffect.Poison, StatusEffect.PTZD, StatusEffect.Stunned,};

                    ExtraFields.Children.Add(new Label { Content = "Trap Effect: ", HorizontalAlignment = HorizontalAlignment.Right });
                    ExtraFields.Children.Add(cb);

                    break;

                default :
                    break;
            }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {

            Item newI = null;
            try
            {
                switch ((string)ItemType.SelectedValue)
                {
                    case "Ranged Weapon":
                        newI = new RangedWeapon
                        {
                            Condition = int.Parse(((TextBox)ExtraFields.Children[1]).Text),
                            Damage = new DieRoll(
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[0])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[2])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[4])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[6])).Text)
                                ),
                            Description = ItemDescription.Text,
                            IgnoresAR = (bool)((CheckBox)ExtraFields.Children[5]).IsChecked,
                            Name = ItemName.Text,
                            RangedWeaponType = (RangedWeaponType)((ComboBox)ExtraFields.Children[7]).SelectedValue,
                            

                        };
                        break;
                    case "Melee Weapon":
                        newI = new MeleeWeapon
                        {
                            Condition = int.Parse(((TextBox)ExtraFields.Children[1]).Text),
                            Damage = new DieRoll(
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[0])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[2])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[4])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[3]).Children[6])).Text)
                                ),
                            Description = ItemDescription.Text,
                            IgnoresAR = (bool)((CheckBox)ExtraFields.Children[5]).IsChecked,
                            Name = ItemName.Text,
                            MeleeWeaponType = (MeleeWeaponType)((ComboBox)ExtraFields.Children[7]).SelectedValue,
                            
                        };
                        break;

                    case "Health":
                        newI = new Health
                        {

                            Name = ItemName.Text,
                            Description = ItemDescription.Text,
                            
                            AmountHealed = new DieRoll(
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[0])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[2])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[4])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[6])).Text)
                                )

                        };

                        break;
                    case "SparePart":
                        newI = new SparePart
                        {

                            Name = ItemName.Text,
                            Description = ItemDescription.Text,

                            AmountHealed = new DieRoll(
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[0])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[2])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[4])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[6])).Text)
                                )

                        };
                        break;

                    case "Trap":
                        newI = new Trap
                        {

                            Name = ItemName.Text,
                            Description = ItemDescription.Text,

                            Damage = new DieRoll(
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[0])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[2])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[4])).Text),
                                int.Parse(((TextBox)(((StackPanel)ExtraFields.Children[1]).Children[6])).Text)
                                ),

                            StatusEffect = (StatusEffect)((ComboBox)ExtraFields.Children[3]).SelectedValue
                        };
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("You have entered an invalid value.");
                return;
            }

            MainWindow.newItem = newI;

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
