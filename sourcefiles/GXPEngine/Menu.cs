﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Menu : GameObject
    {
        private List<Button> _buttonList = new List<Button>();
        private Button _button;
        private Sprite _selectionArrows = new Sprite("images/buttons/selectionArrows.png");
        private int buttonSelected = 0;


        public Menu()
        {
            _button = new Button("start");
            _button.SetXY(500, 200);
            _buttonList.Add(_button);
            AddChild(_button);

            _button = new Button("tutorial");
            _button.SetXY(500, 400);
            _buttonList.Add(_button);
            AddChild(_button);

            _button = new Button("controls");
            _button.SetXY(500, 600);
            _buttonList.Add(_button);
            AddChild(_button);

            _button = new Button("quit");
            _button.SetXY(500, 800);
            _buttonList.Add(_button);
            AddChild(_button);

        }

        
        

        public Button SelectButton()
        {
            AddChild(_selectionArrows);
            _selectionArrows.SetOrigin(_selectionArrows.width / 2, _selectionArrows.height / 2);
            _selectionArrows.SetXY(_buttonList[buttonSelected].x, _buttonList[buttonSelected].y);

            if (Input.GetKeyDown(Key.DOWN))
            {
                if (buttonSelected + 1 == _buttonList.Count)
                {
                    buttonSelected = -1;
                }
                
                buttonSelected++;
                _selectionArrows.SetXY(_buttonList[buttonSelected].x, _buttonList[buttonSelected].y);
            }
            if (Input.GetKeyDown(Key.UP))
            {
                if (buttonSelected == 0)
                {
                    buttonSelected = 4;
                }
                
                buttonSelected--;
                _selectionArrows.SetXY(_buttonList[buttonSelected].x, _buttonList[buttonSelected].y);
            }

            if (Input.GetKey(Key.SPACE) | Input.GetKey(Key.X))
            {
                return _buttonList[buttonSelected];
            }

            foreach (Button button in _buttonList)
            {
                if(button.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    if (Input.GetMouseButton(0))
                    {
                        return button;
                    }
                }
            }
            return null;
        }
    }
}
