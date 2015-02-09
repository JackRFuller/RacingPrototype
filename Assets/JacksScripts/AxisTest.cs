using UnityEngine;
using System.Collections;

public class AxisTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(Input.GetAxisRaw("Horizontal"));
	
	}
}
