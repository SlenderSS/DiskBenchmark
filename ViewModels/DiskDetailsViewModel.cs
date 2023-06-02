using DiskBenchmark.Infrastructure.Commands;
using DiskBenchmark.Models;
using DiskBenchmark.Services;
using System;
using System.Windows.Input;

namespace DiskBenchmark.ViewModels
{
    internal class DiskDetailsViewModel : Base.ViewModel
    {
        private Action<object, object, object> navigation;


        public Disk Disk { get; set; }


        private SmartDisk _smartDisk;
        public SmartDisk SmartDisk { get => _smartDisk; set { _smartDisk = value; OnPropertyChanged(); } }



        public ICommand BackCommand { get; set; }
        private void Back(object obj) => navigation.Invoke("DisksList", null, null);




        public DiskDetailsViewModel(Disk selectedDisk, Action<object, object, object> navigation)
        {
            this.navigation = navigation;

            BackCommand = new LambdaCommand(Back);

            this.Disk = selectedDisk;

            SmartDisk = new DisksService().GetSmartInformation(Disk);

        }

       
    }
}
