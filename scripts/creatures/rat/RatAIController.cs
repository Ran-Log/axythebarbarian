using Godot;

public partial class RatAIController : Node
{
	[Export]
	public float MoveSpeed = 70.0f;

	[Export]
	public float FleeDistance = 100.0f;

	[Export]
	public float WanderChange = 1.5f;

	private CharacterBody2D rat;
	private Node2D player;

	private float wanderAngle = 0.0f;

	public Vector2 Velocity { get; private set; }
		= Vector2.Zero;

	public override void _Ready()
	{
		rat = GetParent<CharacterBody2D>();

		Node playerNode =
			GetTree().GetFirstNodeInGroup("player");

		if (playerNode is Node2D player2D)
		{
			player = player2D;
		}

		wanderAngle =
			(float)GD.RandRange(
				0.0,
				Mathf.Tau
			);
	}

	public void UpdateAI(double delta)
	{
		if (player == null)
		{
			Velocity = Vector2.Zero;
			return;
		}

		float distance =
			rat.GlobalPosition.DistanceTo(
				player.GlobalPosition
			);

		Vector2 direction;

		if (distance <= FleeDistance)
		{
			direction = CalculateFlee();
		}
		else
		{
			direction = CalculateWander(delta);
		}

		Velocity =
			direction *
			MoveSpeed;
	}

	private Vector2 CalculateFlee()
	{
		Vector2 direction =
			rat.GlobalPosition -
			player.GlobalPosition;

		if (direction == Vector2.Zero)
		{
			return Vector2.Zero;
		}

		return direction.Normalized();
	}

	private Vector2 CalculateWander(double delta)
	{
		float randomChange =
			(float)GD.RandRange(
				-WanderChange,
				WanderChange
			);

		wanderAngle +=
			randomChange *
			(float)delta;

		Vector2 direction =
			new Vector2(
				Mathf.Cos(wanderAngle),
				Mathf.Sin(wanderAngle)
			);

		return direction.Normalized();
	}

	public void ChangeWanderDirection()
	{
		wanderAngle =
			(float)GD.RandRange(
				0.0,
				Mathf.Tau
			);
	}
}
