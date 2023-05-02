using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Forms = System.Windows.Forms;

namespace DiskBenchmark
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Forms.NotifyIcon _notifyIcon;

        public App()
        {
            _notifyIcon = new Forms.NotifyIcon();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //MainWindow = new MainWindow();
            //MainWindow.Show();


            //_notifyIcon.Icon = new System.Drawing.Icon("app_ico.ico");
            //_notifyIcon.Visible = true;
            //_notifyIcon.Text = "DiskBenchmark";
            //_notifyIcon.Click += Notify_Click;


            base.OnStartup(e);
        }

        private void Notify_Click(object sender, EventArgs e)
        {
            MainWindow.WindowState = WindowState.Normal;
            MainWindow.Activate();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
