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
		state = GetParent().GetNode<StateController>("StateController");
	}

	public void UpdateMovement(double delta)
	{
		Vector2 motion =
			state.MoveDirection *
			MoveSpeed *
			(float)delta;

		state.IsColliding = false;

		KinematicCollision2D collision =
			player.MoveAndCollide(motion);

		if (collision != null)
		{
			GodotObject collider =
				collision.GetCollider();

			bool harmfulCollision = false;

			if (collider is Node node)
			{
				if (node.IsInGroup("enemy") ||
					node.IsInGroup("projectile"))
				{
					state.IsDead = true;
					harmfulCollision = true;
				}
			}

			if (!harmfulCollision)
			{
				state.IsColliding = true;
			}

			Vector2 slideMotion =
				collision.GetRemainder().Slide(
					collision.GetNormal()
				);

			player.MoveAndCollide(slideMotion);
		}
	}
}
