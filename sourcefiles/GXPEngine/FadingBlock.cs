using System;

namespace GXPEngine
{
	public class FadingBlock : AnimSprite
	{
		private int _fadingCounter;
		private bool _canCollide = true;
		public FadingBlock () : base ("images/TilesetGround.png", 6, 1)
		{
			this._fadingCounter = 240;
		}

		public FadingBlock (int fadingCounter) : base ("images/TilesetGround.png", 6, 1)
		{
			this._fadingCounter = fadingCounter;
		}

		public void Update()
		{
			Console.WriteLine (_fadingCounter);
			if (this._fadingCounter > 160) {
				this.SetFrame (0);
				this.alpha = 1;
				this._canCollide = true;
			} else if (this._fadingCounter > 120) {
				this.SetFrame (1);
			} else if (this._fadingCounter > 80) {
				this.SetFrame (2);
				if (this._fadingCounter % 10 == 0)
					this.alpha = 0;
				else
					this.alpha = 1;
			} else {
				this.alpha = 0;
				this._canCollide = false;
			}
			_fadingCounter--;
			if (_fadingCounter < 0)
				_fadingCounter = 240;
		}

		public bool CanCollide
		{
			get { return this._canCollide; }
		}

	}
}

