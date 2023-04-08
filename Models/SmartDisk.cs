using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    class SmartDisk
    {
        public int Index { get; set; }
        public string PnpDeviceID { get; set; }
        public bool IsOK { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Serial { get; set; }
        public SmartAttributeCollection SmartAttributes { get; set; }
        public bool IsSupported { get; set; }

        public SmartDisk()
        {
            SmartAttributes = new SmartAttributeCollection();
        }
    }
}
