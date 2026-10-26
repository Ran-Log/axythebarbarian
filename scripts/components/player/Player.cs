using Godot;

public partial class Player : CharacterBody2D, IGameObserver
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
			GetNode<InputController>("InputController");

		physicsController =
			GetNode<PhysicsController>("PhysicsController");

		stateController =
			GetNode<StateController>("StateController");

		audioController =
			GetNode<AudioController>("AudioController");

		GlobalEventListener.Instance.RegisterObserver(this);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (endingGame)
			return;

		inputController.ProcessInput();

		physicsController.UpdateMovement(delta);

		// Se mantiene para actualizar el cooldown del audio.
		audioController.UpdateAudio(delta);
	}

	public void OnNotify(GameEventType gameEvent)
	{
		if (endingGame)
			return;

		switch (gameEvent)
		{
			case GameEventType.Lose:
				StartLoseSequence();
				break;

			case GameEventType.Win:
				StartWinSequence();
				break;
		}
	}

	private async void StartLoseSequence()
	{
		if (endingGame)
			return;

		endingGame = true;

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

		await ToSignal(
			GetTree().CreateTimer(RestartDelay),
			SceneTreeTimer.SignalName.Timeout
		);

		GetTree().ReloadCurrentScene();
	}

	public override void _ExitTree()
	{
		if (GlobalEventListener.Instance != null)
		{
			GlobalEventListener.Instance.RemoveObserver(this);
		}
	}
}
