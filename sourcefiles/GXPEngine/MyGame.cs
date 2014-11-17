using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{	
	public MyGame () : base(800, 600, false)
	{
		//create a canvas
		Canvas canvas = new Canvas(800, 600);

		//add some content
		canvas.graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 400, 300));
		canvas.graphics.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(400, 0, 400, 300));
		canvas.graphics.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(0, 300, 400, 300));
		canvas.graphics.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(400, 300, 400, 300));

		//add canvas to display list
		AddChild(canvas);
	}
	
	void Update () {
		//empty
	}

	static void Main() {
		new MyGame().Start();
	}
}

