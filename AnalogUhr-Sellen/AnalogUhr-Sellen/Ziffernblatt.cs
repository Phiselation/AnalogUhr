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
        private Rectangle mZeitstrich;
        private EllipseGeometry ZiffernblattDrawing;
        private Point mMittelpunkt;
        private int mRadius;
        private Brush mKreisfarbe;
        private int mKreisdicke;
        private Brush mZeitstrichfarbe;
        private int mZeitstrichdicke;

        protected GeometryGroup ggZiffernblatt;
        protected GeometryDrawing gdZiffernblatt;

        public GeometryDrawing VollstaendigesZiffernblatt { get; private set; }

        public Ziffernblatt(Point Mittelpunkt, int Radius,
                            Brush Kreisfarbe, int Kreisdicke,
                            Brush ZeitstrichFarbe, int Zeitstriche)
            : base(Mittelpunkt, Radius)
        {
            mKreisfarbe = Kreisfarbe;
            mKreisdicke = Kreisdicke;
            mPen = new Pen(mKreisfarbe, mKreisdicke);
            mZeitstrichfarbe = ZeitstrichFarbe;
        }
        public void ZeichneKreis()
        {
            // Äußerer Kreis
            ZiffernblattDrawing = new EllipseGeometry
            {
                Center = mMittelpunkt,
                RadiusX = mRadius * 2,
                RadiusY = mRadius * 2,
            };

            //Canvas.SetLeft(ZiffernblattDrawing, mMittelpunkt.X - mRadius);
            //Canvas.SetTop(ZiffernblattDrawing, mMittelpunkt.Y - mRadius);
            ggZiffernblatt.Children.Add(ZiffernblattDrawing);

            gdZiffernblatt = new GeometryDrawing
            {
                Geometry = ZiffernblattDrawing,
                Pen = mPen,
                Brush = Brushes.White
            };

            // Zeitstriche zeichnen
            ZeichneZeitstriche();

            // Ziffern zeichnen
            ZeichneZiffern();

            // Mittelpunkt-Punkt
            EllipseGeometry mittelpunktKreis = new EllipseGeometry
            {
                RadiusX = 10,
                RadiusY = 10,
            };
            //Canvas.SetLeft(mittelpunktKreis, mMittelpunkt.X - 5);
            //Canvas.SetTop(mittelpunktKreis, mMittelpunkt.Y - 5);
            ggZiffernblatt.Children.Add(mittelpunktKreis);
            gdZiffernblatt = new GeometryDrawing
            {
                Geometry = mittelpunktKreis,
                Brush = Brushes.Black
            };

            VollstaendigesZiffernblatt = gdZiffernblatt;
        }
        private void ZeichneZeitstriche()
        {
            for (int i = 0; i < 60; i++)
            {
                double angle = i * 6.0; // 6° pro Minute, 0° = 12 Uhr (Linie zeigt nach oben)

                double laenge;
                double dicke;

                if (i % 15 == 0) // Stunden-Marken (jede Viertelstunde)
                {
                    laenge = mRadius * 0.15;
                    dicke = mZeitstrichdicke * 2;
                }
                else if (i % 5 == 0) // 5-Minuten-Marken
                {
                    laenge = mRadius * 0.1;
                    dicke = mZeitstrichdicke * 1.5;
                }
                else // Minuten-Marken
                {
                    laenge = mRadius * 0.05;
                    dicke = mZeitstrichdicke;
                }

                mZeitstrich = new Rectangle
                {
                    Width = dicke,
                    Height = laenge,
                    Fill = mZeitstrichfarbe,
                    StrokeThickness = 0
                };

                TransformGroup transforms = new TransformGroup();
                transforms.Children.Add(new TranslateTransform(-dicke / 2.0, -mRadius)); // obere Mitte -> (0,0)
                transforms.Children.Add(new RotateTransform(angle));                     // Drehung um (0,0)
                transforms.Children.Add(new TranslateTransform(mMittelpunkt.X, mMittelpunkt.Y)); // verschieben zur Mitte

                mZeitstrich.RenderTransform = transforms;

                // Canvas-Position kann 0,0 bleiben, Transform macht die Platzierung
                Canvas.SetLeft(mZeitstrich, 0);
                Canvas.SetTop(mZeitstrich, 0);

                ggZiffernblatt.Children.Add(mZeitstrich);
            }
        }
        public void ZeichneZiggernblatt()        
        {
            ZeichneKreis();
            ZeichenZeitstriche();
        }
        private void ZeichneZiffern()
        {
        }
    }
}
