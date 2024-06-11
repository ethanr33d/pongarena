using Godot;
using System;
using Godot;
using System;

public partial class Ball : Area2D
{
    public float Speed = 400f;
    public Vector2 direction = Vector2.Left;

    private Vector2 _velocity = new Vector2();

    public override void _Ready()
    {
        StartMovement();
    }

    public override void _Process(double delta)
    {
        Position += _velocity * (float)delta * direction;
    }

    private void StartMovement()
    {
        Random random = new Random();
        double angle = random.NextDouble() * Math.PI * 2;
        _velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Speed;
    }

    public void Start(Vector2 postion)
    {
        Position = postion;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public void HandleCollision()
    {
        _velocity.X = -_velocity.X;
    }
}
