using System;

namespace GXPEngine
{
	public class FadingBlock : AnimSprite
	{
		private int _fadingCounter;
		private bool _canCollide = true;
		private bool _blockPressed = false;
		private float _frame = 0.0f; // Frame currently used for the animated sprite
		private int _firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
		private int _lastFrame = 1; // Last frame for the range of frames to be used in the animation of the sprite

		public FadingBlock () : base ("images/FlashingTileAnim.png", 6, 1)
		{
			this._fadingCounter = 240;
			this.SetAnimationFrames (0, 5);
		}

		public FadingBlock (int fadingCounter) : base ("images/FlashingTileAnim.png", 6, 1)
		{
			this._fadingCounter = fadingCounter;
		}

		public void Update()
		{	
			UpdateAnimation ();

			if (_blockPressed) {
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
				if (_fadingCounter < 0) {
					_fadingCounter = 240;
					this._blockPressed = false;
					this._canCollide = true;
					this.alpha = 1;
				}
			}
		}

		public bool CanCollide
		{
			get { return this._canCollide; }
		}

		public bool BlockPressed
		{
			get{ return this._blockPressed; }
			set{ this._blockPressed = value; }
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

		public void SetAnimationFrames(int first, int last) // Adjust animation frames to be displayed to the specified values
		{
			_firstFrame = first;
			_lastFrame = last;
		}

	}
}

