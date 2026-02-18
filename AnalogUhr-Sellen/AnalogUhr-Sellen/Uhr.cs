using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace AnalogUhr_Sellen
{
    class Uhr : UhrBasis
    {
        private Ziffernblatt mNeueUhr;
        private Pen mPen;
        private Rectangle mRectangle;
        private Zeiger mSekunde;
        private Zeiger mMinute;
        private Zeiger mStunde;

        public Uhr(Point pMitte, int durchmesser)
            : base(pMitte, durchmesser / 2)
        {
            ZeichneUhrenteile();
        }
        private void ZeichneUhrenteile()
        {
            mNeueUhr = new Ziffernblatt(
                Mittelpunkt,
                Radius,
                Brushes.Black,       // Kreis-Farbe
                3,                  // Kreis-Dicke
                Brushes.Black,       // Strich-Farbe
                2                   // Strich-Dicke
            );
        }
        public void AnalogUhr(Point ptMitte, int iDM)
        {

        }

        public void ZeichneUhr(Canvas canvas)
        {
            mNeueUhr.Mittelpunkt = Mittelpunkt;
            mNeueUhr.Radius = Radius;
            mNeueUhr.Zeichne(canvas);
        }

        private void updateTime(DateTime SystemTime)
        {
            
        }
    }
}