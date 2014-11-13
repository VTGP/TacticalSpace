using System.Collections;
using System.Collections.Generic;
using System;

public class AStar 
{
	private const int PATH_COST = 10;
	private const int EMPTY_COST = PATH_COST / 2 + 1;
	//private const int BLOCK_COST = 100000;

	private HashSet<AStarNode> openList;
	private HashSet<AStarNode> closedList;

	private Vector3i goalPos;
	private Vector3i startPos;

	private AStarNode.NodeType[,,] grid;
	private AStarNode[,,] nodes;

	public AStarNode CalculatePath( AStarNode.NodeType[,,] grid, Vector3i start, Vector3i goal)
	{
		this.grid = grid;
		this.startPos = start;
		this.goalPos = goal;

		this.grid[this.startPos.x, this.startPos.y, this.startPos.z] = AStarNode.NodeType.START;
		this.grid[this.goalPos.x, this.goalPos.y, this.goalPos.z] = AStarNode.NodeType.GOAL;

		this.nodes = new AStarNode[grid.GetLength(0), grid.GetLength(1), grid.GetLength(2)];

		this.openList = new HashSet<AStarNode>();
		this.closedList = new HashSet<AStarNode>();

		this.CreateNodesAndCalculateHeuristicValues();

		AStarNode curNode = nodes[this.startPos.x, this.startPos.y, this.startPos.z];
		openList.Add(curNode);

		while (openList.Count > 0)
		{
			openList.Remove(curNode);
			closedList.Add(curNode);

			// Left
			if (curNode.X - 1 >= 0)
				if (ProcessAdjacentNode(nodes[curNode.X - 1, curNode.Y, curNode.Z], curNode))
					return nodes[curNode.X - 1, curNode.Y, curNode.Z];

			// Up
			if (curNode.Y - 1 >= 0)
				if (ProcessAdjacentNode(nodes[curNode.X, curNode.Y - 1, curNode.Z], curNode))
					return nodes[curNode.X, curNode.Y - 1, curNode.Z];

			// Right
			if (curNode.X + 1 < nodes.GetLength(0))
				if (ProcessAdjacentNode(nodes[curNode.X + 1, curNode.Y, curNode.Z], curNode))
					return nodes[curNode.X + 1, curNode.Y, curNode.Z];
			
			// Down
			if (curNode.Y + 1 < nodes.GetLength(1))
				if (ProcessAdjacentNode(nodes[curNode.X, curNode.Y + 1, curNode.Z], curNode))
					return nodes[curNode.X, curNode.Y + 1, curNode.Z];

			// Forward
			if (curNode.Z + 1 < nodes.GetLength(2))
				if (ProcessAdjacentNode(nodes[curNode.X, curNode.Y, curNode.Z + 1], curNode))
					return nodes[curNode.X, curNode.Y, curNode.Z + 1];
			
			// Backward
			if (curNode.Z - 1 >= 0)
				if (ProcessAdjacentNode(nodes[curNode.X, curNode.Y, curNode.Z - 1], curNode))
					return nodes[curNode.X, curNode.Y, curNode.Z - 1];


			AStarNode minTotalCostNode = null;
			foreach (AStarNode node in openList)
			{
				if (minTotalCostNode == null || node.TotalCost < minTotalCostNode.TotalCost)
				{
					minTotalCostNode = node;
				}
			}

			curNode = minTotalCostNode;
		}

		return null;
	}

	private void CreateNodesAndCalculateHeuristicValues()
	{
		for (int x = 0; x < grid.GetLength(0); x++)
		{
			for (int y = 0; y < grid.GetLength(1); y++)
			{
				for (int z = 0; z < grid.GetLength(2); z++)
				{
					nodes[x, y, z] = new AStarNode(grid[x, y, z], x, y, z, Math.Abs(x - this.goalPos.x) + 
					                            				  		   Math.Abs(y - this.goalPos.y) + 
					                            				  		   Math.Abs(z - this.goalPos.z));
				}
			}
		}
	}

	// Returns true if goal was found.
	private bool ProcessAdjacentNode(AStarNode node, AStarNode curNode)
	{
		if (curNode.Type == AStarNode.NodeType.EMPTY && node.Type == AStarNode.NodeType.EMPTY)
		{
			// Can't move on two empty blocks in sequence.
			return false;
		}

		if (node.Type == AStarNode.NodeType.GOAL)
		{
			node.parent = curNode;

			// Done!
			return true;
		}

		if (node.Type == AStarNode.NodeType.BLOCK || closedList.Contains(node))
			return false;

		int moveCost = PATH_COST;

		if (node.Type == AStarNode.NodeType.EMPTY)
			moveCost = EMPTY_COST;

		if (openList.Contains(node))
		{
			if (curNode.MovementCost + moveCost < node.MovementCost)
			{
				node.parent = curNode;
				node.MovementCost = curNode.MovementCost + moveCost;
			}
		}
		else
		{
			node.parent = curNode;
			node.MovementCost = curNode.MovementCost + moveCost;
			openList.Add(node);
		}

		return false;
	}
}







