using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtonBehaviour : MonoBehaviour {

	private MenuBehaviour MB_Script;
	private bool GameModeChosen = false;

	// Use this for initialization
	void Start () {

		MB_Script = GameObject.Find ("MenuManager").GetComponent<MenuBehaviour> ();
		GameModeChosen = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator ResetButtonPress()
	{
		yield return new WaitForSeconds(0.1F);
		if(!GameModeChosen)
		{
			MB_Script.SlidersActive = false;
		}

	}

	void OnMouseDown()
	{
		if(gameObject.tag == "Button")
		{
			gameObject.transform.parent.gameObject.GetComponent<Text>().enabled = false;
			gameObject.transform.FindChild("Info").GetComponent<Text>().enabled = true;
			MB_Script.SlidersActive = true;
		}
	}

	void OnMouseUp()
	{
		if(gameObject.tag == "Button")
		{
			gameObject.transform.parent.gameObject.GetComponent<Text>().enabled = true;
			gameObject.transform.FindChild("Info").GetComponent<Text>().enabled = false;
			MB_Script.SlidersActive = false;
		}

		if(gameObject.name == "Tune")
		{
			if(MB_Script.SlidersActive)
			{
				gameObject.GetComponent<Text>().text = "Tune";
				GameObject.Find("Save").GetComponent<Text>().enabled = false;
				StartCoroutine(ResetButtonPress());

				foreach(GameObject Item in MB_Script.Sliders)
				{
					Item.SetActive(false);
				}
			}
			if(!MB_Script.SlidersActive)
			{
				MB_Script.TuneCar();
				gameObject.GetComponent<Text>().text = "Stop";
				GameObject.Find("Save").GetComponent<Text>().enabled = true;
				MB_Script.SlidersActive = true;
			}
		}

		if(gameObject.name == "AllStarSprint" || gameObject.name == "Figureof8" || gameObject.name == "FuelLeak")
		{
			if(MB_Script.Sliding)
			{
				MB_Script.SlidersActive = true;
				MB_Script.PivotPoint.transform.localPosition = new Vector3(2846,0,0);
				GameObject.Find("GarageText").GetComponent<Text>().text = "Pick A Car";
				//GameObject.Find("Tune").GetComponent<Text>().enabled = false;
				
				GameObject SelectButton = GameObject.Find("Select");
				SelectButton.GetComponent<Image>().enabled = true;
				SelectButton.collider.enabled = true;
				MB_Script.LevelToLoad = gameObject.name;
				GameModeChosen = true;
			}
		}

		if(gameObject.name == "Select")
		{
			if(MB_Script.LevelToLoad == "AllStarSprint")
			{
				Application.LoadLevel("TestTrack");
			}
			if(MB_Script.LevelToLoad == "Figureof8")
			{
				Application.LoadLevel("BigTrack");
			}
			if(MB_Script.LevelToLoad == "FuelLeak")
			{
				Application.LoadLevel("FuelLeak");
			}

			DontDestroyOnLoad(GameObject.Find("CarManager"));
		}
	}
}
