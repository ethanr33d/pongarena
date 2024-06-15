using Godot;
using System;

public partial class Ball : Area2D
{
    public float Speed = 400f;
    public Vector2 direction = Vector2.Down;

    private Vector2 _velocity = new Vector2();

    public override void _Process(double delta)
    {
        Position += _velocity * (float)delta;
    }

    private void StartMovement()
    {
        Random random = new Random();
        double angle = random.NextDouble() * Math.PI * 2;
        _velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Speed;
    }

    public void Start()
    {
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
        StartMovement();
    }

    public void RandomDirection()
    {
        Vector2 newDirection = new Vector2();
        //newDirection.X = 
    }

    public void HandleCollision()
    {
        _velocity.X = -_velocity.X;
    }
}
