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
        public string Caption { get; set; }
        public string DeviceID { get; set; }
        public string SerialNumber { get; set; }
        public ulong Size { get; set; }
        public ulong TotalUsedSpace { get; set; }

        public string PnpDeviceID { get; set; }
        public ObservableCollection<LogicalDisk> LogicalDisks { get; set; }
        public Disk()
        {

            LogicalDisks = new ObservableCollection<LogicalDisk>();
        }
    }
}
