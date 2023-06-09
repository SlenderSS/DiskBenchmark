using System.Collections.ObjectModel;

namespace DiskBenchmark.Models
{
    internal class Disk : ViewModels.Base.ViewModel
    {
        public string Caption { get; set; } // Назва диску
        public string DeviceID { get; set; } // Порядковий номер диску
        public string SerialNumber { get; set; } // Серійний номер жиску
        public ulong Size { get; set; } // Загальний об'єм пам'яті
        public ulong TotalUsedSpace { get; set; } // Об'єм викорисьаної пам'яті

        public string PnpDeviceID { get; set; } // Унікальний ідентифікаційний код 
        public ObservableCollection<LogicalDisk> LogicalDisks { get; set; } // Список логічних дисків
        public Disk()
        {
            LogicalDisks = new ObservableCollection<LogicalDisk>();
        }
    }
}
