using System.Windows;

namespace DiskBenchmark.Infrastructure.Commands
{
    internal class CloseAppCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
