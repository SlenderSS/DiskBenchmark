using System;
using System.Collections.Generic;
using System
    .Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    class SystemInfo : ViewModels.Base.ViewModel
    {
        public string SystemName { get; set; }
        public string  OSArchitecture { get; set; }
        public string OSManufacturer { get; set; }
        public string Model { get; set; }
        public string CPUName { get; set; }
        public string BIOSVersion { get; set; }
        public string GPUName { get; set; }
    }
}
