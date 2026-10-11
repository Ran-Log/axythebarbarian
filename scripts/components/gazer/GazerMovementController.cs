using Godot;

public partial class GazerMovementController : Node
{
	[Export]
	public float MoveSpeed = 150.0f;

	private CharacterBody2D gazer;
	private GazerStateController state;

	public override void _Ready()
	{
		gazer = GetParent<CharacterBody2D>();
		state = GetParent().GetNode<GazerStateController>("StateController");
	}

	public void UpdateMovement(double delta)
	{
		float direction = state.MovingDown ? 1.0f : -1.0f;

		Vector2 motion = new Vector2(
			0.0f,
			direction * MoveSpeed * (float)delta
		);

		KinematicCollision2D collision =
			gazer.MoveAndCollide(motion);

		if (collision != null && state.CanBounce())
		{
			state.Bounce();
		}
	}
}
