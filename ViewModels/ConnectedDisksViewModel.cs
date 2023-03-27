using DiskBenchmark.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.ViewModels
{
    internal class ConnectedDisksViewModel : Base.ViewModel
    {
        private MainWIndowViewModel _mainWindow;
        public List<Disk> TestDisks  = new List<Disk>();
        public ObservableCollection<Disk> disks { get; private set; }

        public ConnectedDisksViewModel(MainWIndowViewModel mainWindow)
        {
            this._mainWindow = mainWindow;
            disks = mainWindow.disks;


            List<LogicalDisk> logicalDisks = new List<LogicalDisk>();
            logicalDisks.Add(new LogicalDisk()
            {
                Caption = "C:/",
                FreeSpace = 500000,
                Size =     1000000
            });

            List<Partition> partitions = new List<Partition>();
            partitions.Add(new Partition()
            {
                Size = 5000000,
                Caption = "Part#1",
                LogicalDisks = new ObservableCollection<LogicalDisk>(logicalDisks)
            }); ;

            TestDisks.Add(new Disk()
            {
                Caption = "Test Disk Caption",
                DeviceID = "#0",
                FreeSpace = 100000000,
                Size = 100000000000,
                Partitions = new ObservableCollection<Partition>(partitions)
            });

        }
    }
}
