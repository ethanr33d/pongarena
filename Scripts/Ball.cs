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
	public StaticBody2D player;
	public StaticBody2D cpu;

	public override void _Ready()
	{
		win_size = GetViewportRect().Size;
		GD.Randomize();
		NewBall();
		player = GetNode<StaticBody2D>("Player");
		cpu = GetNode<StaticBody2D>("CPU");
		
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

	public override void _PhysicsProcess(double delta)
	{
		//Vector2 motion = dir * speed * (float)delta
		var collision = MoveAndCollide(dir * speed * (float)delta);
		GodotObject collider;

		if (collision!=null)
		{
			collider = collision.GetCollider();

			// if ball hits paddle
			if(collider == player || collider == cpu )
			{
				speed += Accel;
				dir = NewDirection(collider);

			}
			else
			{
				dir = dir.Bounce(collision.GetNormal());
			}
			
		}
	}

	public Vector2 NewDirection(GodotObject collider)
	{
		float ballY = Position.Y;
		float padY = collider.Position.Y;
		float distance = ballY - padY;

		Vector2 newDirection = new Vector2();

		if(dir.X > 0)
		{
			newDirection.X = -1;
		}
		else
		{
			newDirection.X = 1;
		}

		newDirection = (distance / (collider.pHeight / 2)) * MaxYVector;

		return newDirection.Normalized();
	}

	public void Start()
	{
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		//StartMovement();
	}
}
