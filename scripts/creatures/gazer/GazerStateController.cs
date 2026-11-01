using Godot;

public partial class GazerStateController : Node
{
	[Export]
	public float PatrolDistance = 60.0f;

	[Export]
	public float BounceCooldown = 0.15f;

	public bool MovingDown { get; set; } = true;

	public float BounceTimer { get; set; } = 0.0f;

	private float minY;
	private float maxY;
	private bool initialized = false;

	public void Initialize(float startingY)
	{
		minY = startingY - PatrolDistance;
		maxY = startingY + PatrolDistance;

		initialized = true;
	}

	public void UpdateState(
		double delta,
		float currentY
	)
	{
		if (!initialized)
			return;

		UpdateBounceTimer(delta);

		CheckLimits(currentY);
	}

	public bool CanBounce()
	{
		return BounceTimer <= 0.0f;
	}

	public void Bounce()
	{
		MovingDown = !MovingDown;

		BounceTimer = BounceCooldown;
	}

	private void CheckLimits(float currentY)
	{
		if (currentY >= maxY)
		{
			MovingDown = false;
		}
		else if (currentY <= minY)
		{
			MovingDown = true;
		}
	}

	private void UpdateBounceTimer(double delta)
	{
		if (BounceTimer > 0.0f)
		{
			BounceTimer -= (float)delta;
		}
	}
}
