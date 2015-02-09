using UnityEngine;
using System.Collections;

public class PhysicMovement : MonoBehaviour {

	public float Speed;
	public float TurningSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

//		rigidbody.AddRelativeForce (0, 0, Input.GetAxis ("Vertical") * Speed);
//		rigidbody.AddRelativeTorque (0,Input.GetAxis ("Horizontal") * TurningSpeed,0);

		transform.Translate(new Vector3(0,0,(Speed * Time.deltaTime)));
		transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal") * (TurningSpeed * Time.deltaTime),0));
	
	}
}
