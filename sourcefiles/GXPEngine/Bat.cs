using System;

namespace GXPEngine
{
	public class Bat : AnimSprite
	{
		private float _xSpeed = 0.0f; // Horizontal speed
		private float _ySpeed = 0.0f; // Vertical speed
		private float _ySpeedMax = -15; // Maximum speed for jump
		private int _jumps = 0; // Amount of times the entity has jumped already
		private int _maxJumps = 2; // Amount of times the entity can jump after touching the ground
		private int _jumpHeight = 12; // Current jump height
		private float _jumpBoost = 0; // Amount of height to be added to the jump from holding the button longer
		private float _frame = 0.0f; // Frame currently used for the animated sprite
		private int _firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
		private int _lastFrame = 1; // Last frame for the range of frames to be used in the animation of the sprite
		private int _gravity = -1; // Gravity that is currently affecting the entity
		private bool _jumping = false; // Indicates whether or not the entity has jumped
		//private Weapon _weapon; // Weapon the entity is using
		private bool movingLeft = true;
		private int _health = 50;
		private int _damageTimer = 0;
		private MyGame _MG;
		private float originalStartPoint;
        private Level _level;


		public int DamageTimer { get { return _damageTimer; } }

		private float _lastXpos;
		private float _lastYpos;

		public Bat (int x, int y, MyGame MG, Level level) : base("images/BatAnim.png", 5, 1)
		{
			this.x = x; // Set horizontal position for player at the start 
			this.y = y; // Set vertical position for player at the start
			this.SetOrigin (0, 64);
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
				this._gravity = 9;
				this.alpha = this.alpha * 0.9f;
				if (this.DamageTimer <= 0) {
						this.alpha = 0;
						this.Destroy ();
				}

			}
			if (_damageTimer > 0) {
				this.SetAnimationFrames (4, 4);
				_damageTimer--;

			} else
				this.SetAnimationFrames (0, 3);
		}

		public void TakeDamage(int damage)
		{
			if (this._health > 0 && _damageTimer == 0) {
				if (this._health - damage < 0) {
					this._health = 0;
					_damageTimer = 40;
				} else {
					_MG.Sound.PlayBatScreetch ();
					this._health = this._health - damage;
					_damageTimer = 40;
				}
			}
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

            _level.BatCollision(xMovement, yMovement);

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
			if (movingLeft) {
				_xSpeed--;
				SetAnimationFrames (1, 6);
				this.Mirror (false, false);

			} else if (!movingLeft) {
				_xSpeed++;
				SetAnimationFrames (2, 3);
				this.Mirror (true, false);
			} 
			else {
				SetAnimationFrames (1, 6);

			}
			if (_xSpeed > 4)
			{
				_xSpeed = 4;
			}
			if (_xSpeed < -4)
			{
				_xSpeed = -4;
			}
			MoveChar (_xSpeed, 0);
			_xSpeed = _xSpeed * 0.9f;
		}

		public void AIwalking()
		{
			if (_damageTimer == 0) {
				if (this.x <= originalStartPoint) {
					movingLeft = false;
				}
				if (this.x >= originalStartPoint + 288) {
					movingLeft = true;
				}
			}
		}

		public void Attack()
		{

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

		public float LastXpos { get { return _lastXpos; } }
		public float LastYpos { get { return _lastYpos; } }


	}
}

