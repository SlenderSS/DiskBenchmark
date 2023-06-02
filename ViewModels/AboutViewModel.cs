using DiskBenchmark.Models;

namespace DiskBenchmark.ViewModels
{
    class AboutViewModel : Base.ViewModel
    {

        private int _viewCount;

        public int ViewCount
        {
            get { return _viewCount; }
            set { _viewCount = value; OnPropertyChanged(); }
        }

        public AboutViewModel(View view)
        {
            ViewCount = (int)view;
        }
    }
}
