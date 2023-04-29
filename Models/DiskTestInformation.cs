using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    internal class DiskTestInformation : ViewModels.Base.ViewModel
    {
        private string _status;
        public string Status { get => _status; set { Set(ref _status, value); OnPropertyChanged(); } }

        public LineSeries Series { get; set; }

        public void Clear()
        {
            Series.Points.Clear();       
            Status = default;
            
        }

        public DiskTestInformation()
        {


        }
    }
}
