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
        private RectangleGeometry mZeitstrich;
        private EllipseGeometry ZiffernblattDrawing;
        private Point mMittelpunkt;
        private int mRadius;
        private Brush mKreisfarbe;
        private int mKreisdicke;
        private Brush mZeitstrichfarbe;
        private int mZeitstrichdicke;

        private GeometryGroup ggZiffernblatt;
        private GeometryGroup ggZeitstriche;
        private GeometryDrawing gdMittelpunkt;
        private GeometryDrawing gdZiffernblatt;
        private GeometryDrawing gdZeitstrich;

        public DrawingGroup VollstaendigesZiffernblatt { get; private set; }

        public Ziffernblatt(Point Mittelpunkt, int Radius,
                            Brush Kreisfarbe, int Kreisdicke,
                            Brush ZeitstrichFarbe, int Zeitstriche)
            : base(Mittelpunkt, Radius)
        {
            mMittelpunkt = Mittelpunkt;
            mKreisfarbe = Kreisfarbe;
            mKreisdicke = Kreisdicke;
            mRadius = Radius;
            mPen = new Pen(mKreisfarbe, mKreisdicke);
            mZeitstrichfarbe = ZeitstrichFarbe;
            ggZiffernblatt = new GeometryGroup();
            ggZeitstriche = new GeometryGroup();
            VollstaendigesZiffernblatt = new DrawingGroup();
            mZeitstrichdicke = Zeitstriche;
        }
        public void ZeichneKreis()
        {
            // Äußerer Kreis
            ZiffernblattDrawing = new EllipseGeometry
            {
                Center = mMittelpunkt,
                RadiusX = mRadius,
                RadiusY = mRadius,
            };

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
                Center = mMittelpunkt,
                RadiusX = mRadius * 0.05,
                RadiusY = mRadius * 0.05,
            };

            ggZiffernblatt.Children.Add(mittelpunktKreis);
            gdMittelpunkt = new GeometryDrawing
            {
                Geometry = mittelpunktKreis,
                Brush = Brushes.Black
            };

            if (ggZeitstriche.Children.Count > 0)
            {
                gdZeitstrich = new GeometryDrawing
                {
                    Geometry = ggZeitstriche,
                    Brush = mZeitstrichfarbe
                };
            }
            else
            {
                gdZeitstrich = null;
            }

            VollstaendigesZiffernblatt.Children.Clear();
            VollstaendigesZiffernblatt.Children.Add(gdZiffernblatt);
            VollstaendigesZiffernblatt.Children.Add(gdZeitstrich);
            VollstaendigesZiffernblatt.Children.Add(gdMittelpunkt);
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

                mZeitstrich = new RectangleGeometry(new Rect(-dicke / 2.0, -mRadius, dicke, laenge));

                TransformGroup transforms = new TransformGroup();
                transforms.Children.Add(new RotateTransform(angle));
                transforms.Children.Add(new TranslateTransform(mMittelpunkt.X, mMittelpunkt.Y));

                mZeitstrich.Transform = transforms;

                ggZeitstriche.Children.Add(mZeitstrich);
            }
        }
        private void ZeichneZiffern()
        {
        }
    }
}
