using UnityEngine;
using System.Collections;

public class RaceTimer : MonoBehaviour {

	public bool bl_RaceBegins;
	public int in_NumofLaps;
	public float fl_CurrentLapTime;
	public float[] LapTimes;
	public int in_CurrentLap;

	public float fl_LapTime;
	public GameObject go_TotalLapTime;

	//GUI
	public GameObject[] go_LapTimes;

	private GameObject go_Car;
	private bool bl_ActivateStartTrack;
	public int in_LapCounter;


	// Use this for initialization
	void Start () {

		in_NumofLaps = -1;
		in_CurrentLap = 0;
		fl_LapTime = 0;
		in_LapCounter = 0;

		go_Car = GameObject.Find("Car");
	
	}
	
	// Update is called once per frame
	void Update () {

		if(bl_RaceBegins && in_NumofLaps < 5)
		{
			RunLapTimer();
		}

		if(Application.loadedLevelName == "SwipeControls" && in_CurrentLap <= 4)
		{
			DetectCar();

			if(in_LapCounter >= 1)
			{
				RunLapTimer();
			}

		}	
	}

	IEnumerator WaitTime(float _Time)
	{
		yield return new WaitForSeconds(_Time);
		in_LapCounter = 2;
	}

	void DetectCar()
	{
		float _fl_Distance = Vector3.Distance(go_Car.transform.position,transform.position);

		if(_fl_Distance < 1.5 && in_LapCounter == 0)
		{
			in_LapCounter += 1;	
			StartCoroutine(WaitTime(2.0F));
		}

		if(_fl_Distance < 1.5 && in_LapCounter == 2)
		{
			if(bl_ActivateStartTrack)
			{
				RecordLap();
				bl_ActivateStartTrack = false;
			}
		}

		if(_fl_Distance > 2)
		{
			bl_ActivateStartTrack = true;
		}


	}

	void RunLapTimer()
	{
		fl_CurrentLapTime += Time.deltaTime;
		fl_LapTime += Time.deltaTime;

		go_TotalLapTime.guiText.text = "Race Time: " + fl_LapTime.ToString("F2");

	}

	void RecordLap()
	{
		LapTimes[in_CurrentLap] = fl_CurrentLapTime;
		go_LapTimes[in_CurrentLap].guiText.enabled = true;
		go_LapTimes[in_CurrentLap].guiText.text = go_LapTimes[in_CurrentLap].guiText.text + fl_CurrentLapTime.ToString("F2");
		fl_CurrentLapTime = 0;
		in_CurrentLap += 1;		
	}

	void OnTriggerEnter(Collider cc_Hit)
	{
		if(cc_Hit.gameObject.tag == "Car"){

			bl_RaceBegins = true;
			in_NumofLaps += 1;

			Debug.Log(in_NumofLaps);
			if(in_NumofLaps >= 1 && in_CurrentLap <= 5)
			{
				RecordLap();
			}


		}

	}
}
