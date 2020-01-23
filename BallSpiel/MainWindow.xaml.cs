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
using System.Windows.Threading;

namespace BallSpiel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _animationsTimer = new DispatcherTimer();
        private bool gehtNachRechts = true;
        private bool gehtNachUnten = true;

        private int zaehler = 0;

        public MainWindow()
        {
            InitializeComponent();

            _animationsTimer.Interval = TimeSpan.FromMilliseconds(50);
            _animationsTimer.Tick += PositioniereBall;
        }

        private void PositioniereBall(object sender, EventArgs e)
        {
            var x = Canvas.GetLeft(Ball);

            if (gehtNachRechts)
            {
                Canvas.SetLeft(Ball, x + 5);
            }
            else
            {
                Canvas.SetLeft(Ball, x - 5);
            }

            if (x >= Spielplatz.ActualWidth - Ball.ActualWidth)
            {
                gehtNachRechts = false;
            }
            else if (x <= 0)
            {
                gehtNachRechts = true;
            }

            var y = Canvas.GetTop(Ball);

            if (gehtNachUnten)
            {
                Canvas.SetTop(Ball, y + 5);
            }
            else
            {
                Canvas.SetTop(Ball, y - 5);
            }

            if (y >= Spielplatz.ActualHeight - Ball.ActualHeight)
            {
                gehtNachUnten = false;
            }
            else if (y <= 0)
            {
                gehtNachUnten = true;
            }
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (_animationsTimer.IsEnabled)
            {
                _animationsTimer.Stop();
            }
            else
            {
                _animationsTimer.Start();
                zaehler = 0;
                SpielstandLabel.Content = $"{zaehler} Clicks";
            }
        }

        private void Ball_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_animationsTimer.IsEnabled)
            {
                zaehler += 1;
                SpielstandLabel.Content = $"{zaehler} Clicks";
            }
        }

        private void Ball_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F)
            {
                Ball.Fill = Brushes.Red;
            }
        }
    }
}
