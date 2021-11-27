using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MemeFolderN.MFViews.Wpf.Converters
{
    public class ItemsCountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
