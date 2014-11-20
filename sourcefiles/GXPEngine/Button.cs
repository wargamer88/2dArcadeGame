using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Button : Sprite
    {
        public string _buttonType;
        //static string[] files = new string[] {"images/button.png"};
        public Button(string buttonName)
            : base("images/buttons/" + buttonName + "Button.png")
        {
            _buttonType = buttonName;
            SetOrigin(width / 2, height / 2);
        }
    }
}
