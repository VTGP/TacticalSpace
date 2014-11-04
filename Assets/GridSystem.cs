using UnityEngine;
using System.Collections;

public class GridSystem : MonoBehaviour {
	public GameObject plane;
	public const int COLS = 10;
	public const int ROWS = 10;
	public const int HEIGHT = 10;

	private GameObject[,] grid = new GameObject[COLS, ROWS];

	void OnGUI()
	{
		if(GUI.Button (new Rect (10,10,150,100), "DEATH!"))
		{
			Destroy(grid [3, 3]);
		}
	}

	void Start () {
		for (int x=0; x < COLS; x++) 
		{
			for (int z=0; z < ROWS; z++)
			{
				for (int y=0; y < HEIGHT; y++)
				{
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x, 
					gridPlane.transform.position.y + y, gridPlane.transform.position.z + z);
				grid[x,z] = gridPlane;
				}
			}
		
		}
	}
	
	void Update () {
	
	}
}
