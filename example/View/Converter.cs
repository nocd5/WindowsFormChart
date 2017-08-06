using System;
using System.Globalization;
using System.Windows.Data;

namespace WindowsFormChart.example.View
{
    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value as bool? ?? false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value as bool? ?? false);
        }
    }
}
