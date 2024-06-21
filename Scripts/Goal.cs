using Godot;
using System;

public partial class Goal : Node2D
{
	public int goalNumber{get; set;}
	// [Signal]
	// public delegate void GoalScored(int goalNumber);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//..this could probably be done better :)
		goalNumber = (Name == "Goal1") ? 1 : 2;
		
	}

	// Called every frame 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}


	private async void OnBodyEntered(Node2D body)
	{
		if (body is Ball)
		{
			var ball = GetNode<Ball>("../Ball");
			if (ball != null)
			{
				await ToSignal(GetTree().CreateTimer(1.5), "timeout");
				ball.NewBall();
			}
			
		}
	}
}
