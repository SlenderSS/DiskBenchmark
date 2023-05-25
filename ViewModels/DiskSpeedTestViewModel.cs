using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DiskBenchmark.Infrastructure.Commands;
using DiskBenchmark.Models;
using DiskBenchmark.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using StorageSpeedMeter;
using StorageSpeedMeter.Helpers;
using StorageSpeedMeter.Tests;

namespace DiskBenchmark.ViewModels
{
    internal class DiskSpeedTestViewModel : Base.ViewModel
    {
        #region Drives
        private DriveInfo[] _drives = new DriveInfo[0];
        public DriveInfo[] Drives {
            get => _drives;
            set
            {
                Set(ref _drives, value);
                OnPropertyChanged();
            }
        }

        #endregion

        #region Selected drive
        private DriveInfo _SelectedDrive;
        public DriveInfo SelectedDrive
        {
            get => _SelectedDrive;
            set
            {
                Set(ref _SelectedDrive, value);
                OnPropertyChanged();
            }
        }
        #endregion


        #region Sequential Write
        private DiskTestInformation _sequentialWrite;
        public DiskTestInformation SequentialWrite { get => _sequentialWrite; set { Set(ref _sequentialWrite, value); OnPropertyChanged(); } }
        #endregion
        #region SequentialRead
        private DiskTestInformation _sequentialRead;
        public DiskTestInformation SequentialRead { get => _sequentialRead; set { Set(ref _sequentialRead, value); OnPropertyChanged(); } }
        #endregion
        #region RandomWrite
        private DiskTestInformation _randomWrite;
        public DiskTestInformation RandomWrite { get => _randomWrite; set { Set(ref _randomWrite, value); OnPropertyChanged(); } }
        #endregion
        #region RandomRead
        private DiskTestInformation _randomRead;
        public DiskTestInformation RandomRead { get => _randomRead; set { Set(ref _randomRead, value); OnPropertyChanged(); } }
        #endregion
        #region Memory Copy
        private DiskTestInformation _memoryCopy;
        public DiskTestInformation MemoryCopy { get => _memoryCopy; set { Set(ref _memoryCopy, value); OnPropertyChanged(); } }
        #endregion

        public PlotModel MyModel { get; private set; }
        public ICommand DiskTeskCommand => new LambdaCommand(
                  async (a) => {
                      IsRun = true;
                      var testSuite = new BigTest(SelectedDrive.RootDirectory.FullName, DisksService.FILE_SIZE, false);
                      int testCounter = -1;
                      await Task.Run(() =>
                      {
                          SequentialWrite.Clear();
                          SequentialRead.Clear();
                          RandomWrite.Clear();
                          RandomRead.Clear();
                          MemoryCopy.Clear();
                          using (testSuite)
                          {
                              string currentTest = null;
                             
                              var breakTest = false;
                              DiskTestInformation current = new DiskTestInformation();

                              testSuite.StatusUpdate += (sender, e) =>
                              {
                                  if (breakTest) return;
                                  if (e.Status == TestStatus.NotStarted) return;

                                  if ((sender as Test).DisplayName != currentTest)
                                  {
                                      ++testCounter;
                                      if (testCounter > 4)
                                          return;
                                      currentTest = (sender as Test).DisplayName;
                                      switch (testCounter)
                                      {
                                          case 0:
                                              current = SequentialWrite;
                                              break;
                                          case 1:
                                              current = SequentialRead;
                                              break;
                                          case 2:
                                              current = RandomWrite;
                                              break;
                                          case 3:
                                              current = RandomRead;
                                              break;
                                          case 4:
                                              current = MemoryCopy;
                                              break;
                                      }                                  
                                   }

                                  if (e.Status != TestStatus.Completed)
                                  {

                                      switch (e.Status)
                                      {
                                          case TestStatus.Started:
                                              current.Status = "Started";
                                              break;
                                          case TestStatus.InitMemBuffer:
                                              current.Status = "Initializing test data in RAM...";
                                              break;
                                          case TestStatus.PurgingMemCache:
                                              current.Status = "Purging file cache in RAM...";
                                              break;
                                          case TestStatus.WarmigUp:
                                              current.Status = "Warming up...";
                                              break;
                                          case TestStatus.Interrupted:
                                              current.Status = "Test interrupted";
                                              break;
                                          case TestStatus.Running:                                     
                                               current.Status = $"Running...";
                                              var temp = new DataPoint(e.ProgressPercent ?? 0, e.RecentResult ?? 0);
                                              current.Series.Points.Add(temp);
                                              MyModel.InvalidatePlot(true);

                                              break;
                                      }
                                      Console.ResetColor();
                                  }
                                  else if ((e.Status == TestStatus.Completed) && (e.Results != null))
                                  {

                                      current.Status = string.Format("Avg: {0:0.00} MB/s  Min÷Max: {1:0.00} MB/s ÷ {2:0.00} MB/s, Time: {3}m{4:00}s",
                                          e.Results.AvgThroughput,
                                          e.Results.Min,
                                          e.Results.Max,
                                          e.ElapsedMs / 1000 / 60,
                                          e.ElapsedMs / 1000 % 60);

                                  }
                              };
                              testSuite.Execute();

                          }

                          IsRun = false;
                      });
                  },
                  (e) =>
                  {
                      if (SelectedDrive != null && !IsRun) 
                      { 

                          return true; 
                      }
                      else                       
                          return false;
                      
                  }
                  );
        private bool IsRun = false;

