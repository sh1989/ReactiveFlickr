using Splat;
using System;
using Windows.UI.Xaml.Data;

namespace ReactiveFlickr.Mobile
{
    public class BitmapSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bitmap = (IBitmap)value;
            return bitmap.ToNative();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
