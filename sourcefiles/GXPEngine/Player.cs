using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Player : AnimSprite
    {
		private float _xSpeed = 0.0f; // Horizontal speed
		private float _ySpeed = 0.0f; // Vertical speed
		private float _ySpeedMax = -15; // Maximum speed for jump
		private int _jumps = 0; // Amount of times the player has jumped already
		private int _maxJumps = 2; // Amount of times the player can jump after touching the ground
		private int _jumpHeight = 12; // Current jump height
		private float _jumpBoost = 0; // Amount of height to be added to the jump from holding the button longer
		private float _frame = 0.0f; // Frame currently used for the animated sprite
		private int _firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
		private int _lastFrame = 1; // Last frame for the range of frames to be used in the animation of the sprite
		private int _playerwidth = 96; // Determine the width of a player
		private int _gravity = 10; // Gravity that is currently affecting the player
		private bool _jumping = false; // Indicates whether or not the player has jumped
        private int _levelWidth;

        private float _lastXpos;
        private float _lastYpos;

        

        public float LastXpos { get { return _lastXpos; } }
        public float LastYpos { get { return _lastYpos; } }

		private Weapon _weapon;

        public Player(int levelWidth)
            : base("images/PlayerAnim.png", 5, 1)
		{
			this.x = game.width/2; // Set horizontal position for player at the start 
			this.y = game.height/2; // Set vertical position for player at the start
			this.SetOrigin (0, 164);
			Weapon weapon = new Weapon (this);
			this.AddChild (weapon);
			_weapon = weapon;
            _levelWidth = levelWidth;
		}


		void Update()
		{
            _lastXpos = x;
            _lastYpos = y;
			UpdateAnimation (); // Change animation frames
			ApplySteering (); // Move horizontally based on player input
			ApplyGravity (); // Move vertically	based on player input
		}

		void ApplySteering() // Apply horizontal speed based on user input
		{

			if (Input.GetKey (Key.A)) {
				_xSpeed--;
				SetAnimationFrames (2, 3);
				this.Mirror (true, false);
				this._weapon.Flip (true);
 				if (this._weapon.rotation == 0 && !Jumping)
					this._weapon.rotation = 180;
			} else if (Input.GetKey (Key.D)) {
			    _xSpeed++;
				SetAnimationFrames (2, 3);
				this.Mirror (false, false);
				this._weapon.Flip (false);
 				if (this._weapon.rotation == 180 && !Jumping)
					this._weapon.rotation = 0;

			} 
			else {
				SetAnimationFrames (0, 1);

			}
			MoveChar (_xSpeed, 0);
			_xSpeed = _xSpeed * 0.9f;
		}

		void ApplyGravity()
		{
			bool hasMoved = MoveChar (0, _ySpeed);
			if (_ySpeed <= _gravity)
				_ySpeed += 1;
			if (hasMoved == false) {
				_ySpeed = 0.0f;
			}

			if (Input.GetKey(Key.S))
			{
				MoveChar (0, 4);
				//debug
				//Console.WriteLine ("----------------");
			}

			if (Input.GetKey (Key.SPACE) && _jumps < _maxJumps) 
			{
				_jumpBoost = _jumpBoost + 0.2f;
			}

			if (!Input.GetKey(Key.SPACE) && _jumpBoost > 0 && _jumps < _maxJumps)
			{ 
				if (!_jumping)
					_jumping = true;
				_jumpHeight = _jumpHeight + (int)_jumpBoost;
				_jumpBoost = 0;
				this.y--;
				_ySpeed = -_jumpHeight;
                _jumpHeight = 12;
				_jumps++;
			}
			if (_jumping) {
				SetAnimationFrames (5, 5);
			}
		}

		bool MoveChar(float xMovement, float yMovement)
		{
			bool hasMoved = true;

			x = x + xMovement;
			y = y + yMovement;

			if (_ySpeed < _ySpeedMax)
				_ySpeed = _ySpeedMax;

			if (y < 0) {
				y = game.height+1;
				hasMoved = false;
			}
			if (y > game.height){
				y = this.game.height+1;
				_jumps = 0;
				_jumping = false;
				hasMoved = false;
			}
            
			return hasMoved;
		}

		public float XSpeed //Return or set the XSpeed of the player
		{
			get{
				return this._xSpeed;
			}
			set{
				this._xSpeed = value;
			}
		}

		public float YSpeed //Return or set the YSpeed of the player
		{
			get{
				return this._ySpeed;
			}
			set{
				this._ySpeed = value;
			}
		}

		public bool Jumping
		{
			get{
				return this._jumping;
			}
			set{
				this._jumping = value;
			}
		}

		public int Jumps
		{
			get{ return this._jumps; }
			set{ this._jumps = value; }
		}

		public void UpdateAnimation() // Continuously loop through the frames based on the maximum and
		{
			_frame = _frame + 0.1f;
			if (_frame >= _lastFrame + 1)
				_frame = _firstFrame;
			if (_frame < _firstFrame)
				_frame = _firstFrame;
			SetFrame ((int)_frame);
		}

		void SetAnimationFrames(int first, int last) // Adjust animation frames to be displayed to the specified values
		{
			_firstFrame = first;
			_lastFrame = last;
		}

    }
}
