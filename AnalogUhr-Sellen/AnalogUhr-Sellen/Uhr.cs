using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

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
        private DispatcherTimer mTimer;

        private DrawingGroup mAnalogUhrGruppe = new DrawingGroup();
        public Image UhrImage { get; private set; }

        public Uhr(Point pMitte, int durchmesser)
        {
            mptMittelpunkt = pMitte;
            miRadius = durchmesser / 2;
            Uhrenwerte();
            Dispatcher uiDispatcher = Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;
            mTimer = new DispatcherTimer (TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, new EventHandler(mTimer_Tick), uiDispatcher);
        }
        private void mTimer_Tick(object sender, EventArgs e)
        {
            updateTime();
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
            Point lokalMittelpunkt = new Point(miRadius, miRadius);
            mSekunde = new Zeiger(lokalMittelpunkt, miRadius, (int)(miRadius * 0.5), new Pen(new SolidColorBrush(Colors.Black), 2));
            mMinute = new Zeiger(lokalMittelpunkt, miRadius, (int)(miRadius * 0.35), new Pen(new SolidColorBrush(Colors.Red), 2));
            mStunde = new Zeiger(lokalMittelpunkt, miRadius, (int)(miRadius * 0.2), new Pen(new SolidColorBrush(Colors.Blue), 2));
        }

        public void ZeichneUhr()
        {
            mNeueUhr.ZeichneKreis();
            mAnalogUhrGruppe.Children.Add(mNeueUhr.VollstaendigesZiffernblatt);
            
        }
        public Image CreateImage()
        {
            ZeichneUhr();
            updateTime();
            DrawingImage drawingImage = new DrawingImage(mAnalogUhrGruppe);
            UhrImage = new Image
            {
                Source = drawingImage,
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
        private void updateTime()
        {
            double sekundenWinkel = DateTime.Now.Second + DateTime.Now.Millisecond / 1000;
            double minutenWinkel = DateTime.Now.Minute + sekundenWinkel / 60;
            double stundenWinkel = (DateTime.Now.Hour % 12) + minutenWinkel / 60;

            mAnalogUhrGruppe.Children.Clear();
            mAnalogUhrGruppe.Children.Add(mNeueUhr.VollstaendigesZiffernblatt);

            double sekundenGrad = sekundenWinkel * 6; // 360° / 60 Sekunden
            double minutenGrad = minutenWinkel * 6; // 360° / 60 Minuten
            double stundenGrad = stundenWinkel * 30; // 360° / 12 Stunden

            mSekunde.Set(sekundenGrad);
            mMinute.Set(minutenGrad);
            mStunde.Set(stundenGrad);

            mAnalogUhrGruppe.Children.Add(mSekunde.CreateZeiger());
            mAnalogUhrGruppe.Children.Add(mMinute.CreateZeiger());
            mAnalogUhrGruppe.Children.Add(mStunde.CreateZeiger());

            //mAnalogUhrGruppe.Children.Add(new GeometryDrawing
            //{
            //    Geometry = new EllipseGeometry
            //    {
            //        Center = mptMittelpunkt,
            //        RadiusX = miRadius * 0.05,
            //        RadiusY = miRadius * 0.05,
            //    },
            //    Brush = Brushes.Black
            //});
        }
    }
}