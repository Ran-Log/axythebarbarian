using Godot;

public partial class PhysicsController : Node
{
	[Export]
	public float MoveSpeed = 250.0f;

	private CharacterBody2D player;
	private StateController state;

	public override void _Ready()
	{
		player = GetParent<CharacterBody2D>();

		state =
			GetParent()
			.GetNode<StateController>("StateController");
	}

	public void UpdateMovement(double delta)
	{
		Vector2 motion =
			state.MoveDirection *
			MoveSpeed *
			(float)delta;

		KinematicCollision2D collision =
			player.MoveAndCollide(motion);

		if (collision == null)
			return;

		GodotObject collider =
			collision.GetCollider();

		bool harmfulCollision = false;

		if (collider is Node node)
		{
			if (node.IsInGroup("enemy") ||
				node.IsInGroup("projectile"))
			{
				harmfulCollision = true;

				GlobalEventListener.Instance.NotifyObservers(
					GameEventType.Lose
				);
			}
		}

		if (!harmfulCollision)
		{
			GlobalEventListener.Instance.NotifyObservers(
				GameEventType.Collision
			);
		}

		Vector2 slideMotion =
			collision.GetRemainder().Slide(
				collision.GetNormal()
			);

		player.MoveAndCollide(slideMotion);
	}
}
