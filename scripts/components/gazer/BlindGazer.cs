using Godot;

public partial class BlindGazer : CharacterBody2D
{
	private GazerMovementController movementController;
	private GazerStateController stateController;
	private GazerPhysicsController physicsController;

	public override void _Ready()
	{
		movementController =
			GetNode<GazerMovementController>(
                "MovementController"
			);

		stateController =
			GetNode<GazerStateController>(
                "StateController"
			);
			
		physicsController =
			GetNode<GazerPhysicsController>(
				"PhysicsController"
			);

		stateController.Initialize(Position.Y);
	}

	public override void _PhysicsProcess(double delta)
	{
		stateController.UpdateState(delta, Position.Y);

		Vector2 velocity = movementController.ComputeVelocity();

		physicsController.Move(velocity, delta);
	}
}
