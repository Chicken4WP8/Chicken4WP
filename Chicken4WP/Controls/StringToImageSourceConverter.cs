using System;
using System.Globalization;
using System.Windows.Data;
using Chicken4WP.Services;

namespace Chicken4WP.Controls
{
    public class StringToImageSourceConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageCacheService.GetImageFromUrl(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
