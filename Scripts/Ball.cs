using Godot;
using System;

public partial class Ball : CharacterBody2D
{

	public Vector2 win_size;
	public const float StartSpeed = 500f;
	public const float Accel = 50f;
	public float speed;
	public Vector2 dir;
	public const float MaxYVector = 0.6f;


    public override void _Ready()
    {
		win_size = GetViewportRect().Size;
		GD.Randomize();
		NewBall();
    }

	public void NewBall()
	{
		var random = new RandomNumberGenerator();
		Vector2 position = Position;
		position.X = win_size.X / 2;
		random.Randomize();
        position.Y = random.RandiRange(200, (int)win_size.Y - 200);
        Position = position;
		speed = StartSpeed;
		// dir = RandomDirection();

	}

	public static Vector2 RandomDirection()
	{
		Random random = new Random();
		Vector2 newDirection = new Vector2();
		int[] choices = { 1, -1 };

		newDirection.X = choices[random.Next(choices.Length)];
		newDirection.Y = (float)(random.NextDouble() * 2 - 1);

		return newDirection.Normalized();
	}

    public override void _Process(double delta)
	{
		//Vector2 motion = dir * speed * (float)delta
		var collision = MoveAndCollide(dir * speed * (float)delta);
		GodotObject collider;

		if (collision!=null)
		{
			collider = collision.GetCollider();
		}
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

	public void HandleCollision()
	{
		_velocity.X = -_velocity.X;
	}
}
