﻿using System;
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
using ZombieApocalypseSimulator.Models.Characters;

namespace WpfApplication1.UserControls
{
    /// <summary>
    /// Interaction logic for CharacterControl.xaml
    /// </summary>
    public partial class CharacterControl : UserControl
    {
        private Character _c;
        public Character c 
        {
            get
            {
                return _c;
            }
            set
            {
                _c = value;
                this.DataContext = _c;
                this.InventoryGrid.ItemsSource = _c.Items;
            }
        }
        public CharacterControl()
        {
            InitializeComponent();
            this.DataContext = c;
            this.InventoryGrid.ItemsSource = c.Items;
        }
    }
}
