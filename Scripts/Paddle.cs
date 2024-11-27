using Godot;

public partial class Paddle : CharacterBody2D
{
    public float winHeight;
    public float pHeight;
    public float paddleSpeed = 400f;
    protected bool active = false;

    // Enables movement, paddle will not move until Start()ed
    public void Start()
    {
        active = true;
    }
    public override void _Ready()
    {
        winHeight = GetViewportRect().Size.Y;
        ColorRect colorRect = GetNode<ColorRect>("ColorRect");
        pHeight = colorRect.Size.Y;
    }
    // responsible for moving the paddle and handling any collision with the ball (if any)
    protected void MovePaddleAndCollide(Vector2 motion)
    {
        var collision = MoveAndCollide(motion);

        if (collision != null)
        {
            GodotObject collider = collision.GetCollider();

            if (collider is Ball ball)
            {
                ball.HandlePaddleCollision(this, -collision.GetNormal()); // flip normal for bounce since it is relative to player here
            }
        }
    }
}