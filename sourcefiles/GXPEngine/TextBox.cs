using System;
using System.Drawing;

namespace GXPEngine
{
	public class TextBox : Canvas
	{
		Font font;
		Brush brush;
		PointF position;
		public TextBox () : base(1280,64)
		{
			font = new Font ("Arial", 20, FontStyle.Regular);
			brush = new SolidBrush (Color.Black);
			position = new PointF (0, 0);
		}

		public void DrawTextBox(string name, string text)
		{
			graphics.Clear (Color.Black);
			DrawText (name, text);

		}
		private void DrawText(string name, string text)
		{
			string message = "The {0} says: " + text + ".";
			position = new PointF (0, 0);
			brush = new SolidBrush (Color.Black);
			graphics.DrawString (message, font, brush, position);
		}
	}
}

