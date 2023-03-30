using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    internal class Disk
    {
        public string Caption { get; set; }
        public string DeviceID { get; set; }
        public string SerialNumber { get; set; }
        public long Size { get; set; }
        public long TotalUsedSpace { get; set; }
        public List<LogicalDisk> LogicalDisks { get; set; }
        public Disk()
        {

            LogicalDisks = new List<LogicalDisk>();
        }
    }
}
