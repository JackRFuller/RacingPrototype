using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FuelLeak : MonoBehaviour {

	public float FuelDecreaseRate;
	private SpriteRenderer Ring;
	private GameObject JerryCan;
	public Transform[] JerryCanPositions;
	public float FuelAmount = 50;
	public float FuelIncreaseRate;
	private int MaxPositions;

	//Locator Varaiables
	private GameObject Car;
	private GameObject Arrow;
	public float RotationSpeed;

	private Quaternion LookRotation;
	private Vector3 direction;

	//Timer Variables
	private float Timer = 90;
	public bool GameStarted = false;
	public Text[] TimeGUI;
	private int Score= 0;


	//Modifier Variables
	public Text ModTimerText;
	public float ModTimer = 10;
	public Color[] ModifierColours;
	public bool ModifierInEffect;
	public string CurrentModifier;
	private bool PauseTime = false;
	private bool ReduceRate;
	public Text[] ModifierGUI;

	//Score Varaibles
	public Text[] ScoreGUI;
	private bool DoublePoints;

	// Use this for initialization
	void Start () {

		JerryCan = GameObject.Find ("JerryCan");
		Car = GameObject.Find ("TestCar");
		MaxPositions = JerryCanPositions.Length;
		JerryCan.transform.localPosition = JerryCanPositions[Random.Range(0,MaxPositions)].transform.position;
		Arrow = GameObject.Find ("Arrow");
	
		TimeGUI[1].text = "Current Score: " + Score.ToString();

		GameObject HomeButton = GameObject.Find ("HomeButton");
		HomeButton.GetComponent<Image> ().enabled = true;
		HomeButton.collider.enabled = true;	
		Ring = JerryCan.transform.FindChild ("Ring").gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {


		ModifierGUI [1].text = "Modifier Active: " + ModifierInEffect.ToString ();
		if(!ModifierInEffect)
		{
			ModifierGUI [0].text = "Current Modifier: Null";
			ModTimerText.enabled = false;
		}
		else
		{
			ModifierGUI [0].text = "Current Modifier: " + CurrentModifier;
			ModTimer -= Time.deltaTime;
			ModTimerText.text = "Modifier Time Left: " + ModTimer.ToString("F2");
		}
	


		if(GameStarted)
		{
			if(!PauseTime)
			{
				GameManager();
			}


			if(FuelAmount > 0)
			{
				if(!ReduceRate)
				{
					FuelDecreaseRate = 3.5F;
				}
				else
				{
					FuelDecreaseRate = 1;
				}
				FuelAmount -= FuelDecreaseRate * Time.deltaTime;
				transform.localScale = new Vector3 (FuelAmount, transform.localScale.y, transform.localScale.z);
			}
			if(FuelAmount < 0 || Timer < 0)
			{
				GameOverState();
			}
			Locator ();
		}
	
	}

	void GameOverState()
	{
		GameStarted = false;
		TimeGUI [2].enabled = true;

		if(FuelAmount > 0)
		{
			TimeGUI[2].text = "Time Up!";
			ScoreGUI [0].text = "Number of Jerry Cans: " + Score.ToString () + " x 10";
			ScoreGUI [1].text = "Fuel Remaining: " + FuelAmount.ToString ("F0") + " x 10";
			
			float TempFuel = Mathf.RoundToInt (FuelAmount);
			
			float FinalScore = (Score * 10) + (TempFuel * 10);
			
			ScoreGUI [2].text = "Final Score: " + FinalScore.ToString ("F0");
			if(FinalScore > PlayerPrefs.GetFloat("BestFuelLeakScore"))
			{
				PlayerPrefs.SetFloat("BestFuelLeakScore", FinalScore);
			}
			
			foreach(Text Item in ScoreGUI)
			{
				Item.enabled = true;
			}
			
			GameObject.Find ("Border").GetComponent<Image> ().enabled = true;
			TimeGUI[0].enabled = false;
			TimeGUI[1].enabled = false;
		}

		if(FuelAmount < 0)
		{
			TimeGUI[2].text = "Game Over!";
			GameObject.Find("Border1").GetComponent<Image>().enabled = true;
			GameObject.Find("FuelMessage").GetComponent<Text>().enabled = true;
		}


	}

	void GameManager()
	{
		Timer -= Time.deltaTime;
		TimeGUI [0].text = "Current Time: " + Timer.ToString ("F2");
		TimeGUI[1].text = "Current Score: " + Score.ToString();

		if(Timer < 0)
		{
			TimeGUI [0].text = "Current Time: 00:00";
			GameStarted = false;
		}
	}

	public void ResetGame()
	{
		GameObject[] Barriers = GameObject.FindGameObjectsWithTag ("Barrier");
		foreach(GameObject Barrier in Barriers)
		{
			Barrier.collider.enabled = false;
			Barrier.renderer.enabled = false;
		}

		ModifierInEffect = false;
		CurrentModifier = null;
		StopAllCoroutines ();
		GameStarted = false;
		Timer = 90;
		Score = 0;
		FuelAmount = 50;
		transform.localScale = new Vector3 (FuelAmount, transform.localScale.y, transform.localScale.z);


		TimeGUI [0].text = "Current Time: " + Timer.ToString ("F2");
		TimeGUI[1].text = "Current Score: " + Score.ToString();

		foreach(Text Item in ScoreGUI)
		{
			Item.enabled = false;
		}
		
		GameObject.Find ("Border").GetComponent<Image> ().enabled = false;
		GameObject.Find("Border1").GetComponent<Image>().enabled = false;
		GameObject.Find("FuelMessage").GetComponent<Text>().enabled = false;

		GameObject HomeButton = GameObject.Find ("HomeButton");
		HomeButton.GetComponent<Image> ().enabled = true;
		HomeButton.collider.enabled = true;
	}

	void Locator()
	{

		float distance = Vector3.Distance (Car.transform.position, JerryCan.transform.position);
		if(distance > 50)
		{
			Arrow.transform.FindChild("Icon").gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
		else
		{
			Arrow.transform.FindChild("Icon").gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
		direction = (JerryCan.transform.position - Arrow.transform.position).normalized;
		LookRotation = Quaternion.LookRotation (direction);
		Arrow.transform.rotation = Quaternion.Slerp (Arrow.transform.rotation, LookRotation, RotationSpeed * Time.deltaTime);
	}

	void DetermineModifier()
	{
		int RandomNumber = Random.Range (0, 11);
		ModifierGUI [2].text = "Modifier Number: " + RandomNumber.ToString ();		 

		if((RandomNumber >= 7 && RandomNumber < 11) && !ModifierInEffect)
		{
			if(RandomNumber == 7)
			{
				CurrentModifier = "LeakRate";
				ModifierGUI[3].text = "Reduced Leak Rate";
				ModTimer = 10;
			}
			if(RandomNumber == 8)
			{
				CurrentModifier = "DoublePoints";
				ModifierGUI[3].text = "Double Points";
				ModTimer = 10;
			}
			if(RandomNumber == 9)
			{
				CurrentModifier = "PauseTime";
				ModifierGUI[3].text = "Paused Timer";
				ModTimer = 10;
			}
			if(RandomNumber == 10)
			{
				CurrentModifier = "Barriers";
				ModifierGUI[3].text = "Barriers";
				ModTimer = 20;

				GameObject[] Barriers = GameObject.FindGameObjectsWithTag("Barrier");
				foreach(GameObject Barrier in Barriers)
				{
					Barrier.collider.enabled = true;
					Barrier.renderer.enabled = true;
				}
			}


			ModifierGUI[3].enabled = true;
			ModTimerText.enabled = true;
			ModifierInEffect = true;
			ModifierEffect();
			StartCoroutine(RemoveText());
		}

		if(RandomNumber < 7)
		{
			Ring.enabled = false;
		}
	}

	IEnumerator RemoveText()
	{
		yield return new WaitForSeconds (1.5F);
		ModifierGUI[3].enabled = false;
	}

	public void ModifierEffect()
	{
		if(CurrentModifier == "LeakRate")
		{
			ReduceRate = true;
			StartCoroutine(ReduceTimer());
		}
		if(CurrentModifier == "DoublePoints")
		{
			DoublePoints = true;
			StartCoroutine(PointsTimer());
		}
		if(CurrentModifier == "PauseTime")
		{
			PauseTime = true;
			StartCoroutine(BeginTimer());
		}
		if(CurrentModifier == "Barriers")
		{
			StartCoroutine(BarrierTimer());
		}
	}

	IEnumerator BarrierTimer()
	{
		yield return new WaitForSeconds (20.0F);
		GameObject[] Barriers = GameObject.FindGameObjectsWithTag ("Barrier");
		foreach(GameObject Barrier in Barriers)
		{
			Barrier.collider.enabled = false;
			Barrier.renderer.enabled = false;
		}
		ModifierInEffect = false;
	}

	IEnumerator PointsTimer()
	{
		yield return new WaitForSeconds (10.0F);
		DoublePoints = false;
		ModifierInEffect = false;
	}

	IEnumerator ReduceTimer()
	{
		yield return new WaitForSeconds (10.0F);
		ReduceRate = false;
		ModifierInEffect = false;
	}

	IEnumerator BeginTimer()
	{
		yield return new WaitForSeconds (10.0F);
		PauseTime = false;
		ModifierInEffect = false;
	}

	public void JerryCanUpdate()
	{
		if(GameStarted)
		{
			Vector3 LastPos = JerryCan.transform.localPosition;
			if(DoublePoints)
			{
				Score += 2;
			}
			else
			{
				Score += 1;
			}


			DetermineModifier();
			
			JerryCan.transform.localPosition = JerryCanPositions[Random.Range(0,MaxPositions)].transform.position;

			while(JerryCan.transform.position == LastPos)
			{
				JerryCan.transform.localPosition = JerryCanPositions[Random.Range(0,MaxPositions)].transform.position;
			}	

			FuelAmount += FuelIncreaseRate;
			if(FuelAmount > 50)
			{
				FuelAmount = 50;
			}
		}
	}
}
