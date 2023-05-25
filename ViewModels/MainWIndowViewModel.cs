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
using DiskBenchmark.Services;

namespace DiskBenchmark.ViewModels
{
    internal class MainWIndowViewModel: ViewModel
    {
        #region Title
        private string _Title = "Disk Benchmark";
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Current view
        private object _currentView;
        public object CurrentView { get => _currentView; set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Current view
        private object _about;
        public object AboutView
        {
            get => _about; set
            {
                _about = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public SystemInfoService SystemInfoService { get; }
        private SystemInfo _systemInfo;
        public SystemInfo SystemInfo { get => _systemInfo; set { Set(ref _systemInfo, value); OnPropertyChanged(); } }

        #region Commands
        public ICommand HomeCommand { get; set; }
        public ICommand DisksListCommand { get; set; }
        public ICommand DiskDetailsCommand { get; set; }
        public ICommand DisksTestCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        



       

        private void Home(object obj)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            CurrentView = new HomeViewModel(SystemInfo);
            AboutView = new AboutViewModel(View.Home);
        }
        private void DisksList(object obj)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            CurrentView = new ConnectedDisksViewModel(OpenUserControl);
            AboutView = new AboutViewModel(View.ViewDisks);
        }
        private void DisksTest(object obj)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            CurrentView = new DiskSpeedTestViewModel();
            AboutView = new AboutViewModel(View.Benchmark);
        }

        private void OpenUserControl(object obj, object param, object navigation)
        {
            
            switch (obj.ToString())
            {
                case "DiskDetails":
                    CurrentView = new DiskDetailsViewModel((Disk)param,(Action<object, object, object>)navigation);
                    AboutView = new AboutViewModel(View.Smart);
                    break;
                case "DisksList":
                    CurrentView = new ConnectedDisksViewModel(OpenUserControl);
                    break;
            }
        }

        #endregion




        

        public MainWIndowViewModel()
        {          
            #region Commands
           
            HomeCommand = new LambdaCommand(Home);
            DisksListCommand = new LambdaCommand(DisksList);
            DisksTestCommand = new LambdaCommand(DisksTest);
            #endregion

            SystemInfoService = new SystemInfoService();
            Task.Run(() => { SystemInfo = SystemInfoService.GetOperatingSystemInfo(); }).Wait();

            CurrentView = new HomeViewModel(SystemInfo);
            AboutView = new AboutViewModel(View.Home);
        }
    }
}
