using DiskBenchmark.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DiskBenchmark.ViewModels.Base;
using System.Collections.ObjectModel;
using DiskBenchmark.Models;
using System.Management;

namespace DiskBenchmark.ViewModels
{
    internal class MainWIndowViewModel: ViewModel
    {
        #region Title
        private string _Title = "Disk Benchmark";
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

       

        private object _currentView;
        public object CurrentView { get => _currentView; set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand DisksListCommand { get; set; }
        public ICommand DiskDetailsCommand { get; set; }


        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void DisksList(object obj) => CurrentView = new ConnectedDisksViewModel();
        private void DiskDetails(object obj) => CurrentView = new DiskDetailsViewModel();



        //public IEnumerable<Disk> TestDisks1 => Enumerable.Range(1, 9)
        //   .Select(d => new Disk
        //   {
        //       Caption = $"Caption {d}",
        //       DeviceID = $"DeviceID #{d}",
        //       SerialNumber = $"SerialNumber {d}-{d}-{d}-{d}",
        //       Size = 1000 * d,
        //       FreeSpace = d * 1000 / 2,
        //       Partitions = new ObservableCollection<Partition>(Enumerable.Range(1, 5).Select(p => new Partition
        //       {
        //           Caption = $"Caption #{d} #{p}",
        //           DeviceID = $" {p}",
        //           Size = 100,
        //           LogicalDisks = new ObservableCollection<LogicalDisk>(Enumerable.Range(0, 1).Select(lg => new LogicalDisk
        //           {
        //               Caption = $" {d}, {p}, {lg}",
        //               DeviceID = $" {lg}",
        //               FreeSpace = 50,
        //               Size = 100
        //           }))

        //       }))
        //   });


        //public ObservableCollection<Disk> disks { get; set; }




        #region Commands

        #region CloseApp
        //public ICommand CloseAppCommand { get; }


        //private void OnCloseAppCommandExecuted(object p)
        //{
        //    Application.Current.Shutdown();
        //}
        //private bool CanCloseAppCommandExecute(object p) => true;


        #endregion

        #region GetDisks

        public ICommand GetDisks { get; }
        private void OnGetDisksCommandExecuted(object p)
        {

        }
        private bool CanGetDisksCommandExecute(object p) => true;
        #endregion


        #endregion
        public MainWIndowViewModel()
        {
            #region Commands
            //CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            HomeCommand = new LambdaCommand(Home);
            DisksListCommand = new LambdaCommand(DisksList);
            DiskDetailsCommand = new LambdaCommand(DiskDetails);
            #endregion

            
            CurrentView = new HomeViewModel();
        }
    }
}
