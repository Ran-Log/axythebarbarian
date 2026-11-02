using Godot;

public partial class RatMovementController : Node
{
	private CharacterBody2D rat;
	private RatAIController aiController;

	public override void _Ready()
	{
		rat =
			GetParent<CharacterBody2D>();

		aiController =
			GetParent()
			.GetNode<RatAIController>(
                "AIController"
			);
	}

	public void UpdateMovement(double delta)
	{
		Vector2 motion =
			aiController.Velocity *
			(float)delta;

		KinematicCollision2D collision =
			rat.MoveAndCollide(motion);

		if (collision != null)
		{
			aiController.ChangeWanderDirection();

			Vector2 slideMotion =
				collision.GetRemainder().Slide(
					collision.GetNormal()
				);

			rat.MoveAndCollide(slideMotion);
		}
	}
}
