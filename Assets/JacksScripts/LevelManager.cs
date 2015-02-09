using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameManager GM_Script;

	public GameObject[] go_TapInputs;
	public GameObject[] go_WheelInputs;
	public GameObject[] go_SwipeInputs;
	public bool bl_DecativeCrashing;

	public bool bl_LevelSetUp;

	public bool bl_TestLevel;

	public bool bl_RaceStarted;

	public GameObject go_RacingLights;

	public GameObject[] go_DragBack;



	// Use this for initialization
	void Start () {

		if(!bl_TestLevel)
		{
			GM_Script = GameObject.Find("GameManager").GetComponent<GameManager>();
			SetUpLevel();

		}

		bl_RaceStarted = false;


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetUpLevel()
	{
		if(GM_Script.bl_CarActivated)
		{
			ActivateCarControls();
		}
		if(GM_Script.bl_AccelerationActivated)
		{
			ActivateAccelerationControls();
		}
	}

	void ActivateAccelerationControls()
	{
		go_RacingLights.SetActive(true);

		gameObject.GetComponent<RaceStart>().enabled = true;

		if(GM_Script.st_Acceleration == "Drag Back")
		{
			GameObject.Find("Car").GetComponent<DragControl>().enabled = true;
		}

		bl_LevelSetUp = true;

	}

	void ActivateCarControls()
	{
		if(GM_Script.st_CarControl == "Tap")
		{
			foreach(GameObject tap in go_TapInputs)
			{
				tap.SetActive(true);
			}

			go_TapInputs[4].GetComponent<RaceManager>().enabled = true;


		}

		if(GM_Script.st_CarControl == "Steering Wheel")
		{
			go_WheelInputs[0].GetComponent<SteeringWheel>().enabled = true;
		}

		if(GM_Script.st_CarControl == "Swipe")
		{

			foreach(GameObject Swipe in go_SwipeInputs)
			{
				Swipe.SetActive(true);
			}
			go_SwipeInputs[0].GetComponent<MouseSwipeControls>().enabled = true;
			//go_SwipeInputs[4].GetComponent<SplineWalker>().enabled = true;
			go_SwipeInputs[4].GetComponent<CarController>().enabled = false;
			go_SwipeInputs[4].GetComponent<CarAudio>().enabled = false;
			go_SwipeInputs[4].GetComponent<CarSelfRighting>().enabled = false;
			go_SwipeInputs[4].GetComponent<ObjectResetter>().enabled = false;
			go_SwipeInputs[4].GetComponent<PlatformSpecificContent>().enabled = false;

			bl_DecativeCrashing = true;
		}

		bl_LevelSetUp = true;
	}
}
