using Godot;

public partial class TimingController : Node
{
	[Export]
	public float ShootInterval = 2.0f;

	private float shootTimer = 0.0f;

	public bool UpdateTimer(double delta)
	{
		shootTimer += (float)delta;

		if (shootTimer >= ShootInterval)
		{
			shootTimer = 0.0f;
			return true;
		}

		return false;
	}
}
