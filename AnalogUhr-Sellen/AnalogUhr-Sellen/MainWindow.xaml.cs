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
        private Ziffernblatt NewUhr;
        //public MainWindow()
        //{
        //    InitializeComponent();
            
        //    NewUhr = new Ziffernblatt(
        //        new Point(150, 150), // Mittelpunkt der Uhr
        //        150,                 // Radius der Uhr
        //        Brushes.Black,        // Kreisfarbe
        //        3,                   // Kreisdicke
        //        Brushes.Black,        // Strichfarbe
        //        2                    // Strichdicke
        //    );
        //    NewUhr.Zeichne(UhrCanvas);
        //}
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

            double canvasWidth = UhrCanvas.ActualWidth;
            double canvasHeight = UhrCanvas.ActualHeight;

            if (canvasWidth <= 0 || canvasHeight <= 0)
                return;

            double radius = Math.Floor(Math.Min(canvasWidth, canvasHeight) / 2.0);
            Point mitte = new Point(canvasWidth / 2.0, canvasHeight / 2.0);

            Ziffernblatt neueUhr = new Ziffernblatt(
                mitte,
                Convert.ToInt32(radius),
                Brushes.Black,
                3,
                Brushes.Black,
                2
            );

            neueUhr.Zeichne(UhrCanvas);
        }
    }
}