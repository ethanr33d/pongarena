using Godot;
using System;

public partial class Player : StaticBody2D
{
	
	public float winHeight; // GetViewportRect().Size.Y; wasnt working with integer... idk why
	public float pHeight;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		winHeight = GetViewportRect().Size.Y;
		ColorRect colorRect = GetNode<ColorRect>("ColorRect");
		pHeight = colorRect.Size.Y;
		//paddleSpeed = Main.getSpeed();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Using tutorial movement inputs - our game may be simple enough for it
		//although there was an alternative method for taking game input
		if( Input.IsActionPressed("ui_up")){
		//	Position.Y -= PADDLE_SPEED * delta; //NEED TO GET(PADDLE_SPEED) from main.cs
		}else if(Input.IsActionPressed("ui_down")){
		//  Position.Y += PADDLE_SPEED * delta;
		}

		//limit paddle movement to window
		//mathf.clamp keeps values between the ranges
		 Position = new Vector2(
            Position.X,  // Keep the current X position unchanged
            Mathf.Clamp(Position.Y, pHeight / 2, winHeight - pHeight / 2)  // Clamp the Y position
        );

	}
}
