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
        public MainWindow()
        {
            InitializeComponent();
            
            NewUhr = new Ziffernblatt(
                new Point((UhrCanvas.ActualWidth/2), (UhrCanvas.ActualHeight/2)), // Mittelpunkt der Uhr
                Convert.ToInt32(UhrCanvas.ActualWidth/2),                 // Radius der Uhr
                Brushes.Black,        // Kreisfarbe
                3,                   // Kreisdicke
                Brushes.Black,        // Strichfarbe
                2                    // Strichdicke
            );
            NewUhr.Zeichne(UhrCanvas);
        }
    }
}