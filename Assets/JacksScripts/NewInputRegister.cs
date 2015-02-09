using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewInputRegister : MonoBehaviour {

	public float in_Horizontal;
	public bool bl_Braking;
	public float fl_TapCooler;
	public int in_TapCount;

	private CarController car;

	public float InputSensitivity = 0.04F;


	// Use this for initialization
	void Start () {

		bl_Braking = false;

		if(Application.loadedLevelName == "CoinPlacement")
		{
			car = GameObject.Find("Car1").GetComponent<CarController>();
		}
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

#if UNITY_IOS

		SteerCar();

		if(Input.touchCount <= 0)
		{
			in_Horizontal = 0;
			bl_Braking = false;
		}

#endif

#if UNITY_EDITOR

		if(Input.GetMouseButtonDown(0))
		{
			SteerCarMouse();

		}
		if(Input.GetMouseButtonUp(0))
		{
			in_Horizontal = 0;
		}


#endif

		if(Application.loadedLevelName == "CoinPlacement")
		{
			if(car.bl_CarReady)
			{
				car.Move(in_Horizontal,1);
			}

		}
	}

	void SteerCarMouse()
	{
		Ray _ray = gameObject.camera.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		if(Physics.Raycast(_ray, out hit))
		{	
			if(hit.collider.tag == "Left" && in_Horizontal > -1)
			{
				in_Horizontal -= InputSensitivity;
			}
			
			if(hit.collider.tag == "Right" && in_Horizontal < 1)
			{
				in_Horizontal += InputSensitivity;
			}

			if(hit.collider.tag == "Brake")
			{
				bl_Braking = true;
			}
		}
	}
	
	void SteerCar()
	{


		if(Input.touchCount > 0)
		{			
			Ray _ray = gameObject.camera.ScreenPointToRay(Input.GetTouch(0).position);
							
			RaycastHit hit;
			
			if(Physics.Raycast(_ray, out hit))
			{	
				if(hit.collider.tag == "Left" && in_Horizontal > -1)
				{
					in_Horizontal -= InputSensitivity;
				}

				if(hit.collider.tag == "Right" && in_Horizontal < 1)
				{
					in_Horizontal += InputSensitivity;
				}
			}
		}
	}
	
}
