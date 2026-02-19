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

namespace AnalogUhr_Sellen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Uhr NewUhr;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ZeichneUhr();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Optional: neu zeichnen, wenn Fenstergröße sich ändert
            ZeichneUhr();
        }

        private void ZeichneUhr()
        {
            UhrCanvas.Children.Clear();

            UhrCanvas.Background = Brushes.LightGray;

            double canvasWidth = UhrCanvas.ActualWidth;
            double canvasHeight = UhrCanvas.ActualHeight;

            double durchmesser = Math.Min(canvasHeight, canvasWidth);
            Point mitte = new Point(canvasWidth / 2.0, canvasHeight / 2.0);

            NewUhr = new Uhr(mitte, (int)durchmesser);

            Image AnalogUhr = NewUhr.CreateImage();

            Canvas.SetLeft(AnalogUhr, mitte.X);
            Canvas.SetTop(AnalogUhr, mitte.Y);

            UhrCanvas.Children.Add(AnalogUhr);
        }
    }
}