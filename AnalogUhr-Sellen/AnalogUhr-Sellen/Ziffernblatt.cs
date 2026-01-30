using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AnalogUhr_Sellen
{
    class Ziffernblatt : UhrBasis
    {
        private Pen mPen;
        private RectangleGeometry mRect;
        private GeometryGroup ZiffernblattDrawing;

        public Ziffernblatt(Point Mittelpunkt, int Radius, Color Kreisfarbe, int Kreisdicke, Color WeiserFarbe, int WeiserDicke)
            : base(Mittelpunkt, Radius)
        {
            ZiffernblattDrawing
        }
    }
}
