using System;

namespace GXPEngine
{
	public class Weapon : Sprite
	{
		private bool _facingLeft = true;
		private bool _attacking = false;
		private Player _currentPlayer;
		public Weapon (Player player) : base ("images/tempsword.png")
		{
			this._currentPlayer = player;
			this.x += 60;
			this.y -= 120;
			this.Mirror (true, false);
			this.rotation = 0;
			this.SetOrigin (18, 68);
		}

		public void Update()
		{
			if (Input.GetKeyDown(Key.F))
			{
				Attack();
			}
			if (this._currentPlayer.Jumping) {
				this.y = -80;
				if (this._attacking)
					Attack ();
			} else {
				this.y = -60;
			}
		}

		public void Flip(bool left)
		{
			_facingLeft = left;
			if (_facingLeft) {
				this.Mirror (true, true);
				this.SetOrigin (18, 10);
				if (this.x == 60)
					this.x -= 20;
				this._attacking = false;
			} else {
				this.Mirror (true, false);
				this.SetOrigin (18, 68);
				if (this.x == 40) {
					this.x += 20;
				}
				this._attacking = false;
			}

		}

		public void Attack()
		{
			if (!_facingLeft) {
				if (this.rotation != 90) {
					this.rotation = 90;
					this._attacking = true;
				} else {
					this.rotation = 0;
					this._attacking = false;
				}
			} else {
				if (this.rotation != 180) {
					this.rotation = 180;
					this._attacking = true;
				} else {
					this.rotation = 90;
					this._attacking = false;
				}
			}
		}

		public bool Attacking
			{
			get{ return this._attacking; }
			set{ this._attacking = value; }
			}
		}
}
