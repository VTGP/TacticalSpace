using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class AStarTest : MonoBehaviour {

	private const string TEST_FILES_PATH = "Assets/Scripts/TestFiles/AStar";

	private AStarNode.NodeType[,] grid = null;
	private int startX;
	private int startY;
	private int goalX;
	private int goalY;

	// Use this for initialization
	void Start () 
	{
		ParseFile (TEST_FILES_PATH + "/small_no_obstacles.txt");

		AStar aStar = new AStar();
		AStarNode node = aStar.CalculatePath(grid, startX, startY, goalX, goalY);

		// Unwind the correct path.
		while (node != null)
		{
			Debug.Log ("(" + node.X + ", " + node.Y + ")");
			node = node.parent;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void ParseFile (string filePath)
	{
		try
		{
			using (StreamReader sr = new StreamReader(filePath))
			{
				string line;

				int row = 0;
				int numCols = 0;
				int numRows = 0;
				while((line = sr.ReadLine()) != null && !line.Equals(""))
				{
					if (grid == null && line.StartsWith("INPUT "))
					{
						int.TryParse( Regex.Match(line,  @"INPUT\s*(\d*)x(\d*)$").Groups[1].Value,
							out numCols);

						int.TryParse(Regex.Match(line, @"^INPUT\s*(\d*)x(\d*)$").Groups[2].Value,
							out numRows);

						//Debug.Log("cols = " + numCols + ", rows = " + numRows);
						grid = new AStarNode.NodeType[numCols, numRows];
					}
					else if (grid != null && row < numRows)
					{
						String[] nodesRow = line.Split(new string[] { " " }, StringSplitOptions.None);

						for (int col = 0; col < numCols; col++)
						{
							switch (nodesRow[col][0])
							{
							case '0':
								grid[col, row] = AStarNode.NodeType.PATH;
								break;

							case 'B':
							case 'b':
								grid[col, row] = AStarNode.NodeType.BLOCK;
								break;

							case 'S':
							case 's':
								startX = col;
								startY = row;
								grid[col, row] = AStarNode.NodeType.START;
								break;

							case 'G':
							case 'g':
								goalX = col;
								goalY = row;
								grid[col, row] = AStarNode.NodeType.GOAL;
								break;

							default:
								break;
							}
						}

						row++;
					}
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
		}
	}
}
