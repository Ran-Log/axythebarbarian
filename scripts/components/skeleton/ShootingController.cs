using Godot;

public partial class ShootingController : Node
{
	[Export]
	public PackedScene ArrowScene;

	[Export]
	public float ShootRadius = 120.0f;

	private Node2D skeleton;
	private Node2D currentArrow;

	public override void _Ready()
	{
		skeleton = GetParent<Node2D>();
	}

	public void ShootArrow()
	{
		if (ArrowScene == null)
			return;

		if (currentArrow != null &&
			IsInstanceValid(currentArrow))
		{
			currentArrow.QueueFree();
		}

		currentArrow =
			ArrowScene.Instantiate<Node2D>();

		float angle =
			(float)GD.RandRange(0.0, Mathf.Tau);

		float distance =
			(float)GD.RandRange(0.0, ShootRadius);

		Vector2 offset =
			new Vector2(
				Mathf.Cos(angle),
				Mathf.Sin(angle)
			) * distance;

		currentArrow.GlobalPosition =
			skeleton.GlobalPosition + offset;

		skeleton.GetParent().AddChild(currentArrow);
	}
}
