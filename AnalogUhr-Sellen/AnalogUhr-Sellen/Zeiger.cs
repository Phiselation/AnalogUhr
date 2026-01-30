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

        public Zeiger(Point Mittelpunkt, int Radius, int Länge, Pen ZeigerPen)
            : base(Mittelpunkt, Radius)
        {

        }
        public void Set(int iWinkel)
        {
            
        }
    }
}
