using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CarManager : MonoBehaviour {

	[System.Serializable]
	public class CarVariables
	{
		public float Speed;
		public float TurningSpeed;
		public float InputSensitivity;
		public float Mass;
		public float AngularDrag;
		public float BestTime_AllStarSlog;
		public float BestTime_Figureof8;
	}

	public Text[] CarStats;

	public CarVariables Car1;
	public CarVariables Car2;
	public CarVariables Car3;
	public CarVariables Car4;
	public CarVariables Car5;

	public string CarName;

	// Use this for initialization
	void Start () {	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetValues(string PulledData)
	{
		CarStats[0].text = "Speed: " + PlayerPrefs.GetFloat (PulledData + "Speed");
		CarStats[1].text = "Turning Speed: " + PlayerPrefs.GetFloat (PulledData + "TurningSpeed");
		CarStats[2].text = "Input Sensitivity: " + PlayerPrefs.GetFloat (PulledData + "Input").ToString("F3");
		CarStats[3].text = "Mass: " + PlayerPrefs.GetFloat (PulledData + "Mass").ToString("F1");
		CarStats[4].text = "Angular Drag: " + PlayerPrefs.GetFloat (PulledData + "AngularDrag").ToString("F2");



		foreach(Text Item in CarStats)
		{
			Item.enabled = true;
		}

		CarName = PulledData;
	}
}
