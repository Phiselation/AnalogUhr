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

        private string Uhrenton;
        private string Font = "Times New Roman";
        private int PlayTime = 1;

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

            double durchmesser = (Math.Min(canvasHeight, canvasWidth) * 0.9);
            Point mitte = new Point(canvasWidth / 2, canvasHeight / 2);

            if (NewUhr == null)
            {
                NewUhr = new Uhr(mitte, (int)durchmesser, Uhrenton, Font, PlayTime);
                NewUhr.CreateImage();
            }
            else
                NewUhr.AktualisiereImage(mitte, (int)durchmesser, Uhrenton, Font);

            Canvas.SetLeft(NewUhr.UhrImage, mitte.X);
            Canvas.SetTop(NewUhr.UhrImage, mitte.Y);

            UhrCanvas.Children.Add(NewUhr.UhrImage);
        }

        private void Sounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sounds.SelectedItem is ComboBoxItem selectedItem) //Macht aus dem ausgewählten Item ein ComboBoxItem, damit man darauf zugreifen kann
            {
                string ausgewaehleterTon = selectedItem.Content.ToString(); //Holt den Text des ausgewählten Items

                switch (ausgewaehleterTon)
                {
                    case "AC/DC":
                        Uhrenton = "ACDC";
                        break;
                    case "Fahrrad":
                        Uhrenton = "Fahrrad";
                        break;
                    case "Kirche":
                        Uhrenton = "Kirche";
                        break;
                    case "Kein Ton":
                        Uhrenton = "kein Ton";
                        break;
                }
            }
            ZeichneUhr();
        }

        private void Fonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Fonts.SelectedItem is ComboBoxItem selectedItem)
            {
                string ausgewaehlteSchriftart = selectedItem.Content.ToString();

                switch (ausgewaehlteSchriftart)
                {
                    case "Arial":
                        Font = "Arial";
                        break;
                    case "Times New Roman":
                        Font = "Times New Roman";
                        break;
                    case "Comic Sans MS":
                        Font = "Comic Sans MS";
                        break;
                    case "Courier New":
                        Font = "Courier New";
                        break;
                }
            }
            ZeichneUhr();
        }

        private void Minute_Checked(object sender, RoutedEventArgs e)
        {
            PlayTime = 1;
        }

        private void Stunde_Checked(object sender, RoutedEventArgs e)
        {
            PlayTime = 2;
        }
    }
}