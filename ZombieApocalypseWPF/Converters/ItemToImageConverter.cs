using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZombieApocalypseSimulator.Models.Items;

namespace ZombieApocalypseWPF.Converters
{
    public class ItemToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            Uri uri = null;

            if (value is Trap)
                uri = new Uri("Images/ItemImages/Trap.png", UriKind.Relative);
            else if (value is List<Item> && ((List<Item>)value).Count != 0)
                uri = new Uri("Images/ItemImages/Items.png", UriKind.Relative);
            else if (value is ObservableCollection<Item> && ((ObservableCollection<Item>)value).Count != 0)
                uri = new Uri("Images/ItemImages/Items.png", UriKind.Relative);

            if (uri == null)
                return Brushes.Firebrick;

            ImageSource i = new BitmapImage(uri);
            return new ImageBrush(i);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
