namespace DiskBenchmark.Models
{
    internal class SmartAttribute : ViewModels.Base.ViewModel
    {
        public int Register { get; set; }
        public string Name { get; set; } // Ім'я атрибуту

        public int Current { get; set; } // Поточне значення
        public int Worst { get; set; } // Найгірше значення
        public int Threshold { get; set; } // Порогове значення
        public int Data { get; set; } // Значення атрибуту
        public bool IsOK { get; set; }

        public bool HasData
        {
            get
            {
                if (Current == 0 && Worst == 0 && Threshold == 0 && Data == 0)
                    return false;
                return true;
            }
        }

        public SmartAttribute(int register, string attributeName)
        {
            this.Register = register;
            this.Name = attributeName;
        }
    }
}
