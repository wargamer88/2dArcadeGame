using System;

namespace GXPEngine
{
	public class NPC : AnimSprite
	{
		private float _frame = 0.0f; // Frame currently used for the animated sprite
		private int _firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
		private int _lastFrame = 1; // Last frame for the range of frames to be used in the animation of the sprite
		private string _name;
		private string _text;

		public NPC (string name, string text) : base("images/WDoctor.png", 4, 1)
		{
			SetScaleXY (2, 2);
			this._name = name;
			this._text = text;
			SetAnimationFrames (3, 3);
		}

		public void Update()
		{
			UpdateAnimation ();
		}

		public string Name
		{
			get {return this._name;}
			set {this._name = value;}
		}

		public string Text
		{
			get { return this._text; }
			set {this._text = value;}
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

