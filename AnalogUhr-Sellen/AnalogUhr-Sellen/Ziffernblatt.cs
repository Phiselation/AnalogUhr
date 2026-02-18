using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnalogUhr_Sellen
{
    class Ziffernblatt : UhrBasis
    {
        private Pen mPen;
        private RectangleGeometry mRect;
        private GeometryGroup ZiffernblattDrawing;
        private Point mMittelpunkt;
        private int mRadius;
        private Brush mKreisfarbe;
        private int mKreisdicke;
        private Brush mZeitstrichfarbe;
        private int mZeitstrichdicke;

        public Ziffernblatt(Point Mittelpunkt, int Radius,
                            Brush Kreisfarbe, int Kreisdicke,
                            Brush ZeitstrichFarbe, int Zeitstriche)
            : base(Mittelpunkt, Radius)
        {
            mMittelpunkt = Mittelpunkt;
            mRadius = Radius;
            mKreisfarbe = Kreisfarbe;
            mKreisdicke = Kreisdicke;
            mZeitstrichfarbe = ZeitstrichFarbe;
            mZeitstrichdicke = Zeitstriche;
        }
        public void Zeichne(Canvas canvas)
        {
            // Äußerer Kreis
            Ellipse kreis = new Ellipse
            {
                Width = Radius * 2,
                Height = Radius * 2,
                Stroke = mKreisfarbe,
                StrokeThickness = mKreisdicke,
                Fill = Brushes.White
            };

            Canvas.SetLeft(kreis, Mittelpunkt.X - Radius);
            Canvas.SetTop(kreis, Mittelpunkt.Y - Radius);
            canvas.Children.Add(kreis);

            // Zeitstriche zeichnen
            ZeichneZeitstriche(canvas);

            // Ziffern zeichnen
            ZeichneZiffern(canvas);

            // Mittelpunkt-Punkt
            Ellipse mittelpunktKreis = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black
            };
            Canvas.SetLeft(mittelpunktKreis, Mittelpunkt.X - 5);
            Canvas.SetTop(mittelpunktKreis, Mittelpunkt.Y - 5);
            canvas.Children.Add(mittelpunktKreis);
        }
        private void ZeichneZeitstriche(Canvas canvas)
        {
            for (int i = 0; i < 60; i++)
            {
                double winkel = i * 6 - 90; // 6 Grad pro Minute, -90 für Start oben
                double winkelRad = winkel * Math.PI / 180;

                // Unterschiedliche Längen für Stunden (15 Min) und 5-Minuten-Marken
                double laenge;
                double dicke;

                if (i % 15 == 0) // Stunden-Marken
                {
                    laenge = Radius * 0.15;
                    dicke = mStrichDicke * 2;
                }
                else if (i % 5 == 0) // 5-Minuten-Marken
                {
                    laenge = Radius * 0.1;
                    dicke = mStrichDicke * 1.5;
                }
                else // Minuten-Marken
                {
                    laenge = Radius * 0.05;
                    dicke = mStrichDicke;
                }

                // Start- und Endpunkt des Strichs berechnen
                double startX = Mittelpunkt.X + (Radius - laenge) * Math.Cos(winkelRad);
                double startY = Mittelpunkt.Y + (Radius - laenge) * Math.Sin(winkelRad);
                double endX = Mittelpunkt.X + Radius * Math.Cos(winkelRad);
                double endY = Mittelpunkt.Y + Radius * Math.Sin(winkelRad);

                Line strich = new Line
                {
                    X1 = startX,
                    Y1 = startY,
                    X2 = endX,
                    Y2 = endY,
                    Stroke = mStrichFarbe,
                    StrokeThickness = dicke
                };

                canvas.Children.Add(strich);
            }
        }
        private void ZeichneZiffern(Canvas canvas)
        {
        }
    }
}
