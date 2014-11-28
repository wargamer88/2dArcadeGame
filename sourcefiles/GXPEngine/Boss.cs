using System;

namespace GXPEngine
{
	public class Boss : AnimSprite
	{
		private float _xSpeed = 0.0f; // Horizontal speed
		private float _ySpeed = 0.0f; // Vertical speed
		private float _ySpeedMax = -15; // Maximum speed for jump
		private int _jumps = 0; // Amount of times the entity has jumped already
		private int _maxJumps = 1; // Amount of times the entity can jump after touching the ground
		private int _jumpHeight = 12; // Current jump height
		private float _jumpBoost = 0; // Amount of height to be added to the jump from holding the button longer
		private float _frame = 0.0f; // Frame currently used for the animated sprite
		private float _lastXpos;
		private float _lastYpos;
		private int _firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
		private int _lastFrame = 1; // Last frame for the range of frames to be used in the animation of the sprite
		private int _gravity = 10; // Gravity that is currently affecting the entity
		private bool _jumping = false; // Indicates whether or not the entity has jumped
		//private Weapon _weapon; // Weapon the entity is using
		private bool movingLeft = true;
		private bool _aggressive = false;
		private int _health = 250;
		private int _damageTimer = 0;
		private int _attackTimer = 0;
		private MyGame _MG;
		private float originalStartPoint;
		private Level _level;
		private bool _attacking = false;


		public Boss (MyGame MG, Level level) : base("images/BossAnim.png", 12, 1)
		{
			this.SetOrigin (0, 192);
			originalStartPoint = this.x;
			_MG = MG;
			_level = level;
		}

		void Update()
		{
			_lastXpos = x;
			_lastYpos = y;
			UpdateAnimation (); // Change animation frames
			AIwalking();
			ApplySteering (); // Move horizontally based on player input
			ApplyGravity (); // Move vertically	based on player input
			ApplyDamage ();
		}

		public void ApplyDamage()
		{
			if (_health == 0)
			{
				this.SetAnimationFrames (4, 4);
				this.alpha = this.alpha * 0.9f;
				if (this.DamageTimer <= 0) {
					this.alpha = 0;
					this.Destroy ();
				}

			}
			if (_damageTimer > 0) {
				this.SetAnimationFrames (4, 4);
				_damageTimer--;
				if (_damageTimer % 20 == 1)
					this.alpha = 0;
				else
					this.alpha = 1;
			} else
				this.SetAnimationFrames (0, 2);
		}

		public void TakeDamage(int damage)
		{
			if (this._health > 0 && _damageTimer == 0) {
				if (this._health - damage < 0) {
					this._health = 0;
					_damageTimer = 80;
				} else {
					this._health = this._health - damage;
					_damageTimer = 80;
				}
			}
		}

		public void UpdateAnimation() // Continuously loop through the frames based on the maximum and
		{
			_frame = _frame + 0.2f;
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

			_level.BossCollision(xMovement, yMovement);

			return hasMoved;
		}

		void ApplyGravity()
		{
			bool hasMoved = MoveChar (0, _ySpeed);
			if (_ySpeed <= _gravity)
				_ySpeed += 1;
			if (hasMoved == false) {
				_ySpeed = 0.0f;
			}
		}

		public void TurnAround()
		{
			if (movingLeft)
				movingLeft = false;
			else
				movingLeft = true;
		}

		void ApplySteering() // Apply horizontal speed based on user input
		{
			if (!Attacking && _health > 0) {
				if (movingLeft) {
					_xSpeed--;
					SetAnimationFrames (3, 8);
					this.Mirror (false, false);

				} else if (!movingLeft) {
					_xSpeed++;
					SetAnimationFrames (3, 8);
					this.Mirror (true, false);
				} else {
					SetAnimationFrames (0, 2);

				}
				if (_xSpeed > 4) {
					_xSpeed = 4;
				}
				if (_xSpeed < -4) {
					_xSpeed = -4;
				}
			}
			MoveChar (_xSpeed, 0);
			_xSpeed = _xSpeed * 0.9f;
		}

		public void AIwalking()
		{
			if (_damageTimer == 0 && !_aggressive) {
				if (this.x <= originalStartPoint) {
					movingLeft = false;
				}
				if (this.x >= originalStartPoint + 128) {
					movingLeft = true;
				}
			}
		}

		public void Initiate(bool fromLeft)
		{
			this.Aggressive = true;
			if (fromLeft) {
				this.Mirror (false, false);
				_xSpeed--;
			}
			if (!fromLeft) {
				this.Mirror (true, false);
				_xSpeed++;
			}
		}

		public void Attack(bool inRange)
		{
			if (inRange) {
				this.Attacking = true;
				this.SetAnimationFrames (9, 11);
			} else {
				this.Attacking = false;
				this.SetAnimationFrames (0, 2);
			}
		}

		public bool Aggressive
		{
			get { return this._aggressive; }
			set { this._aggressive = value; }
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

		public bool Attacking
		{
			get{ return this._attacking; }
			set{ this._attacking = value; }
		}
		public float LastXpos { get { return _lastXpos; } }
		public float LastYpos { get { return _lastYpos; } }
		public int DamageTimer { get { return _damageTimer; } }


	}
}

