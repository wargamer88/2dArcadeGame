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

		public ScoreBoard () : base(800,128)
		{
			_life = new Life ();
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
			graphics.Clear (Color.Empty);
			DrawScore (score);
			//DrawHealth (health);
		}
		private void DrawScore(int score)
		{
			string message = score.ToString ();
			font = new Font ("Arial", 60, FontStyle.Bold);
			position = new PointF (200, 0);
			brush = new SolidBrush (Color.DarkGreen);
			graphics.DrawString (message, font, brush, position);
		}

		private void DrawHealth(int health)
		{
			string message;
			if (health > 0)
				message = "Health: " + health;
			else {
				message = "You have died.";
				brush = new SolidBrush (Color.Red);
			}
			position = new PointF (400, 0);
			graphics.DrawString (message, font, brush, position);
		}
	}
}

