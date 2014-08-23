using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;

namespace ZombieApocalypseWPF.Converters
{
    public class CharacterToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Character c = (Character)value;
            string ret = "";

            if (c is Zed)
            {
                if (c is Tank)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\Tank.png";
                else if (c is Sloucher)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\Sloucher.png";
                else if (c is Shank)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\Shank.png";
                else if (c is FastAttack)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\FastAttack.png";
                
            }
            else
            {
                if (c is Bruiser)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharaterImages\\Bruiser.png";
                else if (c is Engineer)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\Engineer.png";
                else if (c is Fighter)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\Fighter.png";
                else if (c is HalfZombie)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\HalfZombie.png";
                else if (c is Medic)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\Medic.png";
                else if (c is Rifleman)
                    ret = "C:\\Users\\jcampbell\\Documents\\GitHub\\ZombocalypseSimulator2014\\ZombieApocalypseWPF\\Images\\CharacterImages\\Rifleman.png";
            }


            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
