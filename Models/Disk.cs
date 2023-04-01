using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    internal class Disk : ViewModels.Base.ViewModel
    {
        private string caption;
        private string deviceID;
        private string serialNumber;
        private long size;
        private long totalUsedSpace;
        private ObservableCollection<LogicalDisk> logicalDisks;

        public string Caption { get => caption; set { caption = value; OnPropertyChanged(); } }
        public string DeviceID { get => deviceID; set { deviceID = value; OnPropertyChanged(); } }
        public string SerialNumber { get => serialNumber; set { serialNumber = value; OnPropertyChanged(); } }
        public long Size { get => size; set { size = value; OnPropertyChanged(); } }
        public long TotalUsedSpace { get => totalUsedSpace; set { totalUsedSpace = value; OnPropertyChanged(); } }
        public ObservableCollection<LogicalDisk> LogicalDisks { get => logicalDisks; set { logicalDisks = value; OnPropertyChanged(); } }
        public Disk()
        {

            LogicalDisks = new ObservableCollection<LogicalDisk>();
        }
    }
}
