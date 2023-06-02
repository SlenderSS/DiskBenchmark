namespace DiskBenchmark.Models
{
    internal class LogicalDisk : ViewModels.Base.ViewModel
    {
      
        public string Caption { get; set; }
        public string DeviceID { get; set;}
        public string FileSystem { get; set; }

        public ulong UsedSpace { get; set; }
        public ulong Size { get; set; }
    }

}
