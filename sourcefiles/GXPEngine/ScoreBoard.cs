using System;
using System.Drawing;

namespace GXPEngine
{
	public class ScoreBoard : Canvas
	{
		Font font;
		Brush brush;
		PointF position;
		Life _life;
        MyGame _MG;

		public ScoreBoard (MyGame MG) : base(800,128)
		{
			_life = new Life ();
            _MG = MG;
			font = new Font ("Arial", 20, FontStyle.Regular);
			brush = new SolidBrush (Color.Black);
			position = new PointF (400, 0);
			//this.AddChild(new Sprite("images/HudBar.png"));
			this.AddChild(new Sprite("images/diamonds.png"));
			this.AddChild (_life);
			_life.x += 300;
		}

		public Life Life
		{
			get { return this._life; }
		}

		public void DrawStats(int score, int health)
		{
            if (_life.CurrentLife > -2 || !_MG.victory)
            {
                graphics.Clear (Color.Empty);
			    DrawScore (score);
            }
			
		}
		private void DrawScore(int score)
		{
			string message = score.ToString ();
			font = new Font ("White Rabbit", 70, FontStyle.Bold);
			position = new PointF (160, 15);
			brush = new SolidBrush (Color.DarkGreen);
			graphics.DrawString (message, font, brush, position);
		}


	}
}

