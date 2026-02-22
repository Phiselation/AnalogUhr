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
        private Point mptMittelpunkt;
        public int Länge
        {
            get { return mLänge; }
            set { mLänge = value; }
        }
        public Zeiger(Point Mittelpunkt, double Radius, int Länge, Pen ZeigerPen)
            : base(Mittelpunkt, Radius)
        { 
            mptMittelpunkt = Mittelpunkt;
            mZeigerWinkel = Radius;
            mLänge = Länge;
            mZeigerPen = ZeigerPen;
        }
        public DrawingGroup CreateZeiger()
        {
            UhrZeiger = new LineGeometry
            {
                StartPoint = mptMittelpunkt,
                EndPoint = new Point(mptMittelpunkt.X, mptMittelpunkt.Y - mLänge)
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
                CenterX = mptMittelpunkt.X,
                CenterY = mptMittelpunkt.Y
            };

            dgZeiger.Transform = rtZeiger;

            return dgZeiger;
        }
        public void Set(double iWinkel)
        {
            mZeigerWinkel = iWinkel;
            if (rtZeiger != null)
            {
                rtZeiger.Angle = mZeigerWinkel; // transform sofort aktualisieren, wenn vorhanden
            }
        }
    }
}
