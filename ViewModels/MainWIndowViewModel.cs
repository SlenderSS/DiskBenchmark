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

        private MainWIndowViewModel mainWindow;
        

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
        public ICommand DisksTestCommand { get; set; }


        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void DisksList(object obj) => CurrentView = new ConnectedDisksViewModel(OpenUserControl);
        private void DisksTest(object obj) => CurrentView = new DiskSpeedTestViewModel();
        //private void DiskDetails(object obj) => CurrentView = new DiskDetailsViewModel(SelectedDisk);

        private void OpenUserControl(object obj, object param, object navigation)
        {
            
            switch (obj.ToString())
            {
                case "DiskDetails":
                    CurrentView = new DiskDetailsViewModel((Disk)param,(Action<object, object, object>)navigation);
                    break;
                case "DisksList":
                    CurrentView = new ConnectedDisksViewModel(OpenUserControl);
                    break;
            }
        }

      



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
            this.mainWindow = this;
            #region Commands
            //CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            HomeCommand = new LambdaCommand(Home);
            DisksListCommand = new LambdaCommand(DisksList);
            DisksTestCommand = new LambdaCommand(DisksTest);
            #endregion

            
            CurrentView = new HomeViewModel();
        }
    }
}
