using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GarageManager : MonoBehaviour {

	private CarManager CM_Script;
	public Text[] CarStats;
	public int CarID = 0;
	public int MaxCarID;
	private bool TouchInput;

	public GameObject[] CarParts;

	private MenuBehaviour MB_Script;

	//CarVariables
	private bool UpdateCarStats = false;

	// Use this for initialization
	void Start () {

		CM_Script = GameObject.Find ("CarManager").GetComponent<CarManager> ();	
		MB_Script = GameObject.Find ("MenuManager").GetComponent<MenuBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {

		ChangeCar ();
	
		if(Input.touchCount <= 0)
		{
			TouchInput = false;
		}

		if(MB_Script.SlidersActive)
		{
			if(!UpdateCarStats)
			{
				MB_Script.Sliders[0].GetComponent<Slider>().value = PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "Speed");
				MB_Script.Sliders[1].GetComponent<Slider>().value = PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "TurningSpeed");
				MB_Script.Sliders[2].GetComponent<Slider>().value = PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "Input");
				MB_Script.Sliders[3].GetComponent<Slider>().value = PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "Mass");
				MB_Script.Sliders[4].GetComponent<Slider>().value = PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "AngularDrag");
				UpdateCarStats = true;
			}

			CM_Script.Cars[CarID].Speed = MB_Script.Sliders[0].GetComponent<Slider>().value;
			CarStats[0].text = "Speed: " + CM_Script.Cars [CarID].Speed.ToString ();

			CM_Script.Cars[CarID].TurningSpeed = MB_Script.Sliders[1].GetComponent<Slider>().value;
			CarStats [1].text = "Turning Speed: " + CM_Script.Cars [CarID].TurningSpeed.ToString ();

			CM_Script.Cars[CarID].InputSensitivity = MB_Script.Sliders[2].GetComponent<Slider>().value;
			CarStats [2].text = "Input Speed: " + CM_Script.Cars [CarID].InputSensitivity.ToString("F2");

			CM_Script.Cars[CarID].Mass = MB_Script.Sliders[3].GetComponent<Slider>().value;
			CarStats [3].text = "Mass: " + CM_Script.Cars [CarID].Mass.ToString ("F2");

			CM_Script.Cars[CarID].AngularDrag = MB_Script.Sliders[4].GetComponent<Slider>().value;
			CarStats [4].text = "Drag: " + CM_Script.Cars [CarID].AngularDrag.ToString ("F2");
		}

	}

	void SaveValues()
	{
		PlayerPrefs.SetFloat(CM_Script.Cars[CarID].CarName + "Speed", MB_Script.Sliders[0].GetComponent<Slider>().value);
		PlayerPrefs.SetFloat(CM_Script.Cars[CarID].CarName + "TurningSpeed", MB_Script.Sliders[1].GetComponent<Slider>().value);
		PlayerPrefs.SetFloat(CM_Script.Cars[CarID].CarName + "Input", MB_Script.Sliders[2].GetComponent<Slider>().value);
		PlayerPrefs.SetFloat(CM_Script.Cars[CarID].CarName + "Mass", MB_Script.Sliders[3].GetComponent<Slider>().value);
		PlayerPrefs.SetFloat(CM_Script.Cars[CarID].CarName + "AngularDrag", MB_Script.Sliders[4].GetComponent<Slider>().value);

	}

	void ChangeCar()
	{
		if(Input.touchCount > 0 && !TouchInput)
		{			
			Ray _ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			
			RaycastHit hit;
			
			if(Physics.Raycast(_ray, out hit))
			{				
				if(hit.collider.gameObject.name == "LeftArrow")
				{
					CarID -= 1;
					if(CarID < 0)
					{
						CarID = MaxCarID;
					}
					UpdateCarStats = false;
					ChangeStats();
				}
				if(hit.collider.gameObject.name == "RightArrow")
				{
					CarID += 1;
					if(CarID > MaxCarID)
					{
						CarID = 0;
					}
					UpdateCarStats = false;
					ChangeStats();
				}

				if(hit.collider.gameObject.name == "Save")
				{
					SaveValues();
				}

				TouchInput = true;
			}
		}
	}

	public void ChangeStats()
	{

		CarStats [0].text = "Speed: " + PlayerPrefs.GetFloat (CM_Script.Cars [CarID].CarName + "Speed").ToString ();
		CarStats [1].text = "Turning Speed: " + PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "TurningSpeed").ToString();
		CarStats [2].text = "Input Speed: " + PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "Input").ToString("F2");
		CarStats [3].text = "Mass: " + PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "Mass").ToString("F2");
		CarStats [4].text = "Drag: " + PlayerPrefs.GetFloat(CM_Script.Cars[CarID].CarName + "AngularDrag").ToString("F2");
		CarStats [5].text = CM_Script.Cars [CarID].CarName;

		CarParts[0].renderer.materials[1].color = CM_Script.Cars [CarID].CarColor;
		CarParts[1].renderer.materials[0].color = CM_Script.Cars [CarID].CarColor;
		CarParts[2].renderer.materials[0].color = CM_Script.Cars [CarID].CarColor;

		CM_Script.SelectedCar = CarID;

//		foreach(GameObject Part in CarParts)
//		{
////			if(gameObject.name == "Mudguard")
////			{
////				Part.renderer.material.color = CM_Script.Cars [CarID].CarColor;
////			}
//			if(gameObject.name == "Body")
//			{
//				Part.renderer.materials[1].color = CM_Script.Cars [CarID].CarColor;
//			}
//		}



	}


}
