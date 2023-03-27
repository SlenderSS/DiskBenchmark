using System;
using System.Collections.Generic;
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
        public long FreeSpace { get; set; }
        public List<Partition> Partitions { get; set; }

        public Disk()
        {
            Partitions = new List<Partition>();
        }
    }
}
