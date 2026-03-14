using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace AnalogUhr_Sellen
{
    internal class Ton
    {
        private string[] mTonDatei = 
        {   
            "Sounds/Voicy_Hells Bells ACDC.wav", 
            "Sounds/universfield-single-church-bell-2-352062.wav", 
            "Sounds/FahrradKlringel.wav"
        };
        private string mTonName;
        private bool IsPlaying;

        public Ton(string TonName)
        {
            mTonName = TonName;
            Play();
        }
        private SoundPlayer mUhrenSound;
        public void Play()
        {
            if (mTonName == "ACDC")
            {
                IsPlaying = true;
                mUhrenSound = new SoundPlayer(mTonDatei[0]);
            }
            else if (mTonName == "Kirche")
            {
                IsPlaying = true;
                mUhrenSound = new SoundPlayer(mTonDatei[1]);
            }
            else if (mTonName == "Fahrrad")
            {
                IsPlaying = true;
                mUhrenSound = new SoundPlayer(mTonDatei[2]);
            }
            else
            {
                IsPlaying = false;
            }
            
            if(IsPlaying == true)
            {
                mUhrenSound.Play();
            }
        }
    }
}
