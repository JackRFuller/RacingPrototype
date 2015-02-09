using UnityEngine;
using System.Collections;

public class InputControls : MonoBehaviour {

	private bool bl_Turning;
	public float fl_TurningSpeed;
	public float fl_TurningAngle;
	public float fl_FowardSpeed;

	private CarController car;

	// Use this for initialization
	void Start () {

		car = GameObject.Find("Car").GetComponent<CarController>();
		fl_FowardSpeed = 1F;

	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(bl_Turning)
		{
			TurnCar();
		}	




	}

	void TurnCar()
	{
		if(gameObject.name == "RightInput")
		{
			if(fl_TurningSpeed < 1F)
			{
				fl_TurningSpeed -= 0.01F;
			}	

		}

		if(gameObject.name == "LeftInput")
		{
			if(fl_TurningSpeed > -1F)
			{
				fl_TurningSpeed += 0.01F;
			}	
		}

		car.Move(fl_TurningSpeed,fl_FowardSpeed);
	}

	void OnMouseDown()
	{
		bl_Turning = true;

	}

	void OnMouseUp()
	{
		bl_Turning = false;
		fl_TurningSpeed = 0;

	}
}
