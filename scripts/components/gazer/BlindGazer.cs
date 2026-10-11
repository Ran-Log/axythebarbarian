using Godot;

public partial class BlindGazer : CharacterBody2D
{
	private GazerMovementController movementController;
	private GazerStateController stateController;

	public override void _Ready()
	{
		movementController =
			GetNode<GazerMovementController>("MovementController");

		stateController =
			GetNode<GazerStateController>("StateController");
	}

	public override void _PhysicsProcess(double delta)
	{
		stateController.UpdateState(delta, Position.Y);
		movementController.UpdateMovement(delta);
	}
}
