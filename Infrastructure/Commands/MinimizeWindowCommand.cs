using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiskBenchmark.Infrastructure.Commands
{
    class MinimizeWindowCommand :Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
