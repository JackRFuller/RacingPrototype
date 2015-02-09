using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ValueManager : MonoBehaviour {

	private CarManager CM_Script;
	private LapManager LM_Script;
	private GameObject Car;

	public bool ValuesSet;
	public Text[] GUIItems;
	public GameObject[] ValueItems;

	public TestMovements TM_Script;
	private Vector3 OriginalPos;
	private float OriginalDrag;
	private float OriginalAngularDrag;
	private Quaternion OriginalRotation;

	private NewInputRegister NIR_Script;

	public Slider SpeedSlider;
	public Slider TurningSlider;
	public Slider InputSlider;
	public Slider MassSlider;
	public Slider ADSlider;

	public Text SpeedValue;
	public Text TurningValue;
	public Text InputValue;
	public Text MassValue;
	public Text CongratsText;
	public Text ADValue;

	private bool bl_UpdateValues;
	public bool GamePaused;

	public GameObject CarBody;
	public GameObject Mudguard1;
	public GameObject Mudguard2;

	// Use this for initialization
	void Start () {

		GamePaused = true;

		CM_Script = GameObject.Find ("CarManager").GetComponent<CarManager> ();
		CarBody.renderer.materials [1].color = CM_Script.Cars [CM_Script.SelectedCar].CarColor;
		Mudguard1.renderer.materials[0].color = CM_Script.Cars [CM_Script.SelectedCar].CarColor;
		Mudguard2.renderer.materials[0].color = CM_Script.Cars [CM_Script.SelectedCar].CarColor;

		Car = GameObject.Find ("TestCar");
		if(Application.loadedLevelName != "FuelLeak")
		{
			LM_Script = GameObject.Find ("Track").GetComponent<LapManager> ();
		}

		TM_Script = Car.GetComponent<TestMovements> ();
		Car = GameObject.Find ("TestCar");
		OriginalPos = Car.transform.position;
		OriginalRotation = Car.transform.rotation;
		OriginalDrag = Car.rigidbody.drag;
		//OriginalAngularDrag = Car.rigidbody.angularDrag;

		NIR_Script = GameObject.Find ("Main Camera").GetComponent<NewInputRegister> ();

		TM_Script.Speed = PlayerPrefs.GetFloat(CM_Script.Cars[CM_Script.SelectedCar].CarName + "Speed");
		TM_Script.TurningSpeed = PlayerPrefs.GetFloat(CM_Script.Cars[CM_Script.SelectedCar].CarName + "TurningSpeed");
		NIR_Script.InputSensitivity = PlayerPrefs.GetFloat(CM_Script.Cars[CM_Script.SelectedCar].CarName + "Input");
		Car.rigidbody.mass = PlayerPrefs.GetFloat(CM_Script.Cars[CM_Script.SelectedCar].CarName + "Mass");
		TM_Script.maxAngularDrag = PlayerPrefs.GetFloat(CM_Script.Cars[CM_Script.SelectedCar].CarName + "AngularDrag");


		SpeedSlider.value = TM_Script.Speed;
		SpeedValue.text = TM_Script.Speed.ToString ();

		TurningSlider.value = TM_Script.TurningSpeed;
		TurningValue.text = TM_Script.TurningSpeed.ToString ();

		InputSlider.value = NIR_Script.InputSensitivity;
		InputValue.text = NIR_Script.InputSensitivity.ToString ("F3");

		MassSlider.value = Car.rigidbody.mass;
		MassValue.text = Car.rigidbody.mass.ToString ("F1");

		ADSlider.value = TM_Script.maxAngularDrag;
		ADValue.text = TM_Script.maxAngularDrag.ToString ();



		foreach(Text Item in GUIItems)
		{
			Item.enabled = false;
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if(!bl_UpdateValues)
		{
			TM_Script.Speed = SpeedSlider.value;
			SpeedValue.text = TM_Script.Speed.ToString ();
			
			TM_Script.TurningSpeed = TurningSlider.value;
			TurningValue.text = TM_Script.TurningSpeed.ToString ();
			
			NIR_Script.InputSensitivity = InputSlider.value;
			InputValue.text = NIR_Script.InputSensitivity.ToString ("F3");
			
			Car.rigidbody.mass = MassSlider.value;
			MassValue.text = Car.rigidbody.mass.ToString ("F2");

			TM_Script.maxAngularDrag = ADSlider.value;
			ADValue.text = TM_Script.maxAngularDrag.ToString("F2");
		}

		if(GamePaused)
		{
			Car.rigidbody.angularDrag = 0.5F;
			Car.transform.position = OriginalPos;
			Car.transform.rotation = OriginalRotation;
			Car.rigidbody.drag = OriginalDrag;
			//Car.rigidbody.angularDrag = OriginalAngularDrag;
			Car.rigidbody.velocity = Vector3.zero;

			NIR_Script.in_Horizontal = 0;
			TM_Script.GameOverText.enabled = false;
			StopCoroutine(TM_Script.StartCrash());
			TM_Script.bl_Crashed = false;
		}
	
	}

	public void ReturnToDefault()
	{

		TM_Script.Speed = 100;
		SpeedSlider.value = 100;

		TM_Script.TurningSpeed = 50;
		TurningSlider.value = 50;

		NIR_Script.InputSensitivity = 1;
		InputSlider.value = 1;

		Car.rigidbody.mass = 1.5F;
		MassSlider.value = 1.5F;

		TM_Script.maxAngularDrag = 3.00F;
		ADSlider.value = 3.00F;

	}

	public void SaveValues()
	{
		PlayerPrefs.SetFloat (CM_Script.Cars[CM_Script.SelectedCar].CarName + "Speed", TM_Script.Speed);
		PlayerPrefs.SetFloat (CM_Script.Cars[CM_Script.SelectedCar].CarName + "TurningSpeed", TM_Script.TurningSpeed);
		PlayerPrefs.SetFloat (CM_Script.Cars[CM_Script.SelectedCar].CarName + "Input", NIR_Script.InputSensitivity);
		PlayerPrefs.SetFloat (CM_Script.Cars[CM_Script.SelectedCar].CarName + "Mass", Car.rigidbody.mass);
		PlayerPrefs.SetFloat (CM_Script.Cars[CM_Script.SelectedCar].CarName + "AngularDrag", TM_Script.maxAngularDrag);
	}

	public void StartRace()
	{
		GamePaused = false;
		Car.transform.rotation = OriginalRotation;
		NIR_Script.in_Horizontal = 0;
		if(Application.loadedLevelName != "FuelLeak")
		{
			LM_Script.CongratsText.enabled = false;
		}

		TM_Script.GameOverText.enabled = false;
		StopCoroutine(TM_Script.StartCrash());
		TM_Script.bl_Crashed = false;

		foreach(GameObject Items in ValueItems)
		{
			Items.SetActive(false);
		}
		foreach(Text Item in GUIItems)
		{
			Item.enabled = true;
		}

		ValuesSet = true;
		if(Application.loadedLevelName != "FuelLeak")
		{
			LM_Script.enabled = true;
			LM_Script.Success = false;
		}

	}

	public void ResetLapTimes()
	{
		if(Application.loadedLevelName != "FuelLeak")
		{
			LM_Script.enabled = false;
			LM_Script.bl_StartTimer = false;
			LM_Script.fl_LapTimer = 0;
			LM_Script.in_LapNumber = -1;
			LM_Script.OverallTime = 0;
			LM_Script.OverallTimer.text = "Current Time: 00:00";
			foreach(Text Item in LM_Script.Laps)
			{
				Item.enabled = false;
			}
		}



	}

	public void EndRace()
	{
		//CongratsText.enabled = false;
		GamePaused = true;

		foreach(GameObject Items in ValueItems)
		{
			Items.SetActive(true);
		}
		foreach(Text Item in GUIItems)
		{
			Item.enabled = false;
		}
		
		ValuesSet = false;


		Car.rigidbody.angularDrag = 0.5F;
		Car.transform.position = OriginalPos;
		Car.transform.rotation = OriginalRotation;
		Car.rigidbody.drag = OriginalDrag;
		//Car.rigidbody.angularDrag = OriginalAngularDrag;
		Car.rigidbody.velocity = Vector3.zero;
		Car.rigidbody.AddRelativeForce (0, 0, 0);

		GameObject PlayButton = GameObject.Find ("PlayButton");
		PlayButton.GetComponent<Image> ().enabled = true;
		PlayButton.collider.enabled = true;
	}
}
