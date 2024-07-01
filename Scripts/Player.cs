using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Player : CharacterBody2D
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
	public override void _PhysicsProcess(double delta)
	{
		    // Move up and down based on input.
		float input = Input.GetActionStrength(_down) - Input.GetActionStrength(_up);
		input += Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		Vector2 movementVec = new Vector2(0, input * paddleSpeed * (float)delta);
		var collision = MoveAndCollide(movementVec);

		if (collision != null)
		{
			GodotObject collider = collision.GetCollider();

			if (collider is Ball ball)
			{
				ball.dir = ball.NewDirection(this, -collision.GetNormal());
			}
		}
			

	}
}
