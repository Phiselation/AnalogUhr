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
        private Pen mZeigerPen;
        private LineGeometry UhrZeiger;
        public int Länge
        {
            get { return mLänge; }
            set { mLänge = value; }
        }
        public Zeiger(Point Mittelpunkt, int Radius, int Länge, Pen ZeigerPen)
            : base(Mittelpunkt, Radius)
        {
            mLänge = Länge;
            mZeigerPen = ZeigerPen;
        }
        public void CreateZeiger()
        {
            UhrZeiger = new LineGeometry
            {
                
            };
        }
        public void Set(int iWinkel)
        {
            if(UhrZeiger != null)
            {
                
            }
        }
        public void PositionsUpdate(int iWinkel)
        {
            Set(iWinkel);
        }
    }
}
