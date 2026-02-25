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
            ZeichneUhr();
        }

        private void ZeichneUhr()
        {
            UhrCanvas.Children.Clear();

            UhrCanvas.Background = Brushes.LightGray;

            double canvasWidth = UhrCanvas.ActualWidth;
            double canvasHeight = UhrCanvas.ActualHeight;

            double durchmesser = (Math.Min(canvasHeight, canvasWidth)*0.9);
            Point mitte = new Point(canvasWidth / 2, canvasHeight / 2);

            if (NewUhr == null)
            {
                NewUhr = new Uhr(mitte, (int)durchmesser);
                NewUhr.CreateImage();
            }
            else
                NewUhr.AktualisiereImage(mitte, (int)durchmesser);

            Canvas.SetLeft(NewUhr.UhrImage, mitte.X);
            Canvas.SetTop(NewUhr.UhrImage, mitte.Y);

            UhrCanvas.Children.Add(NewUhr.UhrImage);
        }
    }
}