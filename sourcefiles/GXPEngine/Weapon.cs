using System;

namespace GXPEngine
{
	public class Weapon : Sprite
	{
		private bool _left = true;
		private bool _attacking = false;
		private Player _currentPlayer;
		public Weapon (Player player) : base ("images/tempsword.png")
		{
			this._currentPlayer = player;
			this.x += 60;
			this.y -= 70;
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
				this.y = -140;
				if (this._attacking)
					Attack ();
			} else {
				this.y = -70;
			}
		}

		public void Flip(bool left)
		{
			_left = left;
			if (_left) {
				this.Mirror (true, true);
				this.SetOrigin (18, 10);
				if (this.x == 60)
					this.x -= 60;
				this._attacking = false;
			} else {
				this.Mirror (true, false);
				this.SetOrigin (18, 68);
				if (this.x == 0) {
					this.x += 60;
				}
				this._attacking = false;
			}

		}

		public void Attack()
		{
			if (!_left) {
				if (this.rotation != 90) {
					this.rotation = 90;
					this._attacking = false;
				} else {
					this.rotation = 0;
					this._attacking = true;
				}
			} else {
				if (this.rotation != 180) {
					this.rotation = 180;
					this._attacking = false;
				} else {
					this.rotation = 90;
					this._attacking = true;
				}
			}
		}
	}
}