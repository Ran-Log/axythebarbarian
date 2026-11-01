using Godot;

public partial class HungryZombie : CharacterBody2D
{
	private ZombieAIController aiController;

	private ZombieMovementController movementController;

	public override void _Ready()
	{
		aiController =
			GetNode<ZombieAIController>(
                "AIController"
			);

		movementController =
			GetNode<ZombieMovementController>(
                "MovementController"
			);
	}

	public override void _PhysicsProcess(
		double delta
	)
	{
		aiController.UpdateAI(delta);

		movementController.UpdateMovement(
			delta
		);
	}
}
