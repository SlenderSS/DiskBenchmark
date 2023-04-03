using DiskBenchmark.Infrastructure.Commands;
using DiskBenchmark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiskBenchmark.ViewModels
{
    internal class DiskDetailsViewModel : Base.ViewModel
    {
        private Action<object, object, object> navigation;
        public Disk Disk { get; set; }

        public ICommand BackCommand { get; set; }
        private void Back(object obj)
        {
            navigation.Invoke("DisksList",null,null);
        }

        public DiskDetailsViewModel(Disk selectedDisk, Action<object, object, object> navigation)
        {
            this.navigation = navigation;

            BackCommand = new LambdaCommand(Back);


            this.Disk = selectedDisk;
        }

       
    }
}
