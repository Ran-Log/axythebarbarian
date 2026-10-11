using Godot;

public partial class GazerStateController : Node
{
	[Export]
	public float MinY = 500.0f;

	[Export]
	public float MaxY = 800.0f;

	[Export]
	public float BounceCooldown = 0.15f;

	public bool MovingDown { get; set; } = true;

	public float BounceTimer { get; set; } = 0.0f;

	public void UpdateState(double delta, float currentY)
	{
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
		if (currentY >= MaxY)
		{
			MovingDown = false;
		}
		else if (currentY <= MinY)
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
