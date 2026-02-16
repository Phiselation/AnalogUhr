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
        public MainWindow()
        {
            InitializeComponent();

            NewUhr = new Ziffernblatt(
                new Point(150, 150), // Mittelpunkt der Uhr
                140,                 // Radius der Uhr
                Colors.Black,        // Kreisfarbe
                3,                   // Kreisdicke
                Colors.Black,        // Strichfarbe
                2                    // Strichdicke
            );
            NewUhr.Draw(UhrCanvas);
        }
    }
}
