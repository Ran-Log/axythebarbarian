using Godot;

public partial class DrunkSkeleton : Node2D
{
	[Export]
	public PackedScene ArrowScene;

	[Export]
	public float ShootInterval = 2.0f;

	[Export]
	public float ShootRadius = 120.0f;

	private float shootTimer = 0.0f;

	private Node2D currentArrow;

	public override void _Process(double delta)
	{
		UpdateState(delta);
	}

	private void UpdateState(double delta)
	{
		shootTimer += (float)delta;

		if (shootTimer >= ShootInterval)
		{
			ShootArrow();
			shootTimer = 0.0f;
		}
	}

	private void ShootArrow()
	{
		if (currentArrow != null)
		{
			currentArrow.QueueFree();
			currentArrow = null;
		}

		currentArrow = ArrowScene.Instantiate<Node2D>();

		float angle = (float)GD.RandRange(0.0, Mathf.Tau);
		float distance = (float)GD.RandRange(0.0, ShootRadius);

		Vector2 randomOffset = new Vector2(
			Mathf.Cos(angle),
			Mathf.Sin(angle)
		) * distance;

		currentArrow.Position = Position + randomOffset;

		GetParent().AddChild(currentArrow);
	}
}
