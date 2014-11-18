using System;

namespace GXPEngine
{
	public class Weapon : Sprite
	{
		public Weapon () : base ("images/checkers.png")
		{
			this.SetOrigin (32, 32);
			this.x = this.x + 100;
			this.y = this.y - 100;
		}

		public void Update()
		{
			if (Input.GetKeyDown(Key.F))
				{
					Attack();
				}
		}

		public void Attack()
		{
			this.rotation = 90;
		}
	}
}

