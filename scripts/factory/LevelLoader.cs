using Godot;
using Godot.Collections;

public partial class LevelLoader : Node
{
	[Export]
	public string LevelPath =
		"res://levels/level1.json";

	private LevelFactory factory;

	public override async void _Ready()
	{
		factory =
			GetNode<LevelFactory>(
	            "../LevelFactory"
			);

		await ToSignal(
			GetTree(),
			SceneTree.SignalName.ProcessFrame
		);

		LoadLevel();
	}

	private void LoadLevel()
	{
		if (!FileAccess.FileExists(LevelPath))
		{
			GD.PrintErr(
				$"Level file not found: {LevelPath}"
			);

			return;
		}

		string jsonText =
			FileAccess.GetFileAsString(LevelPath);

		Json json = new Json();

		Error error =
			json.Parse(jsonText);

		if (error != Error.Ok)
		{
			GD.PrintErr(
				$"JSON parse error: {json.GetErrorMessage()}"
			);

			return;
		}

		Dictionary data =
			json.Data.AsGodotDictionary();

		Array objects =
			data["objects"].AsGodotArray();

		foreach (Variant item in objects)
		{
			Dictionary objectData =
				item.AsGodotDictionary();

			string type =
				objectData["type"].AsString();

			float x =
				(float)objectData["x"];

			float y =
				(float)objectData["y"];

			float scaleX =
				objectData.ContainsKey("scaleX")
				? (float)objectData["scaleX"]
				: 1.0f;

			float scaleY =
				objectData.ContainsKey("scaleY")
				? (float)objectData["scaleY"]
				: 1.0f;

			Node2D instance =
				factory.CreateObject(
					type,
					new Vector2(x, y),
					new Vector2(scaleX, scaleY)
				);

			if (instance != null)
			{
				GetParent().AddChild(instance);
			}
		}
	}
}
