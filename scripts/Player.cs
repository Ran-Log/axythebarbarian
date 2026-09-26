using Godot;

public partial class Player : Node2D
{
	[Export]
	public float MoveSpeed = 250.0f;

	private Vector2 inputDirection = Vector2.Zero;

	public override void _Process(double delta)
	{
		ProcessInput();
		UpdateState(delta);
	}

	private void ProcessInput()
	{
		inputDirection = Vector2.Zero;

		if (Input.IsActionPressed("move_up"))
			inputDirection.Y -= 1;

		if (Input.IsActionPressed("move_down"))
			inputDirection.Y += 1;

		if (Input.IsActionPressed("move_left"))
			inputDirection.X -= 1;

		if (Input.IsActionPressed("move_right"))
			inputDirection.X += 1;

		if (inputDirection != Vector2.Zero)
			inputDirection = inputDirection.Normalized();
	}

	private void UpdateState(double delta)
	{
		Position += inputDirection * MoveSpeed * (float)delta;
	}
}
