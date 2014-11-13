using UnityEngine;
using UnityEditor;

using System.Collections;

[ExecuteInEditMode]
public class Snap : MonoBehaviour {
	
	#if UNITY_EDITOR
	public float cell_size = 1.0f; // For equal widht, height, and depth
	private float x, y, z;
	
	private Vector3 currRotation;
	private float deltaRotationY;
	
	void Start() 
	{
		x = 0.0f;
		y = 0.0f;
		z = 0.0f;		
		
		currRotation = new Vector3(0.0f, Mathf.Round(transform.localEulerAngles.y / 90.0f) * 90.0f, 0.0f);
		while (currRotation.y < 0.0f )
			currRotation.y += 360.0f;
		
		while (currRotation.y >= 360.0f)
			currRotation.y -= 360.0f;
		
		transform.localEulerAngles = currRotation;
	}
	
	void Update() 
	{
		// Don't want this in play mode.
		if (EditorApplication.isPlaying ) 
			return;
		
		x = Mathf.Round(transform.position.x / cell_size) * cell_size;
		y = Mathf.Round(transform.position.y / cell_size) * cell_size;
		z = Mathf.Round(transform.position.z / cell_size) * cell_size;
		transform.position = new Vector3(x, y, z);
		
		if (transform.localEulerAngles.y != currRotation.y)
		{
			deltaRotationY += transform.localEulerAngles.y - currRotation.y;
			
			if (Mathf.Abs(deltaRotationY) >= 45.0f)
			{
				currRotation.y = Mathf.Round((currRotation.y + deltaRotationY) / 90.0f) * 90.0f;
				
				while (currRotation.y < 0.0f )
					currRotation.y += 360.0f;
				
				while (currRotation.y >= 360.0f)
					currRotation.y -= 360.0f;
				
				deltaRotationY = 0.0f;
			}
			transform.localEulerAngles = currRotation;
		}
	}
	# endif
}