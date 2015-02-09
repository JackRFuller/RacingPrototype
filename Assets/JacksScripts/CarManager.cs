﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CarManager : MonoBehaviour {

	private GarageManager GM_Script;

	[System.Serializable]
	public class CarVariables
	{
		public float Speed;
		public float TurningSpeed;
		public float InputSensitivity;
		public float Mass;
		public float AngularDrag;
		public string CarName;
		public Color CarColor;
		public float BestTime_AllStarSlog;
		public float BestTime_Figureof8;
	}

	public CarVariables[] Cars;

	public Text[] CarStats;

	public string CarName;
	public int SelectedCar;

//	public CarVariables Car1;
//	public CarVariables Car2;
//	public CarVariables Car3;
//	public CarVariables Car4;
//	public CarVariables Car5;



	// Use this for initialization
	void Start () {	

		GM_Script = GameObject.Find ("MenuManager").GetComponent<GarageManager> ();

		PlayerPrefs.SetFloat (Cars [0].CarName + "Speed", 100.0F);
		PlayerPrefs.SetFloat(Cars[0].CarName + "TurningSpeed", 50.0F);
		PlayerPrefs.SetFloat (Cars [0].CarName + "Input", 1.00F);
		PlayerPrefs.SetFloat (Cars [0].CarName + "Mass", 1.50F);
		PlayerPrefs.SetFloat (Cars [0].CarName + "AngularDrag", 3.00F);

		for(int i = 0; i < Cars.Length; i++)
		{
			Cars [i].Speed = PlayerPrefs.GetFloat (Cars [i].CarName + "Speed");
			Cars [i].TurningSpeed = PlayerPrefs.GetFloat (Cars [i].CarName + "TurningSpeed");
			Cars[i].InputSensitivity = PlayerPrefs.GetFloat(Cars[i].CarName + "Input");
			Cars[i].Mass = PlayerPrefs.GetFloat(Cars[i].CarName + "Mass");
			Cars[i].AngularDrag = PlayerPrefs.GetFloat(Cars[i].CarName + "AngularDrag");
		}

		GM_Script.ChangeStats ();



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


	}
}
