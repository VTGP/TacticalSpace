using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour 
{
	public int lengthX;
	public int lengthY;
	public int lengthZ;

	private GameObject highlightPlane;
	private AStarNode path;
	private AStarNode.NodeType[,,] grid;

	// Use this for initialization
	void Start () 
	{
		highlightPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		highlightPlane.transform.localScale = new Vector3 (0.1f, 1.0f, 0.1f);
		highlightPlane.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		highlightPlane.renderer.material.color = new Color (1.0f, 0.15f, 0.15f, 0.0f);

		// Grab the level layout from the scene.
		grid = new AStarNode.NodeType[lengthX + 2, lengthY + 2, lengthZ + 2];

		for (int x = 0; x < grid.GetLength(0); x++)
		{
			for (int y = 0; y < grid.GetLength(1); y++)
			{
				for (int z = 0; z < grid.GetLength(2); z++)
				{
					if (x == 0 && y == 0 ||
					    x == 0 && z == 0 ||
					    x == 0 && y == grid.GetLength(1) - 1 ||
					    x == 0 && z == grid.GetLength(2) - 1 ||
					    y == 0 && z == 0 ||
					    y == 0 && x == grid.GetLength(0) - 1 ||
					    y == 0 && z == grid.GetLength(2) - 1 ||
					    z == 0 && y == grid.GetLength(1) - 1 ||
					    z == 0 && x == grid.GetLength(0) - 1 ||
					    y == grid.GetLength(1) - 1 && x == grid.GetLength(0) - 1 ||
					    y == grid.GetLength(1) - 1 && z == grid.GetLength(2) - 1 ||
					    z == grid.GetLength(2) - 1 && x == grid.GetLength(0) - 1)
					{
						grid[x, y, z] = AStarNode.NodeType.EMPTY;
					}
					else
					{
						grid[x, y, z] = AStarNode.NodeType.PATH;
					}
				}
			}
		}

		GameObject start = GameObject.FindWithTag("Start");
		Vector3i startPos = new Vector3i(start.transform.position) +  new Vector3i(1, 1, 1);

		GameObject goal = GameObject.FindWithTag("Goal");
		Vector3i goalPos = new Vector3i(goal.transform.position) + new Vector3i(1, 1, 1);

		// Run the A* algorithm.
		AStar aStar = new AStar();
		path = aStar.CalculatePath(grid, startPos, goalPos);

		// Unwind the correct path.
		while (path != null)
		{
			if (path.Type != AStarNode.NodeType.EMPTY)
			{
				GameObject highlight = CreateHighlightPlane(new Vector3(path.X - 1, path.Y - 1, path.Z - 1),
				                                            new Vector3 (0.1f, 1.0f, 0.1f),
				                                            0.5f);
				Debug.Log("(" + (path.X - 1) + ", " + (path.Y - 1) + ", " + (path.Z - 1) + ")");
			}
			
			path = path.parent;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	private GameObject CreateHighlightPlane(Vector3 position, Vector3 localScale, float alpha)
	{
		Vector3 rotation = new Vector3(transform.localEulerAngles.x,
		                               transform.localEulerAngles.y,
		                               transform.localEulerAngles.z);

		if (position.y == lengthY)
		{
			position.y -= 0.45f;
		}
		else if (position.y == -1)
		{
			position.y += 0.45f;
		}

		if (position.x == lengthX)
		{
			position.x -= 0.45f;
			rotation.z -= 90.0f;
		}
		else if (position.x == -1)
		{
			position.x += 0.45f;
			rotation.z += 90.0f;
		}

		if (position.z == lengthZ)
		{
			position.z -= 0.45f;
			rotation.x += 90.0f;
		}
		else if (position.z == -1)
		{
			position.z += 0.45f;
			rotation.x -= 90.0f;
		}

		GameObject newPlane = (GameObject)Instantiate(highlightPlane);
		newPlane.transform.position = position;
		newPlane.transform.localEulerAngles = rotation;
		newPlane.transform.localScale = localScale;
		Color color = newPlane.renderer.material.color;
		color.a = alpha;
		newPlane.renderer.material.color = color;


		
		return newPlane;
	}
}
