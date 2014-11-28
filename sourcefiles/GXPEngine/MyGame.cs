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
    private ControlScreen _controls;
    private string _sLevel;
    private bool _buttonClicked = false;
    private Button _button;
    private string _sButton;
    private bool _levelLoaded = false;
	private int _livesLost;
    private int _timer = 100;
    private bool _loadedControls = false;
    private bool _victory = false;

    public bool victory { get { return _victory; } set { _victory = value; } }
    #endregion


    public MyGame () : base(1280, 960, true)
	{
		this.LivesLost = 0;
        _sky = new Sprite("images/sky.png");
        AddChild(_sky);

        _menu = new Menu();
        AddChild(_menu);
		_sounds.BgMusic ("Black Vortex");
	
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

        //if (_victory)
        //{
        //    Victory();
        //}

        _timer--;
        //Console.WriteLine(_timer);
        //Console.WriteLine(this.GetChildren().Clear);
        if (_levelLoaded)
        {
            AddChild(_level.Textbox);
            AddChild(_level.Scoreboard);
        }

        if (_levelLoaded | _loadedControls && Input.GetKeyDown(Key.FIVE))
        {
            _sLevel = "";
            _buttonClicked = false;
            _sButton = "";
            if (_levelLoaded)
            {
                _levelLoaded = false;
                _level.RemoveHUD();
                _level.Destroy();
            }
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
                    LoadControlScreen();
                    break;
                case "quit":
                    game.Destroy();
                    break;
            }
        }
	}

    public void LoadControlScreen()
    {
        if (!_loadedControls)
        {
            _controls = new ControlScreen();
            AddChild(_controls);
            _controls.StartControlsAnimation();
            _loadedControls = true;
        }
        else
        {
            _controls.AnimateControlScreen();
        }
    }

    public void ResetTimer()
    {
        _timer = 100;
    }

    public void GameOver()
    {

		Sound.StopMusic ();
        _sLevel = "";
        _buttonClicked = false;
        _sButton = "";
        _levelLoaded = false;
        _level.RemoveHUD();
        _level.Destroy();
		this.LivesLost = 0;

        _sky = new Sprite("images/defeat.png");

        this.AddChild(_sky);
        if (_timer == 1)
        {
            this.GetChildren().Clear();
            
            _level.RemoveHUD();
            _level.Destroy();
            this.LivesLost = 0;
            _sLevel = "";
            _sButton = "";
            _levelLoaded = false;
            _buttonClicked = false;
            _timer = 300;
            AddChild(_menu = new Menu());
        }
    }


    public void Victory()
    {
        
        _sky = new Sprite("images/victory.png");
        Console.WriteLine(_timer);
        this.AddChild(_sky);
        if (_timer == 1)
        {
            this.GetChildren().Clear();
            _level.CurrentPlayer.Lives = -2;
            _level.RemoveHUD();
            _level.Destroy();
            this.LivesLost = 0;
            _sLevel = "";
            _sButton = "";
            _levelLoaded = false;
            _buttonClicked = false;
            _timer = 300;
            AddChild(_menu = new Menu());
        }
    }

	public void LoadNextLevel(string slevel)
	{
		int score = this._level.CurrentPlayer.Score;
		int health = this._level.CurrentPlayer.Health;
		this.Sound.StopMusic ();
		if (CurrentLevel != "level1.3.tmx") {
			_sounds.BgMusic ("Black Vortex");
		} else {
			_sounds.BgMusic ("Volatile Reaction");
		}
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
			Sound.StopMusic ();
            _sky = new Sprite("images/sky.png");
            AddChild(_sky);

            _sLevel = slevel;
            _level = new Level(_sLevel, this);
            AddChild(_level);
			if (CurrentLevel != "level1.3.tmx") {
				_sounds.BgMusic ("Black Vortex");
			} else {
				_sounds.BgMusic ("Volatile Reaction");
			}
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

