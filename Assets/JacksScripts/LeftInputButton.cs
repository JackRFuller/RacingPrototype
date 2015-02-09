using UnityEngine;
using System.Collections;

public class LeftInputButton : MonoBehaviour {

	private bool bl_Turning;
	public float fl_TurningSpeed;
	public float fl_TurningAngle;
	public float fl_FowardSpeed;

	private GameObject go_RightButton;
	
	private CarController car;
	private LevelManager LM_Script;

	public GameObject go_SpawnPos;
	private GameObject go_Car;

	// Use this for initialization
	void Start () {

		go_Car = GameObject.Find("Car");

		if(Application.loadedLevelName != "PointsScene")
		{
			go_Car.transform.position = go_SpawnPos.transform.position;
		}

		car = GameObject.Find("Car").GetComponent<CarController>();
		LM_Script = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		fl_FowardSpeed = 0.75F;

		go_RightButton = GameObject.Find("RightInput");

	}
	
	// Update is called once per frame
	void Update () {

		if(bl_Turning && gameObject.collider.enabled == true){

			if(fl_TurningSpeed > -1F)
			{
				fl_TurningSpeed -= 0.05F;
			}
		}

		car.Move(fl_TurningSpeed, fl_FowardSpeed);
	
	}

	void OnMouseDown()
	{
		bl_Turning = true;
		go_RightButton.collider.enabled = false;
		
	}
	
	void OnMouseUp()
	{
		go_RightButton.collider.enabled = true;
		bl_Turning = false;
		fl_TurningSpeed = 0;

		if(Application.loadedLevelName != "PointsScene")
		{
			if(LM_Script.bl_RaceStarted)
			{
				car.Move(0, fl_FowardSpeed);
			}
		}



		
	}
}
