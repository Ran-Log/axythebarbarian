using Godot;

public partial class Arrow : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			StateController state =
				player.GetNode<StateController>(
                    "StateController"
				);

			state.IsDead = true;
		}
	}
}
