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
            for (int i = 0; i < 60; i++)
            {
                double angle = i * 6.0; // 6° pro Minute, 0° = 12 Uhr (Linie zeigt nach oben)

                double laenge;
                double dicke;

                if (i % 15 == 0) // Stunden-Marken (jede Viertelstunde)
                {
                    laenge = Radius * 0.15;
                    dicke = mZeitstrichdicke * 2;
                }
                else if (i % 5 == 0) // 5-Minuten-Marken
                {
                    laenge = Radius * 0.1;
                    dicke = mZeitstrichdicke * 1.5;
                }
                else // Minuten-Marken
                {
                    laenge = Radius * 0.05;
                    dicke = mZeitstrichdicke;
                }

                // Linie am Ursprung: von y = -Radius (außerer Kreis) nach innen um "laenge"
                Line strich = new Line
                {
                    X1 = 0,
                    Y1 = -Radius,
                    X2 = 0,
                    Y2 = -(Radius - laenge),
                    Stroke = mZeitstrichfarbe,
                    StrokeThickness = dicke,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };

                // Zuerst rotieren (um Ursprung), dann zum Mittelpunkt verschieben
                var transforms = new TransformGroup();
                transforms.Children.Add(new RotateTransform(angle)); // dreht um (0,0)
                transforms.Children.Add(new TranslateTransform(Mittelpunkt.X, Mittelpunkt.Y)); // verschiebt Ursprung zur Mitte
                strich.RenderTransform = transforms;

                canvas.Children.Add(strich);
            }
        }
        private void ZeichneZiffern(Canvas canvas)
        {
        }
    }
}
