using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	public class Sounds
    {
		private SoundChannel _music;
		public void BgMusic(string songname)
        {
			Sound _currentBgMusic = new Sound(@"Sounds/" + songname + ".mp3", true, true);
			_music = _currentBgMusic.Play();
        }

		public void StopMusic()
		{
			_music.Stop ();
		}

        public void PlayJump()
        {
            Sound soundJump = new Sound(@"Sounds/Jump.mp3");
            soundJump.Play();
        }

		public void PlayBatScreetch()
		{
			Sound soundJump = new Sound(@"Sounds/BatScreech.wav");
			soundJump.Play();
		}
    }
}
