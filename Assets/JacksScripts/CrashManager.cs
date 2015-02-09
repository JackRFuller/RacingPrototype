using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrashManager : MonoBehaviour {

	public float fl_CrashTime;
	public float fl_OriginalTime;

	private CarController car; 
	public GameObject[] go_InputButtons;

	public GameObject go_CrashText;
	private ResetManager RM_Script;

	private LevelManager LM_Script;
	private PointsManager PM_Script;


	// Use this for initialization
	void Start () {

		if(Application.loadedLevelName != "PointsScene" && Application.loadedLevelName != "CoinPlacement")		
		{

			LM_Script = GameObject.Find("LevelManager").GetComponent<LevelManager>();
			
			fl_OriginalTime = fl_CrashTime;
			go_CrashText = GameObject.Find("CrashText");


			// get the car controller
			if(!LM_Script.bl_DecativeCrashing)
			{
				car = GameObject.Find("Car").GetComponent<CarController>();
				
			}
		}

		if(Application.loadedLevelName == "PointsScene")
		{
			PM_Script = GameObject.Find("LevelManager").GetComponent<PointsManager>();
		}

		RM_Script = GameObject.Find("ResetButton").GetComponent<ResetManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision cc_Hit)
	{
		if(cc_Hit.gameObject.tag == "Car" && fl_CrashTime > -0.1)
		{
			CountDown();
			Debug.Log("Hit");
		}
	}

	void CountDown()
	{
		fl_CrashTime -= Time.deltaTime;

		if(fl_CrashTime <= 0)
		{
			if(Application.loadedLevelName != "PointsScene")
			{
				go_CrashText.GetComponent<Text>().enabled = true;
				RM_Script.go_ResetText.guiText.enabled = true;
				
				if(Application.loadedLevelName == "TapControlScheme")
				{
					foreach(GameObject _go_Buttons in go_InputButtons)
					{
						_go_Buttons.SetActive(false);
						car.Move(0,0);
					}
					
				}
			}
			if(Application.loadedLevelName == "PointsScene")
			{
				go_CrashText.GetComponent<Text>().enabled = true;
				PM_Script.bl_RegisterPoints = false;
			}

		}
	}

	void OnCollisionExit(Collision cc_Hit)
	{
		if(cc_Hit.gameObject.tag == "Car")
		{
			fl_CrashTime = fl_OriginalTime;
		}
	}
}
