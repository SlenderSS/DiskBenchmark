using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    internal class LogicalDisk
    {
        public string Caption { get; set; }
        public string DeviceID { get; set; }
        public string FileSystem { get; set; }
        public long FreeSpace { get; set; }
        public long Size { get; set; }
    }

}
