using DiskBenchmark.Models;
using DiskBenchmark.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
