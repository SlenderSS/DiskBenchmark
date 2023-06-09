using System;
using System.Collections.Generic;
using System
    .Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    internal class SystemInfo : ViewModels.Base.ViewModel
    {
        public string SystemName { get; set; } // Ім'я системи
        public string  OSArchitecture { get; set; } // Архітектура системи
        public string OSManufacturer { get; set; } // Виробник системи
        public string Model { get; set; } // Модель комп'ютера
        public string CPUName { get; set; } // Назва процесора
        public string BIOSVersion { get; set; } // Версія БІОС
        public string GPUName { get; set; } // Назва графічного процесора
    }
}
