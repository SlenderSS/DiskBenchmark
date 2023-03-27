using System.Collections.Generic;

namespace DiskBenchmark.Models
{
    internal class Partition
    {
        public string Caption { get; set; }
        public string DeviceID { get; set; }
        public long Size { get; set; }
        public List<LogicalDisk> LogicalDisks { get; set; }

        public Partition()
        {
            LogicalDisks = new List<LogicalDisk>();
        }
    }
}
