using Godot;

public partial class Arrow : Area2D
{
	private bool triggered = false;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (triggered)
			return;

		if (body is Player)
		{
			triggered = true;

			GlobalEventListener.Instance?.NotifyObservers(
				GameEventType.Lose
			);
		}
	}
}
