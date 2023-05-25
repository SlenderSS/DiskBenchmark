using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiskBenchmark.Infrastructure.Converters
{
    internal class BooleanToVisibilityConverter : Converter
    {
        private static bool Visible = false;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(Visible)
                return Visibility.Visible;
            if (value is bool boolValue)
            {
                if (boolValue) { Visible = true; return Visibility.Visible; } else return Visibility.Collapsed;
            }

            return Visibility.Visible; // Default visibility
        }
    }
}
