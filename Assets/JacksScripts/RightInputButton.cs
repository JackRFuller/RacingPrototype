using UnityEngine;
using System.Collections;

public class RightInputButton : MonoBehaviour {

	private bool bl_Turning;
	public float fl_TurningSpeed;
	public float fl_TurningAngle;
	public float fl_FowardSpeed;
	
	private GameObject go_LeftButton;
	
	private CarController car;
	private LeftInputButton LTB_Script;

	// Use this for initialization
	void Start () {

		car = GameObject.Find("Car").GetComponent<CarController>();
		fl_FowardSpeed = 1F;
		
		go_LeftButton = GameObject.Find("LeftInput");
		LTB_Script = go_LeftButton.GetComponent<LeftInputButton>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(bl_Turning && gameObject.collider.enabled == true){
			
			if(LTB_Script.fl_TurningSpeed < 1F)
			{
				LTB_Script.fl_TurningSpeed += 0.05F;
			}
		}
	}

	void OnMouseDown()
	{
		bl_Turning = true;
		go_LeftButton.collider.enabled = false;
		
	}
	
	void OnMouseUp()
	{
		go_LeftButton.collider.enabled = true;
		bl_Turning = false;
		LTB_Script.fl_TurningSpeed = 0;
		
	}
}
