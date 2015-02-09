using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public float width = 32.0F;
	public float height = 32.0F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		Vector3 pos = Camera.current.transform.position;

		for(float y = pos.y - 800.0F; y < pos.y + 800.0F; y+= height)
		{
			Gizmos.DrawLine(new Vector3(-1000000.0F, Mathf.Floor(y/height) * height,0.0F),
			new Vector3(1000000.0F, Mathf.Floor(y/height) * height,0.0F));     

		}

		for(float x = pos.x - 1200.0F; x < pos.x + 1200.0F; x+= height)
		{
			Gizmos.DrawLine(new Vector3(Mathf.Floor(x/height) * width,-1000000.0F, 0.0F),
			new Vector3(Mathf.Floor(x/height) * width,1000000.0F,0.0F));     
			
		}
	}
}
