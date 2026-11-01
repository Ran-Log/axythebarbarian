using Godot;

public partial class DrunkSkeleton : StaticBody2D
{
	private ShootingController shootingController;
	private TimingController timingController;

	public override void _Ready()
	{
		shootingController =
			GetNode<ShootingController>("ShootingController");

		timingController =
			GetNode<TimingController>("TimingController");
	}

	public override void _Process(double delta)
	{
		if (timingController.UpdateTimer(delta))
		{
			shootingController.ShootArrow();
		}
	}
}
