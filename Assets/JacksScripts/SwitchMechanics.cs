using UnityEngine;
using System.Collections;

public class SwitchMechanics : MonoBehaviour {

	private CarController CC_Script;

	public GameObject BoostBar;
	public GameObject BrakeBar;

	public bool bl_Switch;
	private bool bl_MouseUp;

	// Use this for initialization
	void Start () {

		CC_Script = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void Update () {

		bl_MouseUp = false;
	
	}

	void OnMouseUp()
	{
		if(bl_Switch && !bl_MouseUp)		
		{
			SwitchToBrake();

		}
		if(!bl_Switch && !bl_MouseUp)
		{
			SwitchToBoost();

		}

		bl_MouseUp = false;
	}

	void SwitchToBoost()
	{
		CC_Script.bl_BoostAvailable = true;
		CC_Script.bl_BrakeAvailable = false;

		BoostBar.SetActive(true);
		BrakeBar.SetActive(false);

		CC_Script.SetScripts();


		bl_Switch = true;
		bl_MouseUp = true;
	}

	void SwitchToBrake()
	{
		CC_Script.bl_BoostAvailable = false;
		CC_Script.bl_BrakeAvailable = true;

		BoostBar.SetActive(false);
		BrakeBar.SetActive(true);

		CC_Script.SetScripts();

		bl_Switch = false;
		bl_MouseUp = true;
	}
}
