using Godot;

public partial class AudioController : Node
{
	[Export]
	public float CollisionSoundCooldown = 0.15f;

	private float collisionSoundTimer = 0.0f;

	private AudioStreamPlayer2D collisionSound;
	private AudioStreamPlayer2D damageSound;
	private AudioStreamPlayer2D winSound;

	private StateController state;

	public override void _Ready()
	{
		Node parent = GetParent();

		collisionSound =
			parent.GetNode<AudioStreamPlayer2D>("CollisionSound");

		damageSound =
			parent.GetNode<AudioStreamPlayer2D>("DamageSound");

		winSound =
			parent.GetNode<AudioStreamPlayer2D>("WinSound");

		state =
			parent.GetNode<StateController>("StateController");
	}

	public void UpdateAudio(double delta)
	{
		UpdateTimer(delta);

		if (state.IsColliding &&
			collisionSoundTimer <= 0)
		{
			PlayCollisionSound();

			collisionSoundTimer =
				CollisionSoundCooldown;
		}
	}

	private void UpdateTimer(double delta)
	{
		if (collisionSoundTimer > 0)
		{
			collisionSoundTimer -= (float)delta;
		}
	}

	public void PlayDamageSound()
	{
		if (damageSound != null &&
			damageSound.Stream != null)
		{
			damageSound.Play();
		}
	}

	public void PlayWinSound()
	{
		if (winSound != null &&
			winSound.Stream != null)
		{
			winSound.Play();
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

		soundInstance.Stream =
			collisionSound.Stream;

		soundInstance.VolumeDb =
			collisionSound.VolumeDb;

		GetParent().AddChild(soundInstance);

		soundInstance.Finished += () =>
		{
			soundInstance.QueueFree();
		};

		soundInstance.Play();
	}
}
