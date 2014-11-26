﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class BrokenRock : AnimSprite
    {
        AnimSprite _explosion = new AnimSprite("images/dust_explosion.png", 8, 1);
        AnimSprite _shrapnel = new AnimSprite("images/rock_shrapnel.png", 3, 1);

        private float _breakBlockframe = 0.0f;
        private int _breakBlockFirstFrame = 0;
        private int _breakBlockLastFrame = 1;

        private float _shrapnelframe = 0.0f;
        private int _shrapnelFirstFrame = 0;
        private int _shrapnelLastFrame = 1;

        private float _explosionframe = 0.0f;
        private int _explosionFirstFrame = 0;
        private int _explosionLastFrame = 1;

        private bool _breakBlock = false;
        private bool _blockBroken = false;

        public bool BlockBroken { get { return _blockBroken; } }

        public BrokenRock()
            : base("images/break_block.png",8,1)
        {
            _shrapnel.SetXY(this.x-25, this.y);
            _explosion.SetXY(this.x-25, this.y-25);

        }

        public void Update()
        {
            UpdateAnimationBreakBlock();
            UpdateAnimationShrapnel();
            UpdateAnimationExplosion();
        }

        public void AnimateBreakBlock(Player player)
        {
            _breakBlock = true;
            _breakBlockFirstFrame = 0;
            _breakBlockLastFrame = 7;

            AddChild(_shrapnel);
            if (player.x >= this.x + this.width)
            {
                //_shrapnel.rotation = 180;
                //_shrapnel.SetOrigin(_shrapnel.width / 2, _shrapnel.height / 2);
                //_shrapnel.SetXY(this.x, this.y);
                //_shrapnel.x = _shrapnel.x;
            }
            else if (player.x + player.width <= this.x)
            {
                _shrapnel.Mirror(false, false);
            }

            _shrapnelFirstFrame = 0;
            _shrapnelLastFrame = 2;
            

            _explosionFirstFrame = 0;
            _explosionLastFrame = 7;
            AddChild(_explosion);
        }

        private void UpdateAnimationBreakBlock()
        {
            if (_breakBlock)
            {
                _breakBlockframe = _breakBlockframe + 0.3f;
            }
            if (_breakBlockframe >= _breakBlockLastFrame + 0.3f)
            {
                _breakBlockframe = _breakBlockFirstFrame;
            }
            if (_breakBlockframe < _breakBlockFirstFrame)
            {
                _breakBlockframe = _breakBlockFirstFrame;
            }
            if (this.currentFrame >= 7)
            {
                this.Destroy();
            }
            this.SetFrame((int)_breakBlockframe);
        }

        private void UpdateAnimationShrapnel()
        {
            if (_breakBlock)
            {
                _shrapnelframe = _shrapnelframe + 0.2f;
            }
            if (_shrapnelframe >= _shrapnelLastFrame + 0.2f)
            {
                _shrapnelframe = _shrapnelFirstFrame;
            }
            if (_shrapnelframe < _shrapnelFirstFrame)
            {
                _shrapnelframe = _shrapnelFirstFrame;
            }
            if (_shrapnel.currentFrame >= 2)
            {
                _shrapnel.Destroy();
            }
            _shrapnel.SetFrame((int)_shrapnelframe);
        }

        private void UpdateAnimationExplosion()
        {
            if (_breakBlock)
            {
                _explosionframe = _explosionframe + 0.3f;
            }
            if (_explosionframe >= _explosionLastFrame + 0.3f)
            {
                _explosionframe = _explosionFirstFrame;
            }
            if (_explosionframe < _explosionFirstFrame)
            {
                _explosionframe = _explosionFirstFrame;
            }
            if (_explosion.currentFrame >= 7)
            {
                _explosion.Destroy();
            }
            _explosion.SetFrame((int)_explosionframe);
        }


    }
}
