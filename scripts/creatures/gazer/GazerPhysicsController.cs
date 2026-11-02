using Godot;

public partial class GazerPhysicsController : Node
{
	private CharacterBody2D gazer;
	private GazerStateController state;

	public override void _Ready()
	{
		gazer = GetParent<CharacterBody2D>();
		state = GetParent().GetNode<GazerStateController>("StateController");
	}

	public void Move(Vector2 velocity, double delta)
	{
		KinematicCollision2D collision =
			gazer.MoveAndCollide(velocity * (float)delta);

		if (collision == null)
			return;

		if (collision.GetCollider() is Player)
		{
			GlobalEventListener.Instance?.NotifyObservers(GameEventType.Lose);
		}

		if (state.CanBounce())
		{
			state.Bounce();
		}
	}
}
