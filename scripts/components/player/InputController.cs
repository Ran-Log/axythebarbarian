using Godot;

public partial class InputController : Node
{
	private StateController state;

	public override void _Ready()
	{
		state = GetParent().GetNode<StateController>("StateController");
	}

	public void ProcessInput()
	{
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("move_up"))
			direction.Y -= 1;

		if (Input.IsActionPressed("move_down"))
			direction.Y += 1;

		if (Input.IsActionPressed("move_left"))
			direction.X -= 1;

		if (Input.IsActionPressed("move_right"))
			direction.X += 1;

		if (direction != Vector2.Zero)
			direction = direction.Normalized();

		state.MoveDirection = direction;
	}
}
