using UnityEngine;
using System.Collections;

public class NewCarMovement : MonoBehaviour {

	public float fl_Speed = 10.0F;
	public float fl_TurnSpeed = 0.6F;

	public float fl_Accerleration;

	private float fl_MoveDirection = 0.0F;
	private float fl_turnDirections = 0.0F;

	private NewInputRegister NIR_Script;

	// Use this for initialization
	void Start () {

		NIR_Script = GameObject.Find("Camera").GetComponent<NewInputRegister>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if(fl_Accerleration < 1)
		{
			fl_Accerleration += 0.04F;
		}
		//Drag & Angular Drag Variables
		float currentSpeed = Mathf.Abs(transform.InverseTransformDirection(rigidbody.velocity).z);

		float maxAngularDrag = 2.5F;
		float currentAngularDrag = 1.0F;
		float aDragLerpTime = currentSpeed * 0.1F;

		float maxDrag = 1.0F;
		float currentDrag = 2.5F;
		float dragLerpTime = currentSpeed * 0.1F;

		float myAngularDrag = Mathf.Lerp(currentAngularDrag, maxAngularDrag, aDragLerpTime);
		float myDrag = Mathf.Lerp(currentDrag, maxDrag, dragLerpTime);


		//Acceeration Movement
		if(fl_Accerleration > 0.0F)
		{
			fl_MoveDirection = fl_Accerleration * fl_Speed;
			rigidbody.AddRelativeForce(fl_MoveDirection,0,0);


				fl_turnDirections = NIR_Script.in_Horizontal * fl_TurnSpeed;
				rigidbody.AddRelativeTorque(0, fl_turnDirections,0);


		}


		//Calculate Drag & Angular Drag
		rigidbody.angularDrag = myAngularDrag;
		rigidbody.drag = myDrag;
	}
}
