using Godot;

public partial class StateController : Node
{
	public Vector2 MoveDirection { get; set; } = Vector2.Zero;

	public bool IsColliding { get; set; } = false;

	public bool IsDead { get; set; } = false;

	public bool HasWon { get; set; } = false;
}
