using Godot;
using System.Collections.Generic;

public static class AStarPathfinder
{
	public static List<GraphNode> FindPath(
		LevelGraph graph,
		GraphNode start,
		GraphNode goal
	)
	{
		List<GraphNode> emptyPath =
			new List<GraphNode>();

		if (
			graph == null ||
			start == null ||
			goal == null
		)
		{
			return emptyPath;
		}

		graph.ResetSearchData();

		List<GraphNode> toVisit =
			new List<GraphNode>();

		HashSet<GraphNode> visited =
			new HashSet<GraphNode>();

		start.GValue = 0.0f;

		start.HValue =
			Heuristic(start, goal);

		toVisit.Add(start);

		while (toVisit.Count > 0)
		{
			GraphNode current =
				GetLowestFCost(toVisit);

			toVisit.Remove(current);

			if (current == goal)
			{
				return BuildPath(goal);
			}

			visited.Add(current);

			foreach (
				GraphNode neighbor
				in graph.GetNeighbors(current)
			)
			{
				if (visited.Contains(neighbor))
					continue;

				float newG =
					current.GValue +
					current.WorldPosition.DistanceTo(
						neighbor.WorldPosition
					);

				if (newG < neighbor.GValue)
				{
					neighbor.GValue = newG;

					neighbor.HValue =
						Heuristic(
							neighbor,
							goal
						);

					neighbor.CameFrom =
						current;

					if (!toVisit.Contains(neighbor))
					{
						toVisit.Add(neighbor);
					}
				}
			}
		}

		return emptyPath;
	}

	private static float Heuristic(
		GraphNode node,
		GraphNode goal
	)
	{
		return node.WorldPosition.DistanceTo(
			goal.WorldPosition
		);
	}

	private static GraphNode GetLowestFCost(
		List<GraphNode> nodes
	)
	{
		GraphNode best = nodes[0];

		foreach (GraphNode node in nodes)
		{
			if (node.F() < best.F())
			{
				best = node;
			}
		}

		return best;
	}

	private static List<GraphNode> BuildPath(
		GraphNode goal
	)
	{
		List<GraphNode> path =
			new List<GraphNode>();

		GraphNode current =
			goal;

		while (current != null)
		{
			path.Insert(0, current);

			current =
				current.CameFrom;
		}

		return path;
	}
}
