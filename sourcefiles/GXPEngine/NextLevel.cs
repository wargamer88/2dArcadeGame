using System;

namespace GXPEngine
{
	public class NextLevel : Sprite
	{
		string _sLevel;
		MyGame _MG;
		public NextLevel (string currentLevel, MyGame MG) : base("images/LevelEnd.png")
		{
			_sLevel = currentLevel;
			_MG = MG;
		}

		public void LoadNext()
		{
            if (_sLevel == "level0.tmx")
            {
                _MG.LoadNextLevel("level1.tmx");
            }
            else if (_sLevel == "level1.tmx")
            {
                _MG.LoadNextLevel("level1.1.tmx");
            }
            else if (_sLevel == "level1.1.tmx")
            {
                _MG.LoadNextLevel("level1.2.tmx");
            }
            else if (_sLevel == "level1.2.tmx")
            {
                _MG.LoadNextLevel("level1.3.tmx");
            }
            else if (_sLevel == "level1.3.tmx")
            {
                _MG.Victory();
                if (!_MG.victory)
                {
                    _MG.ResetTimer();
                }
                _MG.victory = true;
            }

		}
	}
}

