using Godot;

public partial class ZombieMovementController : Node
{
	private CharacterBody2D zombie;

	private ZombieAIController aiController;

	public override void _Ready()
	{
		zombie =
			GetParent<CharacterBody2D>();

		aiController =
			GetParent()
			.GetNode<ZombieAIController>(
                "AIController"
			);
	}

	public void UpdateMovement(double delta)
	{
		Vector2 motion =
			aiController.Velocity *
			(float)delta;

		KinematicCollision2D collision =
			zombie.MoveAndCollide(motion);

		if (collision != null)
		{
			Vector2 slideMotion =
				collision
				.GetRemainder()
				.Slide(
					collision.GetNormal()
				);

			zombie.MoveAndCollide(
				slideMotion
			);
		}
	}
}
