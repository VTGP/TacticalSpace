using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour 
{
	public int lengthX;
	public int lengthY;
	public int lengthZ;

	//private GameObject highlightPlane;
	private GameObject highlightCube;
	private AStarNode path;
	private AStarNode.NodeType[,,] grid;

	// Use this for initialization
	void Start () 
	{
		/*highlightPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		highlightPlane.transform.localScale = new Vector3 (0.1f, 1.0f, 0.1f);
		highlightPlane.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		highlightPlane.renderer.material.color = new Color (1.0f, 0.15f, 0.15f, 0.0f);*/

		highlightCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		highlightCube.transform.localScale = new Vector3 (1f, 1f, 1f);
		highlightCube.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		highlightCube.renderer.material.color = new Color (1.0f, 0.15f, 0.15f, 0.0f);

		grid = new AStarNode.NodeType[lengthX, lengthY, lengthZ];

		// Grab the level layout from the scene.
		GameObject[] cubes = GameObject.FindGameObjectsWithTag("Ground");

		foreach (GameObject cube in cubes)
		{
			Vector3i pos = new Vector3i(cube.transform.position);

			// Block itself
			grid[pos.x, pos.y, pos.z] = AStarNode.NodeType.BLOCK;

			// Left
			if (grid[pos.x - 1, pos.y, pos.z] != AStarNode.NodeType.BLOCK)
				grid[pos.x - 1, pos.y, pos.z] = AStarNode.NodeType.PATH;

			// Top
			if (grid[pos.x, pos.y - 1, pos.z] != AStarNode.NodeType.BLOCK)
				grid[pos.x, pos.y - 1, pos.z] = AStarNode.NodeType.PATH;

			// Right
			if (grid[pos.x + 1, pos.y, pos.z] != AStarNode.NodeType.BLOCK)
				grid[pos.x + 1, pos.y, pos.z] = AStarNode.NodeType.PATH;

			// Bottom
			if (grid[pos.x, pos.y + 1, pos.z] != AStarNode.NodeType.BLOCK)
				grid[pos.x, pos.y + 1, pos.z] = AStarNode.NodeType.PATH;

			// Forward
			if (grid[pos.x, pos.y, pos.z + 1] != AStarNode.NodeType.BLOCK)
				grid[pos.x, pos.y, pos.z + 1] = AStarNode.NodeType.PATH;

			// Backward
			if (grid[pos.x, pos.y, pos.z - 1] != AStarNode.NodeType.BLOCK)
				grid[pos.x, pos.y, pos.z - 1] = AStarNode.NodeType.PATH;
		}

		GameObject start = GameObject.FindWithTag("Start");
		Vector3i startPos = new Vector3i(start.transform.position);

		GameObject goal = GameObject.FindWithTag("Goal");
		Vector3i goalPos = new Vector3i(goal.transform.position);

		// Run the A* algorithm.
		AStar aStar = new AStar();
		path = aStar.CalculatePath(grid, startPos, goalPos);

		// Unwind the correct path.
		while (path != null)
		{
			if (path.Type != AStarNode.NodeType.EMPTY)
			{
				CreateHighlightCube(new Vector3(path.X, path.Y, path.Z),
				                    new Vector3 (1f, 1f, 1f),
				                    0.5f);

				//Debug.Log("(" + path.X + ", " + path.Y + ", " + path.Z + ")");
			}
			
			path = path.parent;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	private GameObject CreateHighlightCube(Vector3 pos, Vector3 localScale, float alpha)
	{
		GameObject newCube = (GameObject)Instantiate(highlightCube);
		newCube.transform.position = pos;
		//newCube.transform.localEulerAngles = rotation;
		newCube.transform.localScale = localScale;
		Color color = newCube.renderer.material.color;
		color.a = alpha;
		newCube.renderer.material.color = color;		
		
		return newCube;
	}

	/*private GameObject CreateHighlightPlane(Vector3 pos, Vector3 localScale, float alpha)
	{
		Vector3 rotation = new Vector3(transform.localEulerAngles.x,
		                               transform.localEulerAngles.y,
		                               transform.localEulerAngles.z);

		// Look for an adjacent block to set orientation.
		// TODO: There can be more than one adjacent block. How do we handle orientation with A*?

		// Left
		if (pos.x - 1 >= 0 &&
			grid[pos.x - 1, pos.y, pos.z] != AStarNode.NodeType.BLOCK)
			grid[pos.x - 1, pos.y, pos.z] = AStarNode.NodeType.PATH;
		
		// Top
		if (pos.y - 1 >= 0 &&
			grid[pos.x, pos.y - 1, pos.z] != AStarNode.NodeType.BLOCK)
			grid[pos.x, pos.y - 1, pos.z] = AStarNode.NodeType.PATH;
		
		// Right
		if (pos.x + 1 < lengthX &&
			grid[pos.x + 1, pos.y, pos.z] != AStarNode.NodeType.BLOCK)
			grid[pos.x + 1, pos.y, pos.z] = AStarNode.NodeType.PATH;
		
		// Bottom
		if (pos.y + 1 < lengthY &&
			grid[pos.x, pos.y + 1, pos.z] != AStarNode.NodeType.BLOCK)
			grid[pos.x, pos.y + 1, pos.z] = AStarNode.NodeType.PATH;
		
		// Forward
		if (pos.z + 1 < lengthZ &&
		    grid[pos.x, pos.y, pos.z + 1] != AStarNode.NodeType.BLOCK)
			grid[pos.x, pos.y, pos.z + 1] = AStarNode.NodeType.PATH;
		
		// Backward
		if (pos.z - 1 >= 0 &&
			grid[pos.x, pos.y, pos.z - 1] != AStarNode.NodeType.BLOCK)
			grid[pos.x, pos.y, pos.z - 1] = AStarNode.NodeType.PATH;

		/*if (position.y == lengthY)
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
	}*/
}
