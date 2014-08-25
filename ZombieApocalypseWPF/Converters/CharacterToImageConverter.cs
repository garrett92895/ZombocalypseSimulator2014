using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZombieApocalypseSimulator.Models.Characters;
using ZombieApocalypseSimulator.Models.Characters.Classes;

namespace ZombieApocalypseWPF.Converters
{
    public class CharacterToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Character c = (Character)value;

            Uri uri = null;

            if (c is Zed)
            {
                if (c is Tank)
                    uri = new Uri("Images/CharacterImages/Tank.png", UriKind.Relative);
                else if (c is Sloucher)
                    uri = new Uri("Images/CharacterImages/Sloucher.png", UriKind.Relative);
                else if (c is Shank)
                    uri = new Uri("Images/CharacterImages/Shank.png", UriKind.Relative);
                else if (c is FastAttack)
                    uri = new Uri("Images/CharacterImages/FastAttack.png", UriKind.Relative);
                
            }
            else if (c is Player)
            {
                if (c is Bruiser)
                    uri = new Uri("Images/CharacterImages/Bruiser.png", UriKind.Relative);
                else if (c is Engineer)
                    uri = new Uri("Images/CharacterImages/Engineer.png", UriKind.Relative);
                else if (c is Fighter)
                    uri = new Uri("Images/CharacterImages/Fighter.png", UriKind.Relative);
                else if (c is HalfZombie)
                    uri = new Uri("Images/CharacterImages/HalfZombie.png", UriKind.Relative);
                else if (c is Medic)
                    uri = new Uri("Images/CharacterImages/Medic.png", UriKind.Relative);
                else if (c is Rifleman)
                    uri = new Uri("Images/CharacterImages/Rifleman.png", UriKind.Relative);
            }

            if (uri == null)
                return Brushes.Firebrick;

            ImageSource i = new BitmapImage(uri);
            return new ImageBrush(i);

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
