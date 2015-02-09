using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LapManager : MonoBehaviour {

	private TestMovements TM_Script;
	private CarManager CM_Script;
	public GameObject[] Checkpoints;
	public int NumofCheckpoints = 0;
	public int MaxNumofCheckpoints;
	public bool[] CheckpointHit;


	//GUI
	public Text OverallTimer;
	public Text[] Laps;

	public float OverallTime;
	public float fl_LapTimer;
	public bool bl_StartTimer;

	public int in_LapNumber = -1;
	private Text BestTimeText;

	public Text CongratsText;
	public bool Success;

	// Use this for initialization
	void Start () {

		CM_Script = GameObject.Find ("CarManager").GetComponent<CarManager> ();
		BestTimeText = GameObject.Find("BestTime").GetComponent<Text>();
		TM_Script = GameObject.Find ("TestCar").GetComponent<TestMovements> ();

		if(Application.loadedLevelName == "TestTrack")
		{
			if(PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_AllStarSlog") == 0)
			{
				BestTimeText.text = "Best Time: --:--";
			}
			else
			{
				BestTimeText.text = "Best Time: " + PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_AllStarSlog").ToString("F2");
			}
		}

		if(Application.loadedLevelName == "BigTrack")
		{
			if(PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_Figureof8") == 0)
			{
				BestTimeText.text = "Best Time: --:--";
			}
			else
			{
				BestTimeText.text = "Best Time: " + PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_Figureof8").ToString("F2");
			}
		}



	}
	
	// Update is called once per frame
	void Update () {

		if(bl_StartTimer && !TM_Script.bl_Crashed)
		{
			if(in_LapNumber < 5)
			{
				LapTimer();
			}

		}
	
	}

	void LapTimer()
	{
		OverallTime += Time.deltaTime;
		fl_LapTimer += Time.deltaTime;
		OverallTimer.text = "Current Time: " + OverallTime.ToString("F2");
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Car")
		{
			bl_StartTimer = true;
			in_LapNumber += 1;

			if(in_LapNumber >= 1)
			{
				foreach(bool Check in CheckpointHit)
				{
					if(Check)
					{
						NumofCheckpoints += 1;
					}
				}
				if(NumofCheckpoints == MaxNumofCheckpoints)
				{
					Laps[in_LapNumber - 1].enabled = true;
					Laps[in_LapNumber - 1].text = "Lap " + in_LapNumber.ToString() + " Time:" + fl_LapTimer.ToString("F2");
					fl_LapTimer = 0;

					for(int i = 0; i < CheckpointHit.Length; i++)
					{
						CheckpointHit[i] = false;
					}

					NumofCheckpoints = 0;
				}

			}
			if(in_LapNumber ==  5)
			{
				if(Application.loadedLevelName == "TestTrack")
				{
					if(PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_AllStarSlog") == 0 || OverallTime < PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_AllStarSlog"))
					{
						PlayerPrefs.SetFloat(CM_Script.CarName + "BestTime_AllStarSlog", OverallTime);
						BestTimeText.text = "Best Time: " + OverallTime.ToString("F2");
					}
				}

				if(Application.loadedLevelName == "BigTrack")
				{
					if(PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_Figureof8") == 0 || OverallTime < PlayerPrefs.GetFloat(CM_Script.CarName + "BestTime_Figureof8"))
					{
						PlayerPrefs.SetFloat(CM_Script.CarName + "BestTime_Figureof8", OverallTime);
						BestTimeText.text = "Best Time: " + OverallTime.ToString("F2");
					}
				}
				CongratsText.enabled = true;
				Success = true;
			}

		}
	}
}
