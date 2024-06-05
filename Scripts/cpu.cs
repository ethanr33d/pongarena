using Godot;
using System;

public partial class CPU : StaticBody2D
{
	public float winHeight;
	public float pHeight;
	public float ballPos;
	public float ballDist;

	public override void _Ready()
	{
		winHeight = GetViewportRect().Size.Y;
		pHeight = GetNode<ColorRect>("ColorRect").Size.Y;
	}


	public override void _Process(double delta)
	{
		ballPos = GetNode<CharacterBody2D>("Ball").Position.Y;
		ballDist = Position.Y - ballPos;

	}
}
