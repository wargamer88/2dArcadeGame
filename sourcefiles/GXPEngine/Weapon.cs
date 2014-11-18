using System;

namespace GXPEngine
{
	public class Weapon : Sprite
	{
		private bool _left = true;
		private bool _up = false;
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
				if (!this._up)
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
				this.rotation = 180;
				if (this.x == 60)
					this.x -= 60;
				this._up = false;
			} else {
				this.Mirror (true, false);
				this.SetOrigin (18, 68);
				this.rotation = 0;
				if (this.x == 0) {
					this.x += 60;
				}
				this._up = false;
			}

		}

		public void Attack()
		{
			if (!_left) {
				if (this.rotation != 90) {
					this.rotation = 90;
					this._up = false;
				} else {
					this.rotation = 0;
					this._up = true;
				}
			} else {
				if (this.rotation != 180) {
					this.rotation = 180;
					this._up = false;
				} else {
					this.rotation = 90;
					this._up = true;
				}
			}
		}
	}
}