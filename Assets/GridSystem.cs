using UnityEngine;
using System.Collections;

public class GridSystem : MonoBehaviour {
	public GameObject plane;
	public int width = 10;
	public int length = 10;

	private GameObject[,] grid = new GameObject[10,10];

	void OnGUI()
	{
		if(GUI.Button (new Rect (10,10,150,100), "DEATH!"))
		{
			Destroy(grid [3, 3]);
		}
	}

	void Start () {
		for (int x=0; x < width; x++) 
		{
			for (int z=0; z < length; z++)
			{
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x, 
					gridPlane.transform.position.y, gridPlane.transform.position.z + z);
				grid[x,z] = gridPlane;
			}
		
		}
	}
	
	void Update () {
	
	}
}
