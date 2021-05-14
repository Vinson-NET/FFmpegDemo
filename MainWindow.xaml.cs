using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ffmpegDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ffmpegManager manager;
        private DispatcherTimer dispatcherTimer = null;
        private int count = 0;
        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }



        private void StartRecord_Click(object sender, RoutedEventArgs e)
        {
            if (manager == null)
            {
                manager = new ffmpegManager();
            }
            manager.RecordScreen();
            dispatcherTimer.Start();
        }

        private void StopRecord_Click(object sender, RoutedEventArgs e)
        {
            if (manager != null)
            {
                manager.StopRecord();
                dispatcherTimer.Stop();
                count = 0;
            }
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            count = count + 1;
            this.lbl.Content = count;
        }
    }
}
