using System;
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



        public Menu()
        {
            _button = new Button("start");
            _button.SetXY(500, 300);
            _buttonList.Add(_button);
            AddChild(_button);

            _button = new Button("tutorial");
            _button.SetXY(500, 400);
            _buttonList.Add(_button);
            AddChild(_button);

            _button = new Button("controls");
            _button.SetXY(500, 500);
            _buttonList.Add(_button);
            AddChild(_button);

            _button = new Button("quit");
            _button.SetXY(500, 600);
            _buttonList.Add(_button);
            AddChild(_button);

        }

        public Button clickCheck()
        {
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
