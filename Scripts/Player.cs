using Godot;
using System;

public partial class Player : Paddle
{
	private string _up = "ui_up";
	private string _down = "ui_down";

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		// Move up and down based on input.
		float input = Input.GetActionStrength(_down) - Input.GetActionStrength(_up);
		input += Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		Vector2 movementVec = new Vector2(0, input * paddleSpeed * (float)delta);

		MovePaddleAndCollide(movementVec);
	}
}
