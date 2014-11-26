using System;

namespace GXPEngine
{
	public class Weapon : AnimSprite
	{
		private bool _facingLeft = true;
		private bool _attacking = false;
		private Player _currentPlayer; // Player that is carrying the weapon
		private int _attackTimer = 0; // Time until the player can move again after starting an attack
		private int _damage = 0; // Amount of damage the weapon can inflict
		private float _frame = 0.0f; // Frame currently used for the animated sprite
		private int _firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
		private int _lastFrame = 6; // Last frame for the range of frames to be used in the animation of the sprite

		public Weapon (Player player, int damage) : base ("images/SwordAnim.png", 14 ,1)
		{
			this._currentPlayer = player;
			this.y -=90;
			this.x += 20;
			this.SetOrigin (18, 68);
			this._damage = damage;
			this.alpha = 0;
            
		}

		public void Update()
		{
            if (Attacking && _attackTimer < 0)
            {
                this.SetFrame(0);
                Attack();
			}
			if (Input.GetKeyDown(Key.F) | Input.GetKey(Key.LEFT_CTRL) && !this._currentPlayer.Jumping && this._currentPlayer.DamageTimer < 40 && _attackTimer <= 0)
			{
                _frame = 0.0f;
				this.SetAnimationFrames (0, 6);
				Attack ();
			}

            if (Input.GetKeyDown(Key.G) | Input.GetKey(Key.LEFT_CTRL) && this._currentPlayer.Jumping && _attackTimer <= 0)
            {
				_frame = 0.0f;
				this.SetAnimationFrames (7, 13);
				Attack ();
				_currentPlayer.YSpeed = -15;
			}

			if (Attacking && !this._currentPlayer.Jumping) {
				this._currentPlayer.XSpeed = 0;
				UpdateAnimation ();
			} 
			else if(Attacking && this._currentPlayer.Jumping)
			{
				UpdateAnimation ();
			}

			_attackTimer--;
			
		}

		public void Flip(bool left)
		{
			_facingLeft = left;
			if (_facingLeft) {
				this.Mirror (false, true);
				this.SetOrigin (68, 68);
				if (this.y == -90)
					this.y += 55;

			} else {
				this.Mirror (false, false);
				this.SetOrigin (18, 68);
				if (this.y == -35)
					this.y -= 55;

			}

		}

		public void Attack()
		{
			if (!Attacking && this._currentPlayer.Jumping) {
				this.alpha = 1;
				this.Attacking = true;
				this.AttackTimer = 20;
			}
			else if (!Attacking)
			{
				this.alpha = 1;
				this.Attacking = true;
				this.AttackTimer = 29;
			}
			else {
				this.alpha = 0;
				this.Attacking = false;
			}
		}

		public void UpdateAnimation() // Continuously loop through the frames based on the maximum and
		{
			if (currentFrame > 6) {
				_frame = _frame + 0.3f;
			} else {
			_frame = _frame + 0.2f;
			}
			if (_frame >= _lastFrame + 1)
				_frame = _firstFrame;
			if (_frame < _firstFrame)
				_frame = _firstFrame;
			SetFrame ((int)_frame);
		}

		public void SetAnimationFrames(int first, int last) // Adjust animation frames to be displayed to the specified values
		{
			_firstFrame = first;
			_lastFrame = last;
		}

		public int AttackTimer
		{
			get { return this._attackTimer; }
			set { this._attackTimer = value; }
		}

		public int Damage
		{
			get{ return this._damage; }
			set{ this._damage = value; }
		}

		public bool Attacking
		{
			get{ return this._attacking; }
			set{ this._attacking = value; }
		}
	}
}
