using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.SoundOut;
using CSCore.Codecs;
using System.Threading;

namespace musicplayer
{
    class PlaybackController
    {
        public Song[] nowPlaying;
        public int nowPlayingIncrement = 0;

        public void playSong()
        {
            using (IWaveSource soundSource = GetSoundSource())
            {
                using (ISoundOut soundOut = GetSoundOut())
                {
                    //Tell the SoundOut which sound it has to play
                    soundOut.Initialize(soundSource);
                    //Play the sound
                    soundOut.Play();

                    Thread.Sleep(soundSource.GetLength());

                    //Stop the playback
                    soundOut.Stop();
                }
            }
        }

        private ISoundOut GetSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();
            else
                return new DirectSoundOut();
        }

        private IWaveSource GetSoundSource()
        {
            //return any source ... in this example, we'll just play a mp3 file
            return CodecFactory.Instance.GetCodec(@"C:\Users\Pavilion\Music\Saint PEPSI\Hit Vibes\SAINT PEPSI - Hit Vibes - 03 Better.mp3");
        }
    }
}
