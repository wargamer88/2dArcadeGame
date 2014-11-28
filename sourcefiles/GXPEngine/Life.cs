using System;

namespace GXPEngine
{
	public class Life : AnimSprite
	{
		private int _life;

		public Life () : base ("images/LivesAnim.png", 4, 1)
		{

		}

		public void Update()
		{
			switch (_life) {
			case 0:
				this.SetFrame (3);
				break;
			case 1:
				this.SetFrame (2);
				break;
			case 2:
				this.SetFrame (1);
				break;
			case 3:
				this.SetFrame (0);
				break;
			default:
			break;
			}
		}

		public int CurrentLife
		{
			get { return this._life; }
			set { this._life = value; }
		}
	}
}

