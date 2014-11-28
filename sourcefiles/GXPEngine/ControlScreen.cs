using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class ControlScreen : AnimSprite
    {
        private float _frame = 0.0f; // Frame currently used for the animated sprite
        private int _firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
        private int _lastFrame = 8; // Last frame for the range of frames to be used in the animation of the sprite
        private bool _startAnimation = false;

        public bool StartAnimation { get { return _startAnimation; } }


        public ControlScreen()
            : base("images/controlSpritesheet.png", 9, 1)
        {
            SetFrame(0);
        }

        public void StartControlsAnimation()
        {
            _startAnimation = true;
        }

        public void StopcontrolsAnimation()
        {
            _startAnimation = false;
        }

        public void AnimateControlScreen()
        {
            if (_startAnimation)
            {
                _frame = _frame + 0.2f;
                if (_frame >= _lastFrame + 1)
                    _frame = _firstFrame;
                if (_frame < _firstFrame)
                    _frame = _firstFrame;
                SetFrame((int)_frame);
            }
        }
    }
}
