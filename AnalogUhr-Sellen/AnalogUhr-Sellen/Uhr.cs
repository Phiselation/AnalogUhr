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
    class Uhr
    {
        private Ziffernblatt mNeueUhr;
        private Zeiger mUhrZeiger;
        private Pen mPen;
        private Rectangle mRectangle;
        private Zeiger mSekunde;
        private Zeiger mMinute;
        private Zeiger mStunde;
        private Point mptMittelpunkt;
        private int miRadius;

        private DrawingGroup mAnalogUhrGruppe = new DrawingGroup();
        public Image UhrImage { get; private set; }

        public Uhr(Point pMitte, int durchmesser)
        {
            mptMittelpunkt = pMitte;
            miRadius = durchmesser / 2;
            Uhrenwerte();
        }
        private void Uhrenwerte()
        {
            mNeueUhr = new Ziffernblatt(
                new Point(miRadius,miRadius),
                miRadius,
                Brushes.Black,       // Kreis-Farbe
                3,                  // Kreis-Dicke
                Brushes.Black,       // Strich-Farbe
                2                   // Strich-Dicke
            );
        }
        public void ZeichneUhr()
        {
            mNeueUhr.ZeichneKreis();
            //mUhrZeiger = new Zeiger(
            //    new Point(miRadius, miRadius),
            //    miRadius,
            //    50,                 // Länge in %
            //    Brushes.Black,
            //    6                   // Dicke
            //);
            mAnalogUhrGruppe.Children.Add(mNeueUhr.VollstaendigesZiffernblatt);
            //mNeueUhr.Mittelpunkt = mptMittelpunkt;
            //mNeueUhr.Radius = miRadius;
            //mNeueUhr.ZeichneKreis(canvas);
        }
        public Image CreateImage()
        {
            ZeichneUhr();
            DrawingImage drawingImage = new DrawingImage(mAnalogUhrGruppe);
            UhrImage = new Image
            {
                Source = drawingImage,
                //Width = miRadius * 2,
                //Height = miRadius * 2
                Stretch = Stretch.None
            };
            TranslateTransform uhrMid = new TranslateTransform
            {
                X = -drawingImage.Width / 2,
                Y = -drawingImage.Height / 2
            };
            UhrImage.RenderTransform = uhrMid;
            return UhrImage;
        }
        private void updateTime(DateTime SystemTime)
        {
            
        }
    }
}