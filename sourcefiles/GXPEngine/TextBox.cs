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
			brush = new SolidBrush (Color.White);
			position = new PointF (0, 64);
			this.y = this.y + 64;
		}

		public void ClearTextBox()
		{
			graphics.Clear (Color.Empty);
		}

		public void DrawTextBox(string name, string text)
		{
			graphics.Clear (Color.Black);
			DrawText (name, text);
		}
		private void DrawText(string name, string text)
		{
			string message = "The " + name + " says: \"" + text + "\".";
			position = new PointF (0, 32);
			brush = new SolidBrush (Color.White);
			graphics.DrawString (message, font, brush, position);
		}
	}
}

