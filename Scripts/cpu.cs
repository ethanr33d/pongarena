using Godot;
using System;

public partial class CPU : StaticBody2D
{
	private float winHeight;
	private float pHeight;
	private float ballPos;
	private float ballDist;

	public override void _Ready()
	{
		winHeight = GetViewportRect().Size.Y;
		pHeight = GetNode<ColorRect>("ColorRect").Size.Y;
	}


	public override void _Process(double delta)
	{
		ballPos = GetNode<Area2D>("Ball").Position.Y;
		ballDist = Math.Abs(Position.Y - ballPos);
		float moveBy = 400 * (float)delta;

		if (ballDist < Math.Abs(moveBy))
		{
			Position = new Vector2(Position.X, ballPos);
		} else
		{
			Position = new Vector2(Position.X, moveBy * Math.Sign(moveBy));
		}

		Position = new Vector2(Position.X, Math.Clamp(Position.Y, pHeight / 2, winHeight - pHeight / 2));
	}
}
