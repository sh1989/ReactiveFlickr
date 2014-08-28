using Splat;
using System;
using System.Windows.Data;

namespace ReactiveFlickr.Desktop
{
    public class BitmapSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var bitmap = (IBitmap)value;
            return bitmap.ToNative();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
