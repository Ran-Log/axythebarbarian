using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class LevelGraph : Node
{
	[Export]
	public int Columns = 20;

	[Export]
	public int Rows = 15;

	[Export]
	public float CellSize = 40.0f;

	[Export]
	public float WallBaseWidth = 200.0f;

	[Export]
	public float WallBaseHeight = 80.0f;

	[Export]
	public string LevelPath =
		"res://levels/level1.json";

	private GraphNode[,] nodes;

	public override void _Ready()
	{
		BuildGraph();
	}

	private void BuildGraph()
	{
		nodes =
			new GraphNode[Columns, Rows];

		// Crear todas las celdas.
		for (int x = 0; x < Columns; x++)
		{
			for (int y = 0; y < Rows; y++)
			{
				Vector2 position =
					new Vector2(
						x * CellSize +
						CellSize / 2.0f,

						y * CellSize +
						CellSize / 2.0f
					);

				nodes[x, y] =
					new GraphNode(
						x,
						y,
						position
					);
			}
		}

		MarkWallsFromJson();

		GD.Print("LevelGraph created.");
	}

	private void MarkWallsFromJson()
	{
		if (!FileAccess.FileExists(LevelPath))
		{
			GD.PrintErr(
				$"Graph could not find {LevelPath}"
			);

			return;
		}

		string jsonText =
			FileAccess.GetFileAsString(
				LevelPath
			);

		Json json = new Json();

		Error error =
			json.Parse(jsonText);

		if (error != Error.Ok)
		{
			GD.PrintErr(
				json.GetErrorMessage()
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

			if (type != "wall")
				continue;

			float wallX =
				(float)objectData["x"];

			float wallY =
				(float)objectData["y"];

			float scaleX =
				objectData.ContainsKey("scaleX")
				? (float)objectData["scaleX"]
				: 1.0f;

			float scaleY =
				objectData.ContainsKey("scaleY")
				? (float)objectData["scaleY"]
				: 1.0f;

			float halfWidth =
				WallBaseWidth *
				scaleX /
				2.0f;

			float halfHeight =
				WallBaseHeight *
				scaleY /
				2.0f;

			MarkBlockedRectangle(
				new Vector2(wallX, wallY),
				halfWidth,
				halfHeight
			);
		}
	}

	private void MarkBlockedRectangle(
		Vector2 center,
		float halfWidth,
		float halfHeight
	)
	{
		for (int x = 0; x < Columns; x++)
		{
			for (int y = 0; y < Rows; y++)
			{
				Vector2 point =
					nodes[x, y]
					.WorldPosition;

				if (
					point.X >=
					center.X - halfWidth &&

					point.X <=
					center.X + halfWidth &&

					point.Y >=
					center.Y - halfHeight &&

					point.Y <=
					center.Y + halfHeight
				)
				{
					nodes[x, y].Walkable =
						false;
				}
			}
		}
	}

	public GraphNode GetNode(
		int x,
		int y
	)
	{
		if (
			x < 0 ||
			y < 0 ||
			x >= Columns ||
			y >= Rows
		)
		{
			return null;
		}

		return nodes[x, y];
	}

	public GraphNode GetNodeFromWorldPosition(
		Vector2 position
	)
	{
		int x =
			Mathf.FloorToInt(
				position.X / CellSize
			);

		int y =
			Mathf.FloorToInt(
				position.Y / CellSize
			);

		return GetNode(x, y);
	}

	public List<GraphNode> GetNeighbors(
		GraphNode node
	)
	{
		List<GraphNode> neighbors =
			new List<GraphNode>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				GraphNode neighbor =
					GetNode(
						node.X + x,
						node.Y + y
					);

				if (
					neighbor != null &&
					neighbor.Walkable
				)
				{
					neighbors.Add(neighbor);
				}
			}
		}

		return neighbors;
	}

	public void ResetSearchData()
	{
		for (int x = 0; x < Columns; x++)
		{
			for (int y = 0; y < Rows; y++)
			{
				nodes[x, y]
					.ResetSearchData();
			}
		}
	}
}
