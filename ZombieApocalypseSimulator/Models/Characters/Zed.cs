using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Items;


namespace ZombieApocalypseSimulator.Models.Characters
{
    public abstract class Zed : Character
    {
        public bool HasAttacked { get; set; }
        public Zed()
        {
            IntelligenceQuotient = 1;
            MentalAffinity = 1;
            MentalEndurance = 1;
            ArmorRating = 14;
            Items = new ObservableCollection<Item>();
            Item RandMoney = new Item();
            RandMoney.Value = DieRoll.RollOne(20);
            RandMoney.Name = "$" + RandMoney.Value;
            Items.Add(RandMoney);
        }

        public override bool HasWeapon()
        {
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}