using Godot;

public class GraphNode
{
	public int X { get; }
	public int Y { get; }

	public Vector2 WorldPosition { get; }

	public bool Walkable { get; set; } = true;

	public float GValue { get; set; }
		= float.PositiveInfinity;

	public float HValue { get; set; } = 0.0f;

	public GraphNode CameFrom { get; set; }
		= null;

	public GraphNode(
		int x,
		int y,
		Vector2 worldPosition
	)
	{
		X = x;
		Y = y;

		WorldPosition = worldPosition;
	}

	public float F()
	{
		return GValue + HValue;
	}

	public void ResetSearchData()
	{
		GValue = float.PositiveInfinity;
		HValue = 0.0f;
		CameFrom = null;
	}
}
