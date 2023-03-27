using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DiskBenchmark.Models
{
    internal class Partition
    {
        public string Caption { get; set; }
        public string DeviceID { get; set; }
        public long Size { get; set; }
        public ObservableCollection<LogicalDisk> LogicalDisks { get; set; }

        public Partition()
        {
            LogicalDisks = new ObservableCollection<LogicalDisk>();
        }
    }
}
