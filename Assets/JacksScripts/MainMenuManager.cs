	using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	private GameObject CoinInput;
	public string LevelToLoad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{

		if(gameObject.name == "DefaultButton")
		{
			ValueManager VM_Script = GameObject.Find("ValueManager").GetComponent<ValueManager>();
			
			VM_Script.ReturnToDefault();
		}
		if(gameObject.name == "SaveButton")
		{
			ValueManager VM_Script = GameObject.Find("ValueManager").GetComponent<ValueManager>();
			
			VM_Script.SaveValues();
		}

		if(gameObject.name == "Yes")
		{
			Debug.Log("YES");
			ValueManager VM_Script = GameObject.Find("ValueManager").GetComponent<ValueManager>();
			VM_Script.SaveValues();

			Application.LoadLevel("MainMenu");
			Destroy(GameObject.Find("CarManager"));
		}

		if(gameObject.name == "No")
		{
			Debug.Log("No");
			Application.LoadLevel("MainMenu");
			Destroy(GameObject.Find("CarManager"));
		}


		if(gameObject.name == "TapControls")
		{
			Application.LoadLevel("TapControlScheme");
		}

		if(gameObject.name == "SteeringWheel")
		{
			Application.LoadLevel("SteeringWheelControlScheme");
		}

		if(gameObject.name == "SwipeControls")
		{
			Application.LoadLevel("SwipeControls");
		}

		if(gameObject.name == "HomeButton")
		{
			ValueManager VM_Script = GameObject.Find("ValueManager").GetComponent<ValueManager>();
			VM_Script.EndRace();
			VM_Script.ResetLapTimes();

			StopCoroutine(VM_Script.TM_Script.StartCrash());
			GameObject.Find("GameOver").GetComponent<Text>().enabled = false;
			GameObject.Find("CongratsText").GetComponent<Text>().enabled = false;

			GameObject.Find("SaveText").GetComponent<Text>().enabled = true;
			GameObject Yes = GameObject.Find("Yes");
			Yes.collider.enabled = true;
			Yes.GetComponent<Text>().enabled = true;

			GameObject No = GameObject.Find("No");
			No.collider.enabled = true;
			No.GetComponent<Text>().enabled = true;
		}

		if(gameObject.name == "Play")
		{
			GameObject CarManager = GameObject.Find("CarManager");
			Application.LoadLevel(LevelToLoad);
			DontDestroyOnLoad(CarManager);
		}

        if (gameObject.name == "PlayButton")
        {
			if(Application.loadedLevelName != "FuelLeak")
			{
				ValueManager VM_Script = GameObject.Find("ValueManager").GetComponent<ValueManager>();
				
				VM_Script.StartRace();
				gameObject.GetComponent<Image>().enabled = false;
				gameObject.collider.enabled = false;
			}
			else
			{
				ValueManager VM_Script = GameObject.Find("ValueManager").GetComponent<ValueManager>();				
				VM_Script.StartRace();
				FuelLeak FM_Script = GameObject.Find("FuelGauge").GetComponent<FuelLeak>();
				FM_Script.GameStarted = true;
				gameObject.GetComponent<Image>().enabled = false;
				gameObject.collider.enabled = false;

				GameObject HomeButton = GameObject.Find ("HomeButton");
				HomeButton.GetComponent<Image> ().enabled = false;
				HomeButton.collider.enabled = false;
			}
				
	
           
        }
	}
}
