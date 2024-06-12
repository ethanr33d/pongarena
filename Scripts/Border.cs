using Godot;
using System;

public partial class Border : Area2D
{
    [Export]
    private int _bounceDirection = 1;

    public void OnAreaEntered(Area2D area)
    {
        if (area is Ball ball)
        {
            GD.Print("IT WORKED?");
            GD.Print(ball.direction);
            ball.direction = (ball.direction + new Vector2(0, _bounceDirection)).Normalized();
            GD.Print("MAYBE???");
            GD.Print(ball.direction);
        }
    }
}
