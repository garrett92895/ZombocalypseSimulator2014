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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZombieApocalypseSimulator;
using ZombieApocalypseWPF.Windows;

namespace ZombieApocalypseWPF.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        private MainWindow _w;
        public string LastSliderDecremented;
        public MainWindow w
        {
            get
            {
                return _w;
            }
            set
            {
                this._w = value;
                this.DataContext = w;
            }
        }
        public SettingsControl()
        {
            InitializeComponent();
            LastSliderDecremented = "";
            this.DataContext = w;
        }

		public void init()
		{
			GameModesCombo.SelectedIndex = GetIndexFromSettings();
		}

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ItemSelected = GameModesCombo.SelectedIndex;

            if (w != null)
            {
                if (w.settings != null)
                {
                    if (ItemSelected == 0)
                    {
                        w.settings.CanEdit = false;
                        w.settings.EnforceTurnOrder = true;
						GameModesCombo.SelectedIndex = 0;
                    }
                    else if (ItemSelected == 1)
                    {
                        w.settings.CanEdit = true;
                        w.settings.EnforceTurnOrder = true;
						GameModesCombo.SelectedIndex = 1;
                    }
                    else
                    {
                        w.settings.CanEdit = true;
                        w.settings.EnforceTurnOrder = false;
						GameModesCombo.SelectedIndex = 2;
                    }
                }
            }
        }

        private void Weapon_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if(LastSliderDecremented.Equals(""))
            //{
            //    LastSliderDecremented = "Ammo";
            //}

            //if (SumValue() > 100)
            //{
            //    if (LastSliderDecremented.Equals("Ammo"))
            //    {
            //        if(SpareParts.Value > 0)
            //        {
            //            s.SparePartDropRate--;
            //            LastSliderDecremented = "SpareParts";
            //        }
            //        else
            //        {
            //            if(Health.Value > 0)
            //            {
            //                s.HealthPackDropRate--;
            //                LastSliderDecremented = "Health";
            //            }
            //            else
            //            {
            //                s.AmmoDropRate--;
            //                LastSliderDecremented = "Ammo";
            //            }
            //        }
            //    }
            //    else if (LastSliderDecremented.Equals("SpareParts") && SpareParts.Value > 0)
            //    {
            //        if (Health.Value > 0)
            //        {
            //            s.HealthPackDropRate--;
            //            LastSliderDecremented = "Health";
            //        }
            //        else
            //        {
            //            if (Ammo.Value > 0)
            //            {
            //                s.AmmoDropRate--;
            //                LastSliderDecremented = "Ammo";
            //            }
            //            else
            //            {
            //                s.SparePartDropRate--;
            //                LastSliderDecremented = "SpareParts";
            //            }
            //        }
            //    }
            //    else if(LastSliderDecremented.Equals("Health") && Health.Value > 0)
            //    {
            //        if (Ammo.Value > 0)
            //        {
            //            s.AmmoDropRate--;
            //            LastSliderDecremented = "Ammo";
            //        }
            //        else
            //        {
            //            if (SpareParts.Value > 0)
            //            {
            //                s.SparePartDropRate--;
            //                LastSliderDecremented = "SpareParts";
            //            }
            //            else
            //            {
            //                s.HealthPackDropRate--;
            //                LastSliderDecremented = "Health";
            //            }
            //        }
            //    }
            //}
            if(SumValue() > 100)
            ReduceSlider(NextAvailable("Weapon"));
        }

        private void Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SumValue() > 100)
            ReduceSlider(NextAvailable("Health"));
        }

        private void Ammo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SumValue() > 100)
            ReduceSlider(NextAvailable("Ammo"));

        }

        private void SpareParts_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SumValue() > 100)
            ReduceSlider(NextAvailable("SpareParts"));

        }

        private void ReduceSlider(string NextAvailableReduce)
        {
            if (NextAvailableReduce != null)
            {
                if (NextAvailableReduce.Equals("Weapon"))
                {
                    w.settings.WeaponDropRate--;
                }
                else if (NextAvailableReduce.Equals("Health"))
                {
                    w.settings.HealthPackDropRate--;
                }
                else if (NextAvailableReduce.Equals("SpareParts"))
                {
                    w.settings.SparePartDropRate--;
                }
                else if (NextAvailableReduce.Equals("Ammo"))
                {
                    w.settings.AmmoDropRate--;
                }
            }
        }

        private string NextAvailable(string exclusion)
        {
            string Next = null;

            if(!exclusion.Equals("Weapon") && Weapon.Value > 0)
            {
                Next = "Weapon";
            }
            else if (!exclusion.Equals("Health") && Health.Value > 0)
            {
                Next = "Health";
            }
            else if (!exclusion.Equals("SpareParts") && SpareParts.Value > 0)
            {
                Next = "SpareParts";
            }
            else if (!exclusion.Equals("Ammo") && Ammo.Value > 0)
            {
                Next = "Ammo";
            }

            return Next;
        }

		private int GetIndexFromSettings()
		{
			int Index = 0;

			if (w != null && w.settings != null)
			{
				if (w.settings.CanEdit)
				{
					if (w.settings.EnforceTurnOrder)
					{
						Index = 1;
					}
					else
					{
						Index = 2;
					}
				}
			}

			return Index;
		}

        private int SumValue()
        {
            int value = (int)(Weapon.Value + Ammo.Value + SpareParts.Value + Health.Value);
            Console.WriteLine(value);
            return value;
        }

        private void HordeSettings_Click(object sender, RoutedEventArgs e)
        {
			HordeSettings NewSettings = new HordeSettings(w.c.HordeMode);
			NewSettings.Show();
        }
    }
}
