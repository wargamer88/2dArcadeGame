using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    static class Sounds
    {

        public static void BgMusic()
        {
            Sound music = new Sound(@"Sounds/UncleBibby_-_01_-_The_Simple_Complex.mp3", true, true);
            music.Play();
        }

        public static void PlayJump()
        {
            Sound soundJump = new Sound(@"Sounds/Jump.wav");
            soundJump.Play();
        }
    }
}
