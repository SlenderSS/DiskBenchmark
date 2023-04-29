using DiskBenchmark.Infrastructure.Commands;
using DiskBenchmark.Models;
using DiskBenchmark.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiskBenchmark.ViewModels
{
    internal class ConnectedDisksViewModel : Base.ViewModel
    {
        private readonly Action<object, object, object> navigate;
        

        private ObservableCollection<Disk> _disks;
        public ObservableCollection<Disk> Disks 
        {
            get => _disks; 
            set 
            {
                _disks = value;
                OnPropertyChanged();
            }
        }

        public Disk SelectedDisk { get; set; }

       
        public ICommand DiskDetailsCommand { get; set; }

        private void DiskDetails(object obj) =>navigate.Invoke("DiskDetails", SelectedDisk, navigate);

        
        public ConnectedDisksViewModel(Action<object, object, object> navigate)
        {
            #region Commands
            DiskDetailsCommand = new LambdaCommand(DiskDetails);
            #endregion

            //Disks = new ObservableCollection<Disk>();
            this.navigate = navigate;
            DisksService disksList = new DisksService();

            try
            {
                Task.Factory.StartNew( async() =>
                {
                    while (true)
                    { 
                        Disks = new ObservableCollection<Disk>(disksList.GetDisks());
                        
                        await Task.Delay(5000);
                    }
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            


        }
    }
}
