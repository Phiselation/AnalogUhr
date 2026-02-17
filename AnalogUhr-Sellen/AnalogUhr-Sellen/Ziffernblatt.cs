using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnalogUhr_Sellen
{
    class Ziffernblatt : UhrBasis
    {
        private Pen mPen;
        private RectangleGeometry mRect;
        private GeometryGroup ZiffernblattDrawing;
        private Point mMittelpunkt;
        private int mRadius;
        private Brush mKreisfarbe;
        private int mKreisdicke;
        private Brush mZeitstrichfarbe;
        private int mZeitstrichdicke;

        public Ziffernblatt(Point Mittelpunkt, int Radius,
                            Brush Kreisfarbe, int Kreisdicke,
                            Brush ZeitstrichFarbe, int Zeitstriche)
            : base(Mittelpunkt, Radius)
        {
            mMittelpunkt = Mittelpunkt;
            mRadius = Radius;
            mKreisfarbe = Kreisfarbe;
            mKreisdicke = Kreisdicke;
            mZeitstrichfarbe = ZeitstrichFarbe;
            mZeitstrichdicke = Zeitstriche;
        }
        public void Zeichne(Canvas canvas)
        {
            // Äußerer Kreis
            Ellipse kreis = new Ellipse
            {
                Width = Radius * 2,
                Height = Radius * 2,
                Stroke = mKreisfarbe,
                StrokeThickness = mKreisdicke,
                Fill = Brushes.White
            };

            Canvas.SetLeft(kreis, Mittelpunkt.X - Radius);
            Canvas.SetTop(kreis, Mittelpunkt.Y - Radius);
            canvas.Children.Add(kreis);

            // Zeitstriche zeichnen
            ZeichneZeitstriche(canvas);

            // Ziffern zeichnen
            ZeichneZiffern(canvas);

            // Mittelpunkt-Punkt
            Ellipse mittelpunktKreis = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black
            };
            Canvas.SetLeft(mittelpunktKreis, Mittelpunkt.X - 5);
            Canvas.SetTop(mittelpunktKreis, Mittelpunkt.Y - 5);
            canvas.Children.Add(mittelpunktKreis);
        }
        private void ZeichneZeitstriche(Canvas canvas)
        {
        }
        private void ZeichneZiffern(Canvas canvas)
        {
        }
    }
}
