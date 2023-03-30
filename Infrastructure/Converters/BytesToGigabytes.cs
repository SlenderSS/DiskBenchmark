using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Infrastructure.Converters
{
    internal class BytesToGigabytes : Converter
    {
        private long _gigabyte = 1024 * 1024 * 1024;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double bytes)) return null;

            return bytes / _gigabyte;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double gigabytes)) return null;

            return gigabytes * _gigabyte;
        }
    }
}
