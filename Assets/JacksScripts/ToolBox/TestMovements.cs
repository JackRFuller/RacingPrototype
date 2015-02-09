using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestMovements : MonoBehaviour {

	private ValueManager VM_Script;
	private LapManager LM_Script;

	public Text GameOverText;

	public float Speed;
	public float TurningSpeed;

	private float moveDirection = 0.0F;
	private float turnDirection = 0.0F;

	private NewInputRegister NIR_Script;


	public bool bl_Crashed;

	//Racing Variables
	public float maxAngularDrag = 2.5F;


	// Use this for initialization
	void Start () {

		VM_Script = GameObject.Find ("ValueManager").GetComponent<ValueManager> ();
		if(Application.loadedLevelName != "FuelLeak")
		{
			LM_Script = GameObject.Find ("Track").GetComponent<LapManager> ();
		}

		NIR_Script = GameObject.Find("Main Camera").GetComponent<NewInputRegister>();
		GameOverText = GameObject.Find("GameOver").GetComponent<Text>();

		maxAngularDrag = PlayerPrefs.GetFloat ("Default_AD");

	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!bl_Crashed && VM_Script.ValuesSet)
		{
			PlayerMovement();
		}


	
	}

	void PlayerMovement()
	{
		moveDirection = Speed;
		rigidbody.AddRelativeForce(0,0,moveDirection);

		turnDirection = NIR_Script.in_Horizontal * TurningSpeed;
		rigidbody.AddRelativeTorque(0,turnDirection,0);

		float currentSpeed = Mathf.Abs(transform.InverseTransformDirection(rigidbody.velocity).z);

		float currentAngularDrag 		= 1.0F;

		float aDragLerpTime 			= currentSpeed * 0.1F;

		float maxDrag 				= 1.0F;	
		float currentDrag 			= 2.5F;
		
		float dragLerpTime			= currentSpeed * 0.1F;

		float CarAngularDrag = Mathf.Lerp(currentAngularDrag, maxAngularDrag, aDragLerpTime);
		float CarDrag = Mathf.Lerp(currentDrag, maxDrag, dragLerpTime);

		rigidbody.angularDrag = CarAngularDrag;
		rigidbody.drag = CarDrag;
	}

	public IEnumerator StartCrash()
	{
		yield return new WaitForSeconds(0.75F);
		if(!VM_Script.GamePaused && !LM_Script.Success)
		{
			bl_Crashed = true;
			GameOverText.enabled = true;
			if(Application.loadedLevelName != "TestScene")
			{
				LM_Script.enabled = false;
			}
		}
		else
		{
			StopAllCoroutines();
		}

	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Wall")
		{
			StartCoroutine(StartCrash());
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.name == "Wall")
		{
			StopAllCoroutines();
		}
	}

	void OnTriggerEnter(Collider cc_Hit)
	{
		if(cc_Hit.gameObject.name  == "InnerTrack")
		{
			StartCoroutine(StartCrash());
		}
		if(cc_Hit.gameObject.name == "JerryCan")
		{
			GameObject.Find("FuelGauge").GetComponent<FuelLeak>().JerryCanUpdate();
		}
	}
	void OnTriggerExit(Collider cc_Hit)
	{
		if(cc_Hit.gameObject.name  == "InnerTrack")
		{
			StopAllCoroutines();
		}
	}
}
