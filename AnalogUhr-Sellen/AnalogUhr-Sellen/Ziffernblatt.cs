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
using System.Globalization;

namespace AnalogUhr_Sellen
{
    class Ziffernblatt : UhrBasis
    {
        private Pen mPen;
        private RectangleGeometry mZeitstrich;
        private EllipseGeometry ZiffernblattDrawing;
        private Point mMittelpunkt;

        private int mRadius;
        private int mKreisdicke; 
        private int mZeitstrichdicke;

        private Brush mZeitstrichfarbe;
        private Brush mKreisfarbe;
        private Brush mZiffernfarbe;

        private GeometryGroup ggZiffernblatt;
        private GeometryGroup ggZeitstriche;
        private GeometryGroup ggZiffern;
        
        private GeometryDrawing gdZiffernblatt;
        private GeometryDrawing gdZeitstrich;
        private GeometryDrawing gdZiffer;

        public GeometryDrawing gdMittelpunkt { get; private set; }

        public DrawingGroup VollstaendigesZiffernblatt { get; private set; }

        public Ziffernblatt(Point Mittelpunkt, int Radius,
                            Brush Kreisfarbe, int Kreisdicke,
                            Brush ZeitstrichFarbe, int Zeitstrichdicke,
                            Brush Ziffernfarbe)
            : base(Mittelpunkt, Radius)
        {
            mMittelpunkt = Mittelpunkt;
            mKreisfarbe = Kreisfarbe;
            mKreisdicke = Kreisdicke;
            mRadius = Radius;
            mPen = new Pen(mKreisfarbe, mKreisdicke);
            mZeitstrichfarbe = ZeitstrichFarbe;
            mZiffernfarbe = Ziffernfarbe;
            mZeitstrichdicke = Zeitstrichdicke;

            ggZiffernblatt = new GeometryGroup();
            ggZeitstriche = new GeometryGroup();
            ggZiffernblatt = new GeometryGroup();
            ggZiffern = new GeometryGroup();

            gdMittelpunkt = new GeometryDrawing();

            VollstaendigesZiffernblatt = new DrawingGroup();

            
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

            // Ziffern zeichnen
            ZeichneZiffern();

            VollstaendigesZiffernblatt.Children.Clear();
            VollstaendigesZiffernblatt.Children.Add(gdZiffernblatt);
            VollstaendigesZiffernblatt.Children.Add(gdZeitstrich);
            VollstaendigesZiffernblatt.Children.Add(gdZiffer);
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
            for (int i = 1; i <= 12; i++)
            {
                int angle = i * 30 + 180;
                FormattedText text = new FormattedText(
                    i.ToString(),
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Times New Roman"),
                    mRadius * 0.1,
                    mZiffernfarbe,
                    VisualTreeHelper.GetDpi(Application.Current.MainWindow).PixelsPerDip);

                Geometry textGeometry = text.BuildGeometry(
                    new Point(
                        mMittelpunkt.X - text.Width /2,
                        mMittelpunkt.Y));

                TransformGroup transformoperations = new TransformGroup();
                transformoperations.Children.Add(new RotateTransform(-angle, mRadius, mRadius));
                transformoperations.Children.Add(new RotateTransform(angle, mRadius, mRadius * 0.3));
                transformoperations.Children.Add(new TranslateTransform(0, mRadius * 0.65));
                //Transformationsreihenfolge: try & error, bis die Ziffern an der richtigen Stelle sind; mit Jakob getüftelt

                textGeometry.Transform = transformoperations;

                ggZiffern.Children.Add(textGeometry);

                gdZiffer = new GeometryDrawing
                {
                    Geometry = ggZiffern,
                    Brush = mZiffernfarbe
                };
            }
        }
    }
}
