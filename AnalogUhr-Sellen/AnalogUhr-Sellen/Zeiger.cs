using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AnalogUhr_Sellen
{
    class Zeiger : UhrBasis
    {
        private int mLänge;
        private double mZeigerWinkel;
        private Pen mZeigerPen;
        private LineGeometry UhrZeiger;
        private GeometryGroup ggZeiger;
        private GeometryDrawing gdZeiger;
        private DrawingGroup dgZeiger;
        private RotateTransform rtZeiger;
        public int Länge
        {
            get { return mLänge; }
            set { mLänge = value; }
        }
        public Zeiger(Point Mittelpunkt, double Radius, int Länge, Pen ZeigerPen)
            : base(Mittelpunkt, Radius)
        {
            mLänge = Länge;
            mZeigerWinkel = 0;
            mZeigerPen = ZeigerPen;
        }
        public DrawingGroup CreateZeiger()
        {
            UhrZeiger = new LineGeometry
            {
                StartPoint = Mittelpunkt,
                EndPoint = new Point(Mittelpunkt.X, Mittelpunkt.Y - (Radius * mLänge / 100))
            };
            ggZeiger = new GeometryGroup();

            ggZeiger.Children.Add(UhrZeiger);

            gdZeiger = new GeometryDrawing
            {
                Geometry = ggZeiger,
                Pen = mZeigerPen
            };
            dgZeiger = new DrawingGroup();

            dgZeiger.Children.Add(gdZeiger);

            rtZeiger = new RotateTransform
            {
                Angle = mZeigerWinkel,
                CenterX = Mittelpunkt.X,
                CenterY = Mittelpunkt.Y
            };

            dgZeiger.Transform = rtZeiger;

            return dgZeiger;
        }
        public void Set(double iWinkel)
        {
            //mZeigerWinkel = iWinkel;
            mZeigerWinkel = iWinkel;
            if (rtZeiger != null)
            {
                rtZeiger.Angle = mZeigerWinkel; // transform sofort aktualisieren, wenn vorhanden
            }
        }
    }
}
