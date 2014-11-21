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
		private int _gravity = 10; // Gravity that is currently affecting the player
		private bool _jumping = false; // Indicates whether or not the player has jumped
        private int _score = 0;

        private float _lastXpos;
        private float _lastYpos;


        public int Score { get { return _score; } set { _score = value; } }
        public float LastXpos { get { return _lastXpos; } }
        public float LastYpos { get { return _lastYpos; } }

		private Weapon _weapon;

        public Player()
            : base("images/PlayerAnim.png", 22, 1)
		{
			this.SetOrigin (0, 96);
			Weapon weapon = new Weapon (this, 50);
			this.AddChild (weapon);
			_weapon = weapon;

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

			if (Input.GetKey (Key.A) && !Weapon.Attacking) {
				_xSpeed--;
				SetAnimationFrames (0, 5);
				this.Mirror (true, false);
				this._weapon.Flip (true);
				if (this._weapon.rotation == 0)
					this._weapon.rotation = 180;
			} else if (Input.GetKey (Key.D) && !Weapon.Attacking) {
				_xSpeed++;
				SetAnimationFrames (0, 5);
				this.Mirror (false, false);
				this._weapon.Flip (false);
				if (this._weapon.rotation == 180)
					this._weapon.rotation = 0;

			} else if (Weapon.Attacking) {
				SetAnimationFrames (15, 21);
				Weapon.SetAnimationFrames (0, 6);
			}
			else {
				SetAnimationFrames (6, 11);

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
				if (YSpeed < 0) {
					SetAnimationFrames (12, 12);
				} else if (YSpeed > 0) {
					SetAnimationFrames (14, 14);
				} else {
					SetAnimationFrames (13, 13);
				}
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

		public Weapon Weapon
		{
			get{return this._weapon;}
			set{this._weapon = value;}
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
