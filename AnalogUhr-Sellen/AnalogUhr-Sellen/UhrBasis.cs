using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnalogUhr_Sellen
{
    class UhrBasis
    {
        private Point mptMittelpunkt;
        private double miRadius;

        public UhrBasis(Point pMittelpunkt, double iRadius)
        {
            mptMittelpunkt = pMittelpunkt;
            miRadius = iRadius;
        }
        public Point Mittelpunkt
        {
            get { return mptMittelpunkt; }
            set { mptMittelpunkt = value; }
        }
        public double Radius
        {
            get { return miRadius; }
            set { miRadius = value; }
        }
    }
}