        public DiskSpeedTestViewModel()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        var updateDrives = DriveInfo.GetDrives().Where(x => x.IsReady && x.AvailableFreeSpace > DisksService.FILE_SIZE).ToArray();
                        if (updateDrives.Length != Drives.Length || SelectedDrive == null)
                            Drives = updateDrives;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    await Task.Delay(5000);
                }
            });

            var color = OxyColor.FromRgb(138, 197, 206);
            MyModel = new PlotModel { Title = "Write/Read speed benchmark test", TextColor = color /*PlotAreaBorderColor = OxyColor.FromRgb(1, 108, 192)*/ };
            MyModel.PlotAreaBorderColor = color;

            var speedAxis = new LinearAxis()
            {
                Title = "Speed (MB/s)",
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                TicklineColor = color,
                MinimumMinorStep = 10,
                AbsoluteMinimum = 0,
                TextColor = color,
                MajorGridlineStyle = LineStyle.Dot,
                MinorGridlineStyle = LineStyle.Dot

            };
            var progressAxis = new LinearAxis()
            {                
                IsAxisVisible = true,
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                TicklineColor = color,
                Minimum = 0,
                MinimumMajorStep = 1,
                Maximum = 100,
                TextColor = color,
                MajorGridlineStyle = LineStyle.Dot,
                MinorGridlineStyle = LineStyle.Dot

            };

            MyModel.Axes.Add(speedAxis);
            MyModel.Axes.Add(progressAxis);


            SequentialWrite = new DiskTestInformation
            {
                Series = new LineSeries
                { 
                    Title = "Sequential write",
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2.0,
                    Color = OxyColor.FromRgb(152, 113, 191)
                }
            };
            SequentialRead = new DiskTestInformation
            {
                Series = new LineSeries
                {
                    Title = "Sequential read",
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2.0,
                    Color = OxyColor.FromRgb(191, 113, 149)
                }
            };
            RandomWrite = new DiskTestInformation
            {
                Series = new LineSeries
                {
                    Title = "Random write",
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2.0,
                    Color = OxyColor.FromRgb(88, 138, 217)
                }
            };
            RandomRead = new DiskTestInformation
            {
                Series = new LineSeries()
                {
                    Title = "Random read",
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2.0,
                    Color = OxyColor.FromRgb(101, 211, 187)
                }
            };
            MemoryCopy = new DiskTestInformation
            {
                Series = new LineSeries()
                {
                    Title = "Memory copy",
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2.0,
                    Color = OxyColor.FromRgb(164, 185, 69)
                }
            };

            MyModel.Series.Add(SequentialWrite.Series);
            MyModel.Series.Add(SequentialRead.Series);
            MyModel.Series.Add(RandomWrite.Series);
            MyModel.Series.Add(RandomRead.Series);
            
            //this.MyModel.Series.Add(MemoryCopy.Series);


            


        }
    }
}
