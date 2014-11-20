using System;

namespace GXPEngine
{
	public class Weapon : Sprite
	{
		private bool _facingLeft = true;
		private bool _attacking = false;
		private Player _currentPlayer;
		private int _attackTimer = 0;
		private int _damage = 0;
		public Weapon (Player player, int damage) : base ("images/tempsword.png")
		{
			this._currentPlayer = player;
			this.x += 60;
			this.y -= 120;
			this.Mirror (true, false);
			this.rotation = 0;
			this.SetOrigin (18, 68);
			this._damage = damage;
		}

		public void Update()
		{
			if (Attacking && _attackTimer < 0)
				Attack ();
			if (Input.GetKeyDown(Key.F) && !this._currentPlayer.Jumping)
			{
				if (_attackTimer <= 0)
				Attack();
			}
			if (this._currentPlayer.Jumping) {
				this.y = -80;
				if (this._attacking)
					Attack ();
			} else {
				this.y = -60;
			}
			if (Attacking) {
				this._currentPlayer.XSpeed = 0;
			}
			_attackTimer--;
		}

		public void Flip(bool left)
		{
			_facingLeft = left;
			if (_facingLeft) {
				this.Mirror (true, true);
				this.SetOrigin (18, 10);
				if (this.x == 60)
					this.x -= 20;
			} else {
				this.Mirror (true, false);
				this.SetOrigin (18, 68);
				if (this.x == 40) {
					this.x += 20;
				}
			}

		}

		public void Attack()
		{
			if (!_facingLeft) {
				if (this.rotation != 90) {
					this.alpha = 1;
					this.rotation = 90;
					this._attacking = true;
					this._attackTimer = 80;
				} else {
					this.rotation = 0;
					this._attacking = false;
					this.alpha = 0;
				}
			} else {
				Console.WriteLine (rotation);
				if (this.rotation != 180) {
					this.alpha = 0;
					this.rotation = 180;
					this._attacking = false;
					this._attackTimer = 80;
				} else {
					this.alpha = 1;
					this.rotation = 90;
					this._attacking = true;
				}
			}
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
