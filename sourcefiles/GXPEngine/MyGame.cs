using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    #region local class variables

    private Level _level;
    private Sprite _sky;
    private string _sLevel;

    #endregion


    public MyGame () : base(800, 600, false)
	{
        _sLevel = "level1.tmx";
        _level = new Level(_sLevel);

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

