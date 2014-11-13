using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class AStarTest : MonoBehaviour {

	private const string TEST_FILES_PATH = "Assets/Scripts/TestFiles/AStar/3D/";

	private AStarNode.NodeType[,,] grid = null;
	private Vector3i startPos;
	private Vector3i goalPos;

	private int targetStepCount;

	private string outputStr = "";

	// Use this for initialization
	void Start () 
	{
		string[] testFiles = Directory.GetFiles(TEST_FILES_PATH, "*.txt");

		outputStr += "Number of tests: " + testFiles.Length;
		for (int i = 0; i < testFiles.Length; i++)
		{
			outputStr += "\n\nRunning test: " + testFiles[i];

			ParseFile(testFiles[i]);
			
			AStar aStar = new AStar();
			AStarNode node = aStar.CalculatePath(grid, startPos, goalPos);

			int steps = -1; //First node doesn't count as a step.
			string path = "";

			if (node == null)
			{
				path += "Not found!";
				steps = -1;
			}
			else
			{
				// Unwind the correct path.
				while (node != null)
				{
					if (node.Type != AStarNode.NodeType.EMPTY)
					{
						path = path.Insert(0, "(" + node.X + ", " + node.Y + ", " + node.Z + ") ");
						steps++;
					}
					else
					{
						path = path.Insert(0, "<" + node.X + ", " + node.Y + ", " + node.Z + "> ");
					}

					node = node.parent;
				}
			}

			outputStr += "\n\nPath found: " + path;
			outputStr += "\nStep count: " + steps + "; supposed to be " + targetStepCount + 
				" - " + (steps == targetStepCount ? "CORRECT" : "INCORRECT");

			if (steps != targetStepCount)
			{
				Debug.LogError(testFiles[i] + " test FAILED!");
			}
		}

		outputStr += "\n\nTest finished.";
		Debug.Log(outputStr);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void ParseFile (string filePath)
	{
		grid = null;

		try
		{
			using (StreamReader sr = new StreamReader(filePath))
			{
				string line;

				int y = 0;
				int z = 0;

				int lengthX = 0;
				int lengthY = 0;
				int lengthZ = 0;
				while((line = sr.ReadLine()) != null)
				{
					if (grid == null && line.StartsWith("INPUT"))
					{
						int.TryParse( Regex.Match(line,  @"INPUT\s*(\d*)x(\d*)x(\d*)").Groups[1].Value,
							out lengthX);

						int.TryParse(Regex.Match(line, @"INPUT\s*(\d*)x(\d*)x(\d*)").Groups[2].Value,
							out lengthY);

						int.TryParse(Regex.Match(line, @"INPUT\s*(\d*)x(\d*)x(\d*)").Groups[3].Value,
						    out lengthZ);

						grid = new AStarNode.NodeType[lengthX, lengthY, lengthZ];
					}
					else if (grid != null && z < lengthZ)
					{
						if (line.Trim().Equals(""))
						{
							y = 0;
							z++;
							continue;
						}

						String[] nodesRow = line.Split(new string[] { " " }, StringSplitOptions.None);

						for (int x = 0; x < lengthX; x++)
						{
							switch (nodesRow[x][0])
							{
							case '0':
								grid[x, y, z] = AStarNode.NodeType.PATH;
								break;

							case 'B':
							case 'b':
							case 'x':
							case 'X':
								grid[x, y, z] = AStarNode.NodeType.BLOCK;
								break;

							case '-':
								grid[x, y, z] = AStarNode.NodeType.EMPTY;
								break;

							case 'S':
							case 's':
								startPos = new Vector3i(x, y, z);
								grid[x, y, z] = AStarNode.NodeType.START;
								break;

							case 'G':
							case 'g':
								goalPos = new Vector3i(x, y, z);
								grid[x, y, z] = AStarNode.NodeType.GOAL;
								break;

							default:
								Debug.LogError("Node token not recognized: " + nodesRow[x][0]);
								break;
							}
						}

						y++;
					}
					else if (line.StartsWith("OUTPUT"))
					{
						int.TryParse( Regex.Match(line,  @"OUTPUT\s*(-?\d*)\s").Groups[1].Value,
						             out targetStepCount);
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
