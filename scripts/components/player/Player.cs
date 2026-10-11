using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float RestartDelay = 1.0f;

	private InputController inputController;
	private PhysicsController physicsController;
	private StateController stateController;
	private AudioController audioController;

	private bool endingGame = false;

	public override void _Ready()
	{
		inputController =
			GetNode<InputController>(
                "InputController"
			);

		physicsController =
			GetNode<PhysicsController>(
                "PhysicsController"
			);

		stateController =
			GetNode<StateController>(
                "StateController"
			);

		audioController =
			GetNode<AudioController>(
                "AudioController"
			);
	}

	public override void _PhysicsProcess(
		double delta
	)
	{
		if (endingGame)
			return;

		inputController.ProcessInput();

		physicsController.UpdateMovement(delta);

		audioController.UpdateAudio(delta);

		if (stateController.IsDead)
		{
			StartLoseSequence();
		}
		else if (stateController.HasWon)
		{
			StartWinSequence();
		}
	}

	private async void StartLoseSequence()
	{
		if (endingGame)
			return;

		endingGame = true;

		audioController.PlayDamageSound();

		await ToSignal(
			GetTree().CreateTimer(RestartDelay),
			SceneTreeTimer.SignalName.Timeout
		);

		GetTree().ReloadCurrentScene();
	}

	private async void StartWinSequence()
	{
		if (endingGame)
			return;

		endingGame = true;

		audioController.PlayWinSound();

		await ToSignal(
			GetTree().CreateTimer(RestartDelay),
			SceneTreeTimer.SignalName.Timeout
		);

		GetTree().ReloadCurrentScene();
	}
}
