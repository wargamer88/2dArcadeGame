﻿using System;
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
        private Ground _ground;
        private Player _player;
        private int _levelWidth;
        private int _levelHeight;

        #endregion

        public Level(string sLevel)
        {
            string level = XMLreader(sLevel);
            int[,] levelArray = LevelArrayBuilder(level);
            BuildGameLevel(levelArray);

            AddChild(_player);
        }

        public void Collisions()
        {
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
                    }
                    if (_player.x + _player.width > ground.x && _player.LastXpos + _player.width <= ground.x)
                    {
                        _player.x = ground.x - _player.width;
                        _player.XSpeed = 0;
                    }
                    if (_player.x < ground.x + ground.width && _player.LastXpos >= ground.x + ground.width)
                    {
                        _player.x = ground.x + ground.width;
                        _player.XSpeed = 0;
                    }
                }
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

                    }

                }
            }
        }
    }
}
