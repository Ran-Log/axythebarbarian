using Godot;

public partial class AudioController : Node, IGameObserver
{
	[Export]
	public float CollisionSoundCooldown = 0.15f;

	private float collisionSoundTimer = 0.0f;

	private AudioStreamPlayer2D collisionSound;
	private AudioStreamPlayer2D damageSound;
	private AudioStreamPlayer2D winSound;

	public override void _Ready()
	{
		Node parent = GetParent();

		collisionSound =
			parent.GetNode<AudioStreamPlayer2D>(
                "CollisionSound"
			);

		damageSound =
			parent.GetNode<AudioStreamPlayer2D>(
                "DamageSound"
			);

		winSound =
			parent.GetNode<AudioStreamPlayer2D>(
                "WinSound"
			);

		GlobalEventListener.Instance.RegisterObserver(this);
	}

	public void UpdateAudio(double delta)
	{
		if (collisionSoundTimer > 0.0f)
		{
			collisionSoundTimer -= (float)delta;
		}
	}

	public void OnNotify(GameEventType gameEvent)
	{
		switch (gameEvent)
		{
			case GameEventType.Collision:

				if (collisionSoundTimer <= 0.0f)
				{
					PlayCollisionSound();

					collisionSoundTimer =
						CollisionSoundCooldown;
				}

				break;

			case GameEventType.Lose:
				PlayDamageSound();
				break;

			case GameEventType.Win:
				PlayWinSound();
				break;
		}
	}

	private void PlayDamageSound()
	{
		if (damageSound != null &&
			damageSound.Stream != null)
		{
			damageSound.Play();
		}
	}

	private void PlayWinSound()
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

	public override void _ExitTree()
	{
		if (GlobalEventListener.Instance != null)
		{
			GlobalEventListener.Instance.RemoveObserver(this);
		}
	}
}
