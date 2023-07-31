using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Media.Effects;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace Micka___Loop
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private System.Threading.Timer restoreTimer;
        public MainWindow()
        {
            //this.Effect = new BlurEffect();
            WindowBlur.SetIsEnabled(this, true);

            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void bouton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            // Introduce a 3-second delay
            //await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine("Hello, world!");

            // Restore the window after the delay
            restoreTimer = new System.Threading.Timer(RestoreWindow, null, 3000, System.Threading.Timeout.Infinite);

        }

        private void RestoreWindow(object state)
        {
            Console.WriteLine("Hello, world!");

            // This method will be called by the timer after the delay
            // It runs on a separate thread, so we need to use Dispatcher to update UI elements
            Dispatcher.Invoke(() =>
            {
                

                this.WindowState = WindowState.Normal;
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    // Show the window to restore it after the delay
                    this.Show();

                    // Bring the window to the foreground
                    this.Activate();
                }));
                Console.WriteLine(this.WindowState.ToString());
            });

            // Dispose of the timer
            restoreTimer.Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer.Stop();
            // Restore the window when the timer is finished
            if (this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Maximized;
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maximize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void close(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Application.Current.MainWindow.Close();
        }
    }


    enum AccentState
    {
        ACCENT_ENABLE_BLURBEHIND = 3
    }

    struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }
}
