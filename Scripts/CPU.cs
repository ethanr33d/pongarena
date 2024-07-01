using Godot;
using System;

public partial class CPU : CharacterBody2D
{
	public float PADDLE_SPEED = 400f;

	private float winHeight;
	public float pHeight;
	private float ballPos;
	private float ballDist;
	private bool active = false;
	
	public void Start()
	{
		active = true;
	}
	public override void _Ready()
	{
		winHeight = GetViewportRect().Size.Y;
		pHeight = GetNode<ColorRect>("ColorRect").Size.Y;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!active) return;
 
        ballPos = GetNode<CharacterBody2D>("../Ball").Position.Y;
		ballDist = ballPos - Position.Y;
		float moveBy = PADDLE_SPEED * (float)delta;
		Vector2 motion = new Vector2();

		if (ballDist < Math.Abs(moveBy))
		{
			motion.Y = ballDist;
		} else
		{
			motion.Y = moveBy * Math.Sign(ballDist);
		}

		motion = new Vector2(0, Math.Clamp(Position.Y + motion.Y, pHeight / 2, winHeight - pHeight / 2) - Position.Y);

		var collision = MoveAndCollide(motion);

		if (collision != null)
		{
			GodotObject collider = collision.GetCollider();

			if (collider is Ball ball)
			{
                ball.dir = ball.NewDirection(this, -collision.GetNormal()); // flip normal for bounce since it is relative to player here
            }
		}
	}

	public float GetPHeight()
	{
		return pHeight;
	}
}
