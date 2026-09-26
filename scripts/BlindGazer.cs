using Godot;

public partial class BlindGazer : Node2D
{
	[Export]
	public float MoveSpeed = 150.0f;

	[Export]
	public float MinY = 150.0f;

	[Export]
	public float MaxY = 450.0f;

	private bool movingDown = true;

	public override void _Process(double delta)
	{
		UpdateState(delta);
	}

	private void UpdateState(double delta)
	{
		float movement = MoveSpeed * (float)delta;

		if (movingDown)
		{
			Position += new Vector2(0, movement);

			if (Position.Y >= MaxY)
			{
				Position = new Vector2(Position.X, MaxY);
				movingDown = false;
			}
		}
		else
		{
			Position -= new Vector2(0, movement);

			if (Position.Y <= MinY)
			{
				Position = new Vector2(Position.X, MinY);
				movingDown = true;
			}
		}
	}
}
