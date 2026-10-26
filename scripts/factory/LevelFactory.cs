using Godot;

public partial class LevelFactory : Node
{
	[Export]
	public PackedScene WallScene;

	[Export]
	public PackedScene GazerScene;

	[Export]
	public PackedScene SkeletonScene;

	[Export]
	public PackedScene ExitScene;

	public Node2D CreateObject(
		string type,
		Vector2 position,
		Vector2 scale
	)
	{
		PackedScene scene = null;

		switch (type)
		{
			case "wall":
				scene = WallScene;
				break;

			case "gazer":
				scene = GazerScene;
				break;

			case "skeleton":
				scene = SkeletonScene;
				break;

			case "exit":
				scene = ExitScene;
				break;

			default:
				GD.PrintErr(
					$"Unknown level object type: {type}"
				);
				return null;
		}

		Node2D instance =
			scene.Instantiate<Node2D>();

		instance.Position = position;
		instance.Scale = scale;

		return instance;
	}
}
