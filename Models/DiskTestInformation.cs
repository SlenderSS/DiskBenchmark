using OxyPlot.Series;

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
