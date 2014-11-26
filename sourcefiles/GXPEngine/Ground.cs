using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Ground : AnimSprite
    {
        public Ground(int frameNr)
            : base("images/TilesetGround.png", 6, 1)
        {
            SetFrame(frameNr - 2);
        }
    }
}
