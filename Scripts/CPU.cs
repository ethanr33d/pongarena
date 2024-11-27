using Godot;
using System;

public partial class CPU : Paddle
{	
	public override void _PhysicsProcess(double delta)
	{
		if (!active) return;
 
        float ballPos = GetNode<CharacterBody2D>("../Ball").Position.Y;
		float ballDist = ballPos - Position.Y;
		float moveBy = paddleSpeed * (float)delta;
		Vector2 motion = new Vector2();

		// compute movement by CPU
		if (ballDist < Math.Abs(moveBy))
		{
			motion.Y = ballDist;
		} else
		{
			motion.Y = moveBy * Math.Sign(ballDist);
		}

		//  clamp to prevent out of bounds movement
		motion = new Vector2(0, Math.Clamp(Position.Y + motion.Y, pHeight / 2, winHeight - pHeight / 2) - Position.Y);

		MovePaddleAndCollide(motion);
	}

	public float GetPHeight()
	{
		return pHeight;
	}
}
