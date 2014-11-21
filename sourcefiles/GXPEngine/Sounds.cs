using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Sounds
    {
        private Sound _soundExplosion;
        private Sound _soundCoin;
        private Sound _soundJump;

        public Sounds()
        {

        }

        #region loading music & play and loading sounds
        public void BgMusic()
        {
            Sound music = new Sound(@"Sounds/UncleBibby_-_01_-_The_Simple_Complex.mp3", true, true);
            music.Play();
        }

        public void LoadSounds()
        {

        }
        #endregion

        #region play sounds
        public void PlayExplosion()
        {
            _soundExplosion.Play();
        }

        public void PlayCoin()
        {
            _soundCoin.Play();
        }

        public void PlayJump()
        {
            _soundJump.Play();
        }
        #endregion
    }
}
