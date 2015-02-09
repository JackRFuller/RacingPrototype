using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour {

	public bool bl_RaceFinished;
	public bool bl_RaceStarting;
	public int in_Countdown;

	private GameObject go_CountdownText;
	public GameObject[] go_Inputs;
	private CarController car;

	// Use this for initialization
	void Start () {

		go_CountdownText = GameObject.Find("CountdownText");
		car = GameObject.Find("Car").GetComponent<CarController>();
		bl_RaceStarting = true;
		in_Countdown = 4;
		StartCoroutine(ChangeNumber(1.0F));
	}
	
	// Update is called once per frame
	void Update () {

		if(bl_RaceStarting)
		{
			car.Move(0,1);
			if(in_Countdown == 0){
				StopAllCoroutines();
				go_CountdownText.guiText.text = "Go!";
				if(Application.loadedLevelName == "TapControlScheme"){

					foreach(GameObject _go_Inputs in go_Inputs)
					{
						_go_Inputs.SetActive(true);
					}

				}
			
				StartCoroutine(TurnOffCountdown(1.0F));
				bl_RaceStarting = false;
			}

		}
	}

	IEnumerator TurnOffCountdown(float fl_Time)
	{

		yield return new WaitForSeconds(fl_Time);
		go_CountdownText.guiText.enabled = false;
	}

	IEnumerator ChangeNumber(float fl_WaitTime)
	{
		while(in_Countdown > 0){
			in_Countdown -= 1;
			go_CountdownText.guiText.text = in_Countdown.ToString();
			yield return new WaitForSeconds(fl_WaitTime);
				

		}





	}


}
