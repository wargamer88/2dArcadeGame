using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    #region local class variables

    private Level _level;
    private Sprite _sky;
    private Menu _menu;
	private Sounds _sounds = new Sounds();
    private string _sLevel;
    private bool _buttonClicked = false;
    private Button _button;
    private string _sButton;
    private bool _levelLoaded = false;
	private int _livesLost;

    #endregion


    public MyGame () : base(1280, 960, false)
	{
		this.LivesLost = 0;
        _sky = new Sprite("images/sky.png");
        AddChild(_sky);

        _menu = new Menu();
        AddChild(_menu);

		if (_sLevel != "level1.2.tmx") {
			_sounds.BgMusic ("Black Vortex");
		} else {
			_sounds.StopMusic ();
			_sounds.BgMusic ("Volatile Reaction");
		}
	}

	public Sounds Sound
	{
		get { return this._sounds; }
	}
	public string CurrentLevel
	{
		get { return this._sLevel; }
	}

	public int LivesLost
	{
		get {return this._livesLost;}
		set {this._livesLost = value;}
	}

	void Update () {

        if (_levelLoaded && Input.GetKeyDown(Key.FIVE))
        {
            _sLevel = "";
            _buttonClicked = false;
            _sButton = "";
            _levelLoaded = false;
            _level.RemoveHUD();
            _level.Destroy();
            this.LivesLost = 0;

            AddChild(_menu = new Menu());
        }
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
		this.LivesLost = 0;

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

