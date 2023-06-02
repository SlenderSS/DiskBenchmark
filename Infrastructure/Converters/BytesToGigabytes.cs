using System;
using System.Globalization;

namespace DiskBenchmark.Infrastructure.Converters
{
    internal class BytesToGigabytes : Converter
    {
        
        private uint _gigabyte = 1024 * 1024 * 1024; //1 073 741 824
        private uint _megabyte = 1024 * 1024; 
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ulong bytes)) return null;

            
            if(bytes > _gigabyte)
                return bytes / _gigabyte + " GB";
            else 
                return bytes / _megabyte + " MB";
            //if(bytes / _gigabyte < 0) 
        }
        
    }
}
