using DiskBenchmark.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DiskBenchmark.ViewModels
{
    internal class MainWIndowViewModel
    {

        #region Commands

        #region CloseApp
        private ICommand CloseAppCommand { get; }


        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseAppCommandExecute(object p) => true;


        #endregion

        #endregion
        public MainWIndowViewModel()
        {
            #region Commands
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            #endregion





        }
    }
}
