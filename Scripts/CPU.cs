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

	public override void _Process(double delta)
	{
		if (!active) return;
 
        ballPos = GetNode<CharacterBody2D>("../Ball").Position.Y;
		ballDist = ballPos - Position.Y;
		float moveBy = PADDLE_SPEED * (float)delta;

		if (ballDist < Math.Abs(moveBy))
		{
			Position = new Vector2(Position.X, ballPos);
		} else
		{
			Position = new Vector2(Position.X, Position.Y + moveBy * Math.Sign(ballDist));
		}

		Position = new Vector2(Position.X, Math.Clamp(Position.Y, pHeight / 2, winHeight - pHeight / 2));
	}

	public float GetPHeight()
	{
		return pHeight;
	}
}
