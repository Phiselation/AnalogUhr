using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public GeometryDrawing gdMittelpunkt { get; private set; }
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
            gdMittelpunkt = new GeometryDrawing();
            VollstaendigesZiffernblatt = new DrawingGroup();
            mZeitstrichdicke = Zeitstriche;
            ZeichneKreis();
            ZeichneMittelpunkt();
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

            // Ziffern zeichnen (für spater)
            ZeichneZiffern();

            VollstaendigesZiffernblatt.Children.Clear();
            VollstaendigesZiffernblatt.Children.Add(gdZiffernblatt);
            VollstaendigesZiffernblatt.Children.Add(gdZeitstrich);
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

                gdZeitstrich = new GeometryDrawing
                {
                    Geometry = ggZeitstriche,
                    Brush = mZeitstrichfarbe
                };
            }
        }
        private void ZeichneMittelpunkt()
        {
            EllipseGeometry mittelpunktKreis = new EllipseGeometry
            {
                Center = mMittelpunkt,
                RadiusX = mRadius * 0.05,
                RadiusY = mRadius * 0.05,
            };

            gdMittelpunkt.Geometry = mittelpunktKreis;
            gdMittelpunkt.Brush = Brushes.Black;
        }
        private void ZeichneZiffern()
        {
            //Noch lehr, da noch nicht in der Basisversion verfügbar :D
        }
    }
}
