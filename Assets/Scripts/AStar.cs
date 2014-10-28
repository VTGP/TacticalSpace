using System.Collections;
using System.Collections.Generic;
using System;

public class AStar 
{
	private const int MOVE_COST = 10;

	private HashSet<AStarNode> openList;
	private HashSet<AStarNode> closedList;
	
	private int goalX;
	private int goalY;
	private int startX;
	private int startY;

	private AStarNode.NodeType[,] grid;
	private AStarNode[,] nodes;

	public AStarNode CalculatePath( AStarNode.NodeType[,] grid, 
	                           int startX, int startY, 
	                           int goalX, int goalY )
	{
		this.grid = grid;
		this.startX = startX;
		this.startY = startY;
		this.goalX = goalX;
		this.goalY = goalY;

		this.grid[this.startX, this.startY] = AStarNode.NodeType.START;
		this.grid[this.goalX, this.goalY] = AStarNode.NodeType.GOAL;

		this.nodes = new AStarNode[grid.GetLength(0), grid.GetLength(1)];

		this.openList = new HashSet<AStarNode>();
		this.closedList = new HashSet<AStarNode>();

		this.CreateNodesAndCalculateHeuristicValues();

		AStarNode curNode = nodes[startX, startY];
		openList.Add(curNode);

		while (openList.Count > 0)
		{
			openList.Remove(curNode);
			closedList.Add(curNode);

			// Left
			if (curNode.X - 1 >= 0)
				if (ProcessAdjacentNode(nodes[curNode.X - 1, curNode.Y], curNode))
					return nodes[curNode.X - 1, curNode.Y];

			// Up
			if (curNode.Y - 1 >= 0)
				if (ProcessAdjacentNode(nodes[curNode.X, curNode.Y - 1], curNode))
					return nodes[curNode.X, curNode.Y - 1];

			// Right
			if (curNode.X + 1 < nodes.GetLength(0))
				if (ProcessAdjacentNode(nodes[curNode.X + 1, curNode.Y], curNode))
					return nodes[curNode.X + 1, curNode.Y];

			// Down
			if (curNode.Y + 1 < nodes.GetLength(1))
				if (ProcessAdjacentNode(nodes[curNode.X, curNode.Y + 1], curNode))
					return nodes[curNode.X, curNode.Y + 1];

			// Left
			/*openList.Add(nodes[curPosX - 1, curPosY]);
			nodes[curPosX - 1, curPosY].parent = curNode;
			nodes[curPosX - 1, curPosY].movementCost = curNode.movementCost + MOVE_COST;

			// Up
			openList.Add(nodes[curPosX, curPosY - 1]);
			nodes[curPosX, curPosY - 1].parent = curNode;
			nodes[curPosX, curPosY - 1].movementCost = curNode.movementCost + MOVE_COST;

			// Right
			openList.Add(nodes[curPosX + 1, curPosY]);
			nodes[curPosX + 1, curPosY].parent = curNode;
			nodes[curPosX + 1, curPosY].movementCost = curNode.movementCost + MOVE_COST;

			// Down
			openList.Add(nodes[curPosX, curPosY + 1]);
			nodes[curPosX, curPosY + 1].parent = curNode;
			nodes[curPosX, curPosY + 1].movementCost = curNode.movementCost + MOVE_COST;*/


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
				nodes[x, y] = new AStarNode(grid[x, y], x, y, Math.Abs(x - this.goalX) + Math.Abs(y - this.goalY));
			}
		}
	}

	// Returns true if goal was found.
	private bool ProcessAdjacentNode(AStarNode node, AStarNode curNode)
	{
		if (node.Type == AStarNode.NodeType.GOAL)
		{
			node.parent = curNode;

			// Done!
			return true;
		}

		if (node.Type == AStarNode.NodeType.BLOCK || closedList.Contains(node))
			return false;

		if (openList.Contains(node))
		{
			if (curNode.MovementCost + MOVE_COST < node.MovementCost)
			{
				node.parent = curNode;
				node.MovementCost = curNode.MovementCost + MOVE_COST;
			}
		}
		else
		{
			node.parent = curNode;
			node.MovementCost = curNode.MovementCost + MOVE_COST;
			openList.Add(node);
		}

		return false;
	}
}







