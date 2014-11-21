using System;
using System.Drawing;

namespace GXPEngine
{
	public class ScoreBoard : Canvas
	{
		Font font;
		Brush brush;
		PointF position;
		public ScoreBoard () : base(400,64)
		{
			font = new Font ("Arial", 20, FontStyle.Regular);
			brush = new SolidBrush (Color.Black);
			position = new PointF (0, 0);
		}

		public void DrawStats(int score, int health)
		{
			graphics.Clear (Color.Gray);
			DrawScore (score);
			DrawHealth (health);
		}
		private void DrawScore(int score)
		{
			string message = "Score: " + score;
			position = new PointF (0, 0);
			brush = new SolidBrush (Color.Black);
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
			position = new PointF (200, 0);
			graphics.DrawString (message, font, brush, position);
		}
	}
}

