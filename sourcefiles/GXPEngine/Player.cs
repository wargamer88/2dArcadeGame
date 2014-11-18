using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Player : AnimSprite
    {
		float xSpeed = 0.0f; // Horizontal speed
		float ySpeed = 0.0f; // Vertical speed
		float ySpeedMax = -15; // Maximum speed for jump
		int jumps = 0; // Amount of times the player has jumped already
		int maxJumps = 2; // Amount of times the player can jump after touching the ground
		int jumpHeight = 12; // Current jump height
		float jumpBoost = 0; // Amount of height to be added to the jump from holding the button longer
		float frame = 0.0f; // Frame currently used for the animated sprite
		int firstFrame = 0; // First frame for the range of frames to be used in the animation of the sprite
		int lastFrame = 1; // Last frame for the range of frames to be used in the animation of the sprite
		int playerwidth = 96; // Determine the width of a player
		int gravity = 10; // Gravity that is currently affecting the player
		bool jumping = false; // Indicates whether or not the player has jumped

		public Player () : base("PlayerAnim.png", 5, 1)
		{
			this.x = 300; // Set horizontal position for player at the start 
			this.y = 360; // Set vertical position for player at the start
		}

		public Player (int x, int y) : base("PlayerAnim.png", 5, 1)
		{
			this.x = x; // Set horizontal position for player at the start 
			this.y = y; // Set vertical position for player at the start
		}

		void Update()
		{
			UpdateAnimation (); // Change animation frames
			ApplySteering (); // Move horizontally based on player input
			ApplyGravity (); // Move vertically	based on player input
		}

		void ApplySteering() // Apply horizontal speed based on user input
		{

			if (Input.GetKey (Key.A)) {
				xSpeed--;
				SetAnimationFrames (2, 3);
				this.Mirror (true, false);
			} else if (Input.GetKey (Key.D)) {
				xSpeed++;
				SetAnimationFrames (2, 3);
				this.Mirror (false, false);
			} 
			else {
				SetAnimationFrames (0, 1);

			}
			MoveChar (xSpeed, 0);
			xSpeed = xSpeed * 0.9f;
		}

		void ApplyGravity()
		{
			bool hasMoved = MoveChar (0, ySpeed);
			if (ySpeed <= gravity)
				ySpeed += 1;
			if (hasMoved == false) {
				ySpeed = 0.0f;
			}

			if (Input.GetKey(Key.S))
			{
				MoveChar (0, 4);
				//debug
				//Console.WriteLine ("----------------");
			}

			if (Input.GetKey (Key.SPACE) && jumps < maxJumps) 
			{
				jumpBoost = jumpBoost + 0.2f;
			}

			if (!Input.GetKey(Key.SPACE) && jumpBoost > 0 && jumps < maxJumps)
			{ 
				if (!jumping)
					jumping = true;
				jumpHeight = jumpHeight + (int)jumpBoost;
				jumpBoost = 0;
				ySpeed = -jumpHeight;
				jumpHeight = 12;
				if (x > this.game.width - 1 || x == 72)
					xSpeed = -(xSpeed * 1.5f);
				jumps++;
			}
			if (jumping) {
				SetAnimationFrames (5, 5);
			}
		}

		bool MoveChar(float xMovement, float yMovement)
		{
			bool hasMoved = true;

			x = x + xMovement;
			y = y + yMovement;

			if (ySpeed < ySpeedMax)
				ySpeed = ySpeedMax;

			if (x < playerwidth) {
				x = playerwidth;
				if (y < game.height)
					jumps = 1;
				ySpeed = ySpeed / 1.25f;
				hasMoved = false;
			}
			if (x > (game.width)) {
				x = game.width;
				if (y < game.height)
					jumps = 1;
				hasMoved = false;
			}
			if (y < this.height) {
				y = this.height;;
				hasMoved = false;
			}
			if (y > (game.height) ){
				y = this.game.height;
				jumps = 0;
				jumping = false;
				hasMoved = false;
			}

			return hasMoved;
		}

		public float XSpeed //Return or set the XSpeed of the player
		{
			get{
				return this.xSpeed;
			}
			set{
				this.xSpeed = value;
			}
		}

		public float YSpeed //Return or set the YSpeed of the player
		{
			get{
				return this.ySpeed;
			}
			set{
				this.ySpeed = value;
			}
		}

		public void UpdateAnimation() // Continuously loop through the frames based on the maximum and
		{
			frame = frame + 0.1f;
			if (frame >= lastFrame + 1)
				frame = firstFrame;
			if (frame < firstFrame)
				frame = firstFrame;
			SetFrame ((int)frame);
		}

		void SetAnimationFrames(int first, int last) // Adjust animation frames to be displayed to the specified values
		{
			firstFrame = first;
			lastFrame = last;
		}

    }
}
