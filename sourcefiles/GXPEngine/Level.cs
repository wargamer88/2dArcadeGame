using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GXPEngine
{
    class Level : GameObject
    {
        #region local class variables

        private List<Ground> _groundList = new List<Ground>();
        private List<Enemy> _enemyList = new List<Enemy>();
        private List<BrokenRock> _brokenRockList = new List<BrokenRock>();
        private List<Sign> _signList = new List<Sign>();
		private List<NPC> _npcList = new List<NPC> ();
        private List<Coin> _coinList = new List<Coin>();
        private List<Spike> _spikeList = new List<Spike>();
        private Ground _ground;
        private Player _player;
        private BrokenRock _brokenRock;
        private Enemy _enemy;
		private NPC _npc;
		private TextBox _textbox = new TextBox();
        private Coin _coin;
        private Spike _spike;
		private ScoreBoard _scoreBoard = new ScoreBoard ();
        private int _levelWidth;
        private int _levelHeight;
        private bool _onTop = true;
        private bool _onBottom = true;
        private bool _allowSideCollision = false;

        #endregion

		public Level(string sLevel, Game game)
        {
            string level = XMLreader(sLevel);
            int[,] levelArray = LevelArrayBuilder(level);
            BuildGameLevel(levelArray);
            foreach (Enemy enemy in _enemyList)
            {
                AddChild(enemy);
            }
            AddChild(_player);
			game.AddChild (_textbox);
			game.AddChild (_scoreBoard);
        }


        public void Collisions()
        {
            #region ground collisions
            foreach (Ground ground in _groundList)
            {
                if(_player.HitTest(ground))
                {
                    if (_player.y > ground.y && _player.LastYpos <= ground.y )
                    {
                        _player.y = ground.y;
                        _player.Jumping = false;
                        _player.Jumps = 0;
                        _player.YSpeed = 0;
                        _onTop = true;
                    }
                    else
                    {
                        _onTop = false;
                    }
                    if (_player.y - _player.height < ground.y + ground.height && _player.LastYpos - _player.height > ground.y + ground.height)
                    {
                        _player.y = ground.y + ground.height + _player.height;
                        _player.Jumping = false;
                        _player.YSpeed = 0;
                        _onBottom = true;
                    }
                    else
                    {
                        _onBottom = false;
                    }

                    if (!_onBottom && !_onTop)
                    {
                        _allowSideCollision = true;
                    }
                    else
                    {
                        _allowSideCollision = false;
                    }

                    if (_player.x + _player.width > ground.x && _player.LastXpos + _player.width <= ground.x && _allowSideCollision)
                    {
                        _player.x = ground.x - _player.width;
                        _player.XSpeed = 0;
                    }
                    if (_player.x < ground.x + ground.width && _player.LastXpos >= ground.x + ground.width && _allowSideCollision)
                    {
                        _player.x = ground.x + ground.width;
                        _player.XSpeed = 0;
                    }
                    
                }

				foreach (Enemy enemy in _enemyList) {
					bool enemyTurning = false;

					if (enemy.HitTest (ground)) {
						if (enemy.y > ground.y && enemy.LastYpos <= ground.y )
						{
							enemy.y = ground.y;
							enemy.Jumping = false;
							enemy.Jumps = 0;
							enemy.YSpeed = 0;
						}
						if (enemy.x + enemy.width > ground.x && enemy.LastXpos + enemy.width <= ground.x)
						{
							enemy.x = ground.x - _enemy.width;
							enemy.XSpeed = 0;
						}
						if (enemy.x < ground.x + ground.width && enemy.LastXpos >= ground.x + ground.width)
						{
							enemy.x = ground.x + ground.width;
							enemy.XSpeed = 0;
						}

					}
                    if (enemyTurning)
                    {
                        enemy.TurnAround();
                    }
				}
            }
            #endregion

            foreach (Spike spike in _spikeList)
            {
                if (_player.HitTest(spike))
                {
					if (_player.DamageTimer == 0) {
						if (_enemy.x > _player.x) {
							_player.XSpeed = -10;
							_player.YSpeed = -6;
						}
						if (_enemy.x < _player.x) {
							_player.XSpeed = +10;
							_player.YSpeed = -6;
						}
					}
					_player.TakeDamage (50);
                    if (_player.y > spike.y && _player.LastYpos <= spike.y)
                    {
                        _player.y = spike.y;
                        _player.Jumping = false;
                        _player.Jumps = 0;
                        _player.YSpeed = 0;
                        _onTop = true;
                    }
                    else
                    {
                        _onTop = false;
                    }
                    if (_player.y - _player.height < spike.y + spike.height && _player.LastYpos - _player.height > spike.y + spike.height)
                    {
                        _player.y = spike.y + spike.height + _player.height;
                        _player.Jumping = false;
                        _player.YSpeed = 0;
                        _onBottom = true;
                    }
                    else
                    {
                        _onBottom = false;
                    }

                    if (!_onBottom && !_onTop)
                    {
                        _allowSideCollision = true;
                    }
                    else
                    {
                        _allowSideCollision = false;
                    }

                    if (_player.x + _player.width > spike.x && _player.LastXpos + _player.width <= spike.x && _allowSideCollision)
                    {
                        _player.x = spike.x - _player.width;
                        _player.XSpeed = 0;
                    }
                    if (_player.x < spike.x + spike.width && _player.LastXpos >= spike.x + spike.width && _allowSideCollision)
                    {
                        _player.x = spike.x + spike.width;
                        _player.XSpeed = 0;
                    }

                }
            }

            #region broken rock collisions
            foreach (BrokenRock brokenRock in _brokenRockList)
            {
                if (_player.HitTest(brokenRock))
                {
                    if (_player.y > brokenRock.y && _player.LastYpos <= brokenRock.y)
                    {
                        _player.y = brokenRock.y;
                        _player.Jumping = false;
                        _player.Jumps = 0;
                        _player.YSpeed = 0;
                    }
                    if (_player.x + _player.width > brokenRock.x && _player.LastXpos + _player.width <= brokenRock.x)
                    {
                        _player.x = brokenRock.x - _player.width;
                        _player.XSpeed = 0;
                    }
                    if (_player.x < brokenRock.x + brokenRock.width && _player.LastXpos >= brokenRock.x + brokenRock.width)
                    {
                        _player.x = brokenRock.x + brokenRock.width;
                        _player.XSpeed = 0;
                    }
                    if (_player.y - _player.height < brokenRock.y + brokenRock.height && _player.LastYpos - _player.height > brokenRock.y + brokenRock.height)
                    {
                        _player.y = brokenRock.y + brokenRock.height + _player.height;
                        _player.Jumping = false;
                        _player.YSpeed = 0;
                    }
                }
            }
            #endregion

            foreach (Coin coin in _coinList)
            {
                if (_player.HitTest(coin))
                {
                    _player.Score++;
                    Console.WriteLine(_player.Score);
                    coin.SetXY(900, 900);
                }
            }


            foreach (Enemy enemy in _enemyList)
            {
				if (_player.Weapon.Attacking) {
					if (enemy.HitTest (_player.Weapon) && _player.Weapon.currentFrame == 3 && _enemy.DamageTimer == 0) {
						enemy.TakeDamage (_player.Weapon.Damage); // get rekt

                        if (_player.x > _enemy.x)
                        {
                            _enemy.XSpeed = -5;
                            _enemy.YSpeed = -3;
                        }
                        if (_player.x < _enemy.x)
                        {
                            _enemy.XSpeed = +5;
                            _enemy.YSpeed = -3;
					}
                        
				}
            }

				if (_player.HitTest(enemy))
                {
					_player.TakeDamage (50);
                    if (_enemy.x > _player.x)
                    {
                        _player.XSpeed = -10;
                        _player.YSpeed = -6;
                    }
                    if (_enemy.x < _player.x)
                    {
                        _player.XSpeed = +10;
                        _player.YSpeed = -6;
                    }
                    
                }
            }

			if (_player.Weapon.Attacking)
            {
                int hitRockIndex = -1;
                int counter = -1;
                foreach (BrokenRock brokenRock in _brokenRockList)
                {
                    counter++;
                    if (_player.Weapon.HitTest(brokenRock))
                    {
                        hitRockIndex = counter;
                    }
                }
                if (hitRockIndex >= 0 && _player.Weapon.currentFrame == 3)
                {
                    BrokenRock BR = _brokenRockList[hitRockIndex];
                    BR.Destroy();
                    _brokenRockList.RemoveAt(hitRockIndex);
                    
                }
            }

			foreach (NPC npc in _npcList) {
				if (_player.HitTest (npc)) {
					npc.SetAnimationFrames (0, 3);
					_textbox.DrawTextBox (npc.Name, npc.Text);
				} else{
					npc.SetAnimationFrames (3, 3);
					_textbox.ClearTextBox ();
				}
			}
        }

		public void DisplayHUD()
		{
			_scoreBoard.DrawStats (_player.Score, _player.Health);
		}

		public void Scrolling()
		{
			if (_player != null) {
				if (_player.x + x > 400)
					x = 400 - _player.x;
				if (_player.x + x < 100)
					x = 100 - _player.x;
			}
		}

        public string XMLreader(string slevel)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"levels\" + slevel);

            XmlElement root = doc.DocumentElement;
            if (root.HasAttribute("width"))
            {
                _levelWidth = Convert.ToInt16(root.GetAttribute("width"));
            }
            if (root.HasAttribute("height"))
            {
                _levelHeight = Convert.ToInt16(root.GetAttribute("height"));
            }

            string level = "";

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                level = node.InnerText;
            }

            return level;

        }

        public int[,] LevelArrayBuilder(string level)
        {
            string[] aLevelString = level.Split(',');
            int[,] aLevelInt = new int[_levelHeight, _levelWidth];

            int indexOfaLevelString = 0;
            for (int h = 0; h < _levelHeight; h++)
            {
                for (int w = 0; w < _levelWidth; w++)
                {
                    aLevelInt[h, w] = Convert.ToInt16(aLevelString[indexOfaLevelString]);
                    indexOfaLevelString++;
                }
            }

            return aLevelInt;
        }

        public void BuildGameLevel(int[,] levelArray)
        {
            for (int h = 0; h < _levelHeight; h++)
            {
                for (int w = 0; w < _levelWidth; w++)
                {
                    int tile = levelArray[h, w];

                    switch (tile)
                    {
                        case 1:
                            _ground = new Ground();
                            AddChild(_ground);
                            _ground.SetXY(w * 64, h * 64);
                            _groundList.Add(_ground);
                            break;
                        case 2:
                            _player = new Player();
                            _player.SetXY(w * 64, h * 64);
                            break;
					    case 3:
						    _enemy = new Enemy (w * 64, h * 64);
                            _enemyList.Add(_enemy);
							break;
                        case 4:
                            _brokenRock = new BrokenRock();
                            AddChild(_brokenRock);
                            _brokenRock.SetXY(w * 64, h * 64);
                            _brokenRockList.Add(_brokenRock);
                            break;
						case 5:
							_npc = new NPC ("Witchdoctor", "Good news everyone!");
							AddChild (_npc);
							_npcList.Add (_npc);
							_npc.SetXY (w * 64, h * 64);
							break;
                        case 6:
                            _coin = new Coin();
                            AddChild(_coin);
                            _coin.SetXY(w * 64, h * 64);
                            _coinList.Add(_coin);
                            break;
                        case 7:
                            _spike = new Spike();
                            AddChild(_spike);
                            _spike.SetXY(w * 64, h * 64);
                            _spikeList.Add(_spike);
                            break;


                    }

                }
            }
        }
    }
}
