using System.Collections;

public class AStarNode 
{
	public enum NodeType
	{
		PATH,
		EMPTY,
		BLOCK,
		START,
		GOAL
	}

	private NodeType type;
	private int heuristicCost = 0;
	private int movementCost = 0;
	private int totalCost = 0;

	private int x;
	private int y;
	private int z;

	public AStarNode parent = null;

	public AStarNode (NodeType type, int x, int y, int z, int heuristicCost)
	{
		this.type = type;
		this.x = x;
		this.y = y;
		this.z = z;
		this.heuristicCost = heuristicCost;
	}

	public NodeType Type
	{
		get { return this.type; }
	}

	public int HeuristicCost
	{
		get { return this.heuristicCost; }
	}

	public int MovementCost 
	{ 
		get {return this.movementCost; } 
		set {this.movementCost = value; this.totalCost = value + this.heuristicCost; }
	}

	public int TotalCost
	{
		get { return this.totalCost; }
	}

	public int X
	{
		get { return this.x; }
	}

	public int Y
	{
		get { return this.y; }
	}

	public int Z
	{
		get { return this.z; }
	}
}
