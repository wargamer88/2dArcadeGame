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
			if (_music != null)
				_music.Stop ();
		}

        public void PlayJump()
        {
            Sound soundJump = new Sound(@"Sounds/Jump.wav");
            soundJump.Play();
        }

		public void PlayBatScreetch()
		{
			Sound soundScreech = new Sound(@"Sounds/BatScreech.wav");
			soundScreech.Play();
		}

        public void PlaySwordSwing()
        {
            Sound soundSwordSwing = new Sound(@"Sounds/sword_swipe.mp3");
            soundSwordSwing.Play();
        }

        public void PlayPickupCoin()
        {
            Sound soundPickupCoin = new Sound(@"Sounds/Coin.wav");
            soundPickupCoin.Play();
        }
    }
}
