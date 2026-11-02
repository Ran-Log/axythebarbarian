using Godot;
using System.Collections.Generic;

public partial class ZombieAIController : Node
{
	[Export]
	public float MoveSpeed = 55.0f;

	[Export]
	public float PathUpdateInterval = 0.5f;

	[Export]
	public float NodeReachDistance = 8.0f;

	public Vector2 Velocity { get; private set; }
		= Vector2.Zero;

	private CharacterBody2D zombie;
	private Node2D player;
	private LevelGraph graph;

	private List<GraphNode> currentPath =
		new List<GraphNode>();

	private int pathIndex = 0;

	private float pathTimer = 0.0f;

	public override void _Ready()
	{
		zombie =
			GetParent<CharacterBody2D>();

		player =
			GetTree()
			.GetFirstNodeInGroup("player")
			as Node2D;

		graph =
			GetTree()
			.Root
			.FindChild(
				"LevelGraph",
				true,
				false
			) as LevelGraph;
	}

	public void UpdateAI(double delta)
	{
		if (
			player == null ||
			graph == null
		)
		{
			Velocity = Vector2.Zero;
			return;
		}

		pathTimer -= (float)delta;

		if (pathTimer <= 0.0f)
		{
			CalculatePath();

			pathTimer =
				PathUpdateInterval;
		}

		FollowPath();
	}

	private void CalculatePath()
	{
		GraphNode start =
			graph.GetNodeFromWorldPosition(
				zombie.GlobalPosition
			);

		GraphNode goal =
			graph.GetNodeFromWorldPosition(
				player.GlobalPosition
			);

		if (
			start == null ||
			goal == null
		)
		{
			return;
		}

		currentPath =
			AStarPathfinder.FindPath(
				graph,
				start,
				goal
			);

		if (currentPath.Count > 1)
		{
			pathIndex = 1;
		}
		else
		{
			pathIndex = 0;
		}
	}

	private void FollowPath()
	{
		if (
			currentPath == null ||
			currentPath.Count == 0 ||
			pathIndex >= currentPath.Count
		)
		{
			Velocity = Vector2.Zero;
			return;
		}

		Vector2 target =
			currentPath[pathIndex]
			.WorldPosition;

		float distance =
			zombie.GlobalPosition
			.DistanceTo(target);

		if (
			distance <=
			NodeReachDistance
		)
		{
			pathIndex++;

			if (
				pathIndex >=
				currentPath.Count
			)
			{
				Velocity =
					Vector2.Zero;

				return;
			}

			target =
				currentPath[pathIndex]
				.WorldPosition;
		}

		Vector2 direction =
			target -
			zombie.GlobalPosition;

		if (direction != Vector2.Zero)
		{
			direction =
				direction.Normalized();
		}

		Velocity =
			direction *
			MoveSpeed;
	}
}
