using DiskBenchmark.Models;
using DiskBenchmark.Services;
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


        //public IEnumerable<Disk> TestDisks1 => Enumerable.Range(1, 10)
        //    .Select(d => new Disk
        //    {
        //        Caption = $"Caption {d}",
        //        DeviceID = $"DeviceID #{d}",
        //        SerialNumber = $"SerialNumber {d}-{d}-{d}-{d}",
        //        Size = 1000 * d,
        //        FreeSpace = d * 1000 / 2,
        //        Partitions = (ObservableCollection<Partition>)Enumerable.Range(1,5).Select(p => new Partition
        //        {
        //            Caption = $"Caption #{d} #{p}",
        //            DeviceID = $" {p}",
        //            Size = 100,
        //            LogicalDisks = (ObservableCollection<LogicalDisk>)Enumerable.Range(0,1).Select(lg => new LogicalDisk
        //            {
        //                Caption = $" {d}, {p}, {lg}",
        //                DeviceID = $" {lg}",
        //                FreeSpace = 50,
        //                Size = 100
        //            })

        //        })
        //    });

        public string TestText = "Test";

        public ConnectedDisksViewModel(): this(null)
        {
            //List<LogicalDisk> logicalDisks = new List<LogicalDisk>();
            //logicalDisks.Add(new LogicalDisk()
            //{
            //    Caption = "C:/",
            //    FreeSpace = 500000,
            //    Size = 1000000
            //});

            //List<Partition> partitions = new List<Partition>();
            //partitions.Add(new Partition()
            //{
            //    Size = 5000000,
            //    Caption = "Part#1",
            //    LogicalDisks = new ObservableCollection<LogicalDisk>(logicalDisks)
            //}); ;

            //TestDisks.Add(new Disk()
            //{
            //    Caption = "Test Disk Caption",
            //    DeviceID = "#0",
            //    FreeSpace = 100000000,
            //    Size = 100000000000,
            //    Partitions = new ObservableCollection<Partition>(partitions)
            //});
        }
        public ConnectedDisksViewModel(MainWIndowViewModel mainWindow)
        {
            this._mainWindow = mainWindow;
            DisksList disksList = new DisksList();
            Task.Factory.StartNew(() => 
            {
                while (true)
                {
                    Disks = new ObservableCollection<Disk>(disksList.GetDisks());
                    Task.Delay(1000);
                }
            });


           

        }
    }
}
