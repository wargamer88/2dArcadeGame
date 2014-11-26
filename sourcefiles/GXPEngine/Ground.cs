using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Ground : AnimSprite
    {
        public Ground(int frameNr)
            : base("images/Tileset1.1.png", 6, 1)
        {
            SetFrame(frameNr - 2);
        }
    }
}
