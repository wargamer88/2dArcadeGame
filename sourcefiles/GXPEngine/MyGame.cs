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

