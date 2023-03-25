using DiskBenchmark.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DiskBenchmark.ViewModels.Base;

namespace DiskBenchmark.ViewModels
{
    internal class MainWIndowViewModel: ViewModel
    {
        #region Title
        private string _Title = "Disk Benchmark";
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Commands

        #region CloseApp
        //public ICommand CloseAppCommand { get; }


        //private void OnCloseAppCommandExecuted(object p)
        //{
        //    Application.Current.Shutdown();
        //}
        //private bool CanCloseAppCommandExecute(object p) => true;


        #endregion

        #endregion
        public MainWIndowViewModel()
        {
            #region Commands
           // CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            #endregion





        }
    }
}
