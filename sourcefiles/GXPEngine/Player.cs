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
		private int _health = 100;
		private int _damageTimer = 0;
		private bool _alive = true;

        private float _lastXpos;
        private float _lastYpos;

		public int DamageTimer { get { return _damageTimer; } }
		public int Health { get { return _health; } set { _health = value; } }
        public int Score { get { return _score; } set { _score = value; } }
        public float LastXpos { get { return _lastXpos; } }
        public float LastYpos { get { return _lastYpos; } }

		private Weapon _weapon;

        public Player()
            : base("images/PlayerAnim.png", 67, 1)
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
			ApplyDamage (); // Take damage if hit by monster
		}

		void ApplySteering() // Apply horizontal speed based on user input
		{
			if (_alive) {
				if (Input.GetKey (Key.A) && !Weapon.Attacking && this.DamageTimer < 40) {
					_xSpeed--;
					if (this.Health == 100) {
						SetAnimationFrames (0, 5);
					} else if (this.Health == 50)
						SetAnimationFrames (37, 42);
					this.Mirror (true, false);
					this._weapon.Flip (true);
					if (this._weapon.rotation == 0)
						this._weapon.rotation = 180;
				} else if (Input.GetKey (Key.D) && !Weapon.Attacking && this.DamageTimer < 40) {
					_xSpeed++;
					if (this.Health == 100) {
						SetAnimationFrames (0, 5);
					} else if (this.Health == 50)
						SetAnimationFrames (37, 42);
					this.Mirror (false, false);
					this._weapon.Flip (false);
					if (this._weapon.rotation == 180)
						this._weapon.rotation = 0;

				} else if (Weapon.Attacking) {
					if (Health == 100) {
						SetAnimationFrames (15, 21);
					} else if (this.Health == 50) {
						SetAnimationFrames (53, 59);
					}
					Weapon.SetAnimationFrames (0, 6);
				} else {
					if (this.Health == 100) {
						SetAnimationFrames (6, 11);
					} else if (this.Health == 50)
						SetAnimationFrames (43, 49);

				}
				MoveChar (_xSpeed, 0);
			}
			_xSpeed = _xSpeed * 0.9f;
		}

		void ApplyGravity()
		{
			bool hasMoved = MoveChar (0, _ySpeed);
				if (_ySpeed <= _gravity)
					_ySpeed += 1;
			if (_alive) {
				if (hasMoved == false) {
					_ySpeed = 0.0f;
				}

				if (Input.GetKey (Key.S)) {
					MoveChar (0, 4);
					//debug
					//Console.WriteLine ("----------------");
				}

				if (Input.GetKey (Key.SPACE) && _jumps < _maxJumps) {
					_jumpBoost = _jumpBoost + 0.2f;
				}

				if (!Input.GetKey (Key.SPACE) && _jumpBoost > 0 && _jumps < _maxJumps) { 
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
						if (this.Health == 100) {
							SetAnimationFrames (12, 12);
						} else if (this.Health == 50) {
							SetAnimationFrames (50, 50);
						}
					} else if (YSpeed > 0) {
						if (this.Health == 100) {
							SetAnimationFrames (14, 14);
						} else if (this.Health == 50) {
							SetAnimationFrames (52, 52);
						}
					} else {
						if (this.Health == 100) {
							SetAnimationFrames (13, 13);
						} else if (this.Health == 50) {
							SetAnimationFrames (51, 51);
						}
					}
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

		public void ApplyDamage()
		{
			if (_health == 0)
			{
				this.SetAnimationFrames (32, 36);
				if (this.currentFrame == 36) {
					SetAnimationFrames (36, 36);
					this.alpha = this.alpha * 0.9f;
				}
				_alive = false;
			}
			if (_damageTimer > 0)
			{
				_damageTimer--;
				if (_damageTimer % 20 == 1)
					this.alpha = 0;
				else
					this.alpha = 1;
			}
		}
		
		public bool IsAlive()
		{
			return this._alive;
		}

		public void TakeDamage(int damage)
		{
			if (this._health > 0 && _damageTimer == 0) {
				if (this._health - damage < 0) {
					this._health = 0;
					_damageTimer = 80;
				} else {
					this.SetAnimationFrames (29, 31);
					this._health = this._health - damage;
					_damageTimer = 80;
				}
			}
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
