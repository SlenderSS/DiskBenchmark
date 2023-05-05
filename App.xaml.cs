using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

            
            
            //_notifyIcon.Icon = ConvertDrawingToIcon(((DrawingImage)Application.Current.FindResource("MyIcon")).Drawing);
            //_notifyIcon.Visible = true;
            //_notifyIcon.Text = "DiskBenchmark";
            //_notifyIcon.Click += Notify_Click;


            base.OnStartup(e);
        }


        private Icon ConvertDrawingToIcon(Drawing drawing)
        {
            // Convert the drawing to a bitmap
            Rect bounds = drawing.Bounds;
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawDrawing(drawing);
            }
            var temp = (int)bounds.Width;
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            MemoryStream stream = new MemoryStream();
            encoder.Save(stream);
            Bitmap bmp = new Bitmap(stream);

            // Convert the bitmap to an icon
            Icon icon = Icon.FromHandle(bmp.GetHicon());

            return icon;
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
