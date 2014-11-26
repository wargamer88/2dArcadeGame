using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    #region local class variables

    private Level _level;
    private Sprite _sky;
    private Menu _menu;
    private string _sLevel;
    private bool _buttonClicked = false;
    private Button _button;
    private string _sButton;
    private bool _levelLoaded = false;

    #endregion


    public MyGame () : base(1280, 960, false)
	{
        _sky = new Sprite("images/sky.png");
        AddChild(_sky);

        _menu = new Menu();
        AddChild(_menu);

        Sounds.BgMusic();
	
	}
	
	void Update () {
        
        if (_buttonClicked == false)
        {
            _button = _menu.SelectButton();
            if (_button != null)
            {
                _buttonClicked = true;
                _menu.Destroy();
                _menu = null;
                _sButton = _button._buttonType;
            }
        }
        if (_buttonClicked)
        {
            switch (_sButton)
            {
                case "start":
                    LoadLevel("level1.tmx");
                    break;
                case "tutorial":
                    //LoadLevel("tutorial_Level.tmx");
                    //tutorial level needs to be w/o encryption and compression
                    break;
                case "controls":
                    //load controls screen
                    break;
                case "quit":
                    game.Destroy();
                    break;
            }
        }
	}

    public void GameOver()
    {
        _sLevel = "";
        _buttonClicked = false;
        _sButton = "";
        _levelLoaded = false;
        _level.RemoveHUD();
        _level.Destroy();
        
        AddChild(_menu = new Menu());
    }

	public void LoadNextLevel(string slevel)
	{
		int score = this._level.CurrentPlayer.Score;
		int health = this._level.CurrentPlayer.Health;
		this._level.RemoveHUD ();
		this._level.Destroy ();
		_sLevel = slevel;
		_level = new Level (_sLevel, this);
		AddChild (_level);
		this._level.CurrentPlayer.Health = health;
		this._level.CurrentPlayer.Score = score;
		_levelLoaded = true;
	}

    private void LoadLevel(string slevel)
    {
        if (!_levelLoaded)
        {
            _sLevel = slevel;
            _level = new Level(_sLevel, this);
            AddChild(_level);
            _levelLoaded = true;
            _button = null;

        }
        else
        {

            _level.Collisions();
            _level.Scrolling();
            _level.DisplayHUD();
        }
    }

	static void Main() {
		new MyGame().Start();
	}
}

