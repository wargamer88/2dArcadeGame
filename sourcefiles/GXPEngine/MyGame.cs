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


    public MyGame () : base(1280, 960, false)
	{
        _sky = new Sprite("images/sky.png");
        _sky.SetScaleXY(2, 2);
        AddChild(_sky);

        _sLevel = "level1.tmx";
        _level = new Level(_sLevel);
        AddChild(_level);

        
        
		
	}
	
	void Update () {
		//empty
	}

	static void Main() {
		new MyGame().Start();
	}
}

