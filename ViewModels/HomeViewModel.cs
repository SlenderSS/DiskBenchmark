using DiskBenchmark.Models;

namespace DiskBenchmark.ViewModels
{
    internal class HomeViewModel : Base.ViewModel
    {
        private SystemInfo _systemInfo;

        public SystemInfo SystemInfo { get => _systemInfo; set { Set(ref _systemInfo, value); OnPropertyChanged(); } }

        

        public HomeViewModel() : this(null)
        {

        }
        public HomeViewModel(SystemInfo systemInfo)
        {
            SystemInfo = systemInfo;
            
        }
    }
}
