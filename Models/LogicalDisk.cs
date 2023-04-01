using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    internal class LogicalDisk : ViewModels.Base.ViewModel
    {
        private string caption;
        private string deviceID;
        private string fileSystem;
        private long usedSpace;
        private long size;

        public string Caption { get => caption; set { caption = value;  OnPropertyChanged(); } }
        public string DeviceID { get => deviceID; set { deviceID = value; OnPropertyChanged(); } }
        public string FileSystem { get => fileSystem; set { fileSystem = value; OnPropertyChanged(); } }

        public long UsedSpace { get => usedSpace; set { usedSpace = value; OnPropertyChanged(); } }
        public long Size { get => size; set { size = value; OnPropertyChanged(); } }
    }

}
