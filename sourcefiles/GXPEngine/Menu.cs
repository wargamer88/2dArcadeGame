using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Glide;

namespace GXPEngine
{
    class Menu : GameObject
    {
        private List<Button> _buttonList = new List<Button>();
        private Button _button;
        private Sprite _selectionArrows = new Sprite("images/buttons/selectionArrows.png");
        private Tweener _tweener = new Tweener();
        private int buttonSelected = 0;


        public Menu()
        {
            Sprite TitleScreen = new Sprite("images/Titlenobuttons.png");
            AddChild(TitleScreen);
            //_selectionArrows.SetScaleXY(0.5f, 0.5f);
            _selectionArrows.SetOrigin(_selectionArrows.width / 2, _selectionArrows.height / 2);

            _button = new Button("start");
            _button.SetXY(750, 550);
            _button.SetOrigin(_button.width / 2, _button.height / 2);
            _buttonList.Add(_button);
            AddChild(_button);
            _tweener.Tween(_button, new { x = 500, y = 500 }, 5, 1);

            _button = new Button("controls");
            _button.SetXY(750, 600);
            _button.SetOrigin(_button.width / 2, _button.height / 2);
            _buttonList.Add(_button);
            AddChild(_button);
            _tweener.Tween(_button, new { x = 500, y = 500 }, 5, 1);

            _button = new Button("quit");
            _button.SetXY(750, 650);
            _button.SetOrigin(_button.width / 2, _button.height / 2);
            _buttonList.Add(_button);
            AddChild(_button);
            _tweener.Tween(_button, new { x = 500, y = 500 }, 5, 1);
        }

        public void Update()
        {
            _tweener.Update(0.1f);
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
                    buttonSelected = _buttonList.Count;
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
