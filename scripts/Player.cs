using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float MoveSpeed = 250.0f;

	private Vector2 inputDirection = Vector2.Zero;

	private AudioStreamPlayer2D collisionSound;

	private float collisionSoundTimer = 0.0f;
	
	[Export]
	public float CollisionSoundCooldown = 0.15f;

	public override void _Ready()
	{
		collisionSound = GetNode<AudioStreamPlayer2D>("CollisionSound");
	}

	public override void _Process(double delta)
	{
		ProcessInput();
		UpdateState(delta);
		UpdateCollisionSoundTimer(delta);
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
		UpdateMovement();
	}

	private void UpdateMovement()
	{
		Velocity = inputDirection * MoveSpeed;

		MoveAndSlide();

		if (GetSlideCollisionCount() > 0 &&
			collisionSoundTimer <= 0)
		{
			PlayCollisionSound();

			collisionSoundTimer = CollisionSoundCooldown;
		}
	}

	private void UpdateCollisionSoundTimer(double delta)
	{
		if (collisionSoundTimer > 0)
		{
			collisionSoundTimer -= (float)delta;
		}
	}

	private void PlayCollisionSound()
	{
		if (collisionSound == null ||
			collisionSound.Stream == null)
		{
			return;
		}

		AudioStreamPlayer2D soundInstance =
			new AudioStreamPlayer2D();

		soundInstance.Stream = collisionSound.Stream;
		soundInstance.VolumeDb = collisionSound.VolumeDb;

		AddChild(soundInstance);

		soundInstance.Finished += () =>
		{
			soundInstance.QueueFree();
		};

		soundInstance.Play();
	}
}
