using Godot;

public partial class CowardRat : CharacterBody2D
{
	private RatAIController aiController;
	private RatMovementController movementController;

	public override void _Ready()
	{
		aiController =
			GetNode<RatAIController>(
                "AIController"
			);

		movementController =
			GetNode<RatMovementController>(
                "MovementController"
			);
	}

	public override void _PhysicsProcess(
		double delta
	)
	{
		aiController.UpdateAI(delta);

		movementController.UpdateMovement(delta);
	}
}
