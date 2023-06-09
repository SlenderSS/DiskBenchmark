using OxyPlot.Series;

namespace DiskBenchmark.Models
{
    internal class DiskTestInformation : ViewModels.Base.ViewModel
    {
        private string _status;
        public string Status { get => _status; set { Set(ref _status, value); OnPropertyChanged(); } } // Поточний статус тестування

        public LineSeries Series { get; set; } // Властивість відповідальна за відображення результату тестування

        public void Clear() // Метод очищення властивостей для повторного використання
        {
            Series.Points.Clear();       
            Status = default;
            
        }
        public DiskTestInformation()
        {


        }
    }
}
