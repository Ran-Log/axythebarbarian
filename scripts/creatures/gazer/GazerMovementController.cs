using Godot;

public partial class GazerMovementController : Node
{
	[Export]
	public float MoveSpeed = 150.0f;

	private GazerStateController state;

	public override void _Ready()
	{
		state = GetParent().GetNode<GazerStateController>("StateController");
	}

	public Vector2 ComputeVelocity()
	{
		Vector2 direction = state.MovingDown ? Vector2.Down : Vector2.Up;
		return direction * MoveSpeed;
	}
}
