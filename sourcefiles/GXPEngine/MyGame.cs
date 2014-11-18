using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    #region local variables

    Level _level;
    Sprite _sky;

    #endregion


    public MyGame () : base(800, 600, false)
	{
        _level = new Level();

        _sky = new Sprite("images/sky.png");
        AddChild(_sky);
	}
	
	void Update () {
		//empty
	}

	static void Main() {
		new MyGame().Start();
	}
}

