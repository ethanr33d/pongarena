using Godot;
using System;

public partial class Player : StaticBody2D
{
	
	public float winHeight; // GetViewportRect().Size.Y; wasnt working with integer... idk why
	public float pHeight;
	public int paddleSpeed = 500;
	private bool active = false;
	private string _up = "ui_up";
	private string _down = "ui_down";
	

	public void Start()
	{
		active = true;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		winHeight = GetViewportRect().Size.Y;
		ColorRect colorRect = GetNode<ColorRect>("ColorRect");
		pHeight = colorRect.Size.Y;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		    // Move up and down based on input.
			float input = Input.GetActionStrength(_down) - Input.GetActionStrength(_up);
			input += Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
			Vector2 position = Position; // Required so that we can modify position.y.
			position += new Vector2(0, input * paddleSpeed * (float)delta);
			position.Y = Mathf.Clamp(position.Y, pHeight/2, winHeight - pHeight / 2);
			Position = position;

	}
}
