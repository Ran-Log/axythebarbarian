using Godot;

public partial class BlindGazer : CharacterBody2D
{
	[Export]
	public float MoveSpeed = 150.0f;

	[Export]
	public float MinY = 500.0f;

	[Export]
	public float MaxY = 800.0f;

	[Export]
	public float BounceCooldown = 0.15f;

	private bool movingDown = true;

	private float bounceTimer = 0.0f;

	public override void _Process(double delta)
	{
		UpdateState(delta);
	}

	private void UpdateState(double delta)
	{
		UpdateBounceTimer(delta);
		CheckLimits();
		UpdateMovement();
	}

	private void UpdateMovement()
	{
		if (movingDown)
			Velocity = new Vector2(0, MoveSpeed);
		else
			Velocity = new Vector2(0, -MoveSpeed);

		MoveAndSlide();

		CheckCollisions();
	}

	private void CheckCollisions()
	{
		if (GetSlideCollisionCount() > 0 &&
			bounceTimer <= 0)
		{
			movingDown = !movingDown;
			bounceTimer = BounceCooldown;
		}
	}

	private void CheckLimits()
	{
		if (Position.Y >= MaxY)
		{
			movingDown = false;
		}
		else if (Position.Y <= MinY)
		{
			movingDown = true;
		}
	}

	private void UpdateBounceTimer(double delta)
	{
		if (bounceTimer > 0)
		{
			bounceTimer -= (float)delta;
		}
	}
}
