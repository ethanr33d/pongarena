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

	public float pHeight;

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
		dir = RandomDirection();

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
			
			if (collider is PhysicsBody2D body) {
				if (collider is Paddle paddle)
				{
					HandlePaddleCollision(paddle, collision.GetNormal());
				}
				else
				{
					GD.Print("ELSE BLOCK HIT");
					dir = dir.Bounce(collision.GetNormal());
				}
				
			} 

		}
	}

	public void HandlePaddleCollision(Paddle paddle, Vector2 normal)
	{
		float distance;
		Vector2 pad = new Vector2();
		Vector2 ball = new Vector2();

		ball.Y = Position.Y;
		pad.Y = paddle.Position.Y;
		distance = ball.Y - pad.Y;

		Vector2 newDirection = new Vector2();

		newDirection.X = dir.Bounce(normal).X;

		newDirection.Y = (distance / (paddle.pHeight / 2)) * MaxYVector;

		dir = newDirection.Normalized();
        speed += Accel;
    }

	public void Start()
	{
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		//StartMovement();
	}
}
