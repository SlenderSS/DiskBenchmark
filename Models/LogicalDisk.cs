namespace DiskBenchmark.Models
{
    internal class LogicalDisk : ViewModels.Base.ViewModel
    {
        public string Caption { get; set; } // Назва логічного диску
        public string DeviceID { get; set;} // Позиційний номер логічного диску
        public string FileSystem { get; set; } // Файлова система 
        public ulong UsedSpace { get; set; } // Використаний об'єм пам'яті логічого диску
        public ulong Size { get; set; } // Загальний об'єм пам'яті логічого диску
    }

}
