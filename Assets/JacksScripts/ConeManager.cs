using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConeManager : MonoBehaviour {

	public GameObject[] go_DrivingCones;
	private int in_ConeIndicator;
	private int in_OriginalNum;

	//Text
	public Text GameOver;
	public Text Timer;
	public Text HighScore;
	public Text CurrentScore;

	//Points
	private int in_Points = 0;
	private bool bl_RunGameMode = true;

	//Timer
	private float fl_Countdown = 10.0F;

	// Use this for initialization
	void Start () {

		in_ConeIndicator = Random.Range(0,6);
		go_DrivingCones[in_ConeIndicator].SetActive(true);

		//Highscore
		if(PlayerPrefs.GetInt("Start") == 0)
		{
			PlayerPrefs.SetInt("HighScoreCone", 0);
			PlayerPrefs.SetInt("Start", 1);
		}

		//Text Settings
		GameOver.enabled = false;
		HighScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScoreCone").ToString();
		CurrentScore.text = "Current Points: " + in_Points.ToString();

	}
	
	// Update is called once per frame
	void Update () {

		if(bl_RunGameMode && GameOver.enabled == false)
		{
			CountdownTimer();
		}
	
	}

	void CountdownTimer()
	{
		if(fl_Countdown >= 0)
		{
			fl_Countdown -= Time.deltaTime;
			Timer.text = "Time Left: " + fl_Countdown.ToString("F2");
		}
		
		if(fl_Countdown < 0)
		{
			
			bl_RunGameMode = false;
			GameOver.enabled = true;
			Timer.text = "Time Left: 00.00";
		}
	}

	public void AddPoints()
	{
		if(bl_RunGameMode)
		{
			go_DrivingCones[in_ConeIndicator].SetActive(false);
			in_OriginalNum = in_ConeIndicator;
			
			in_ConeIndicator = Random.Range(0,6);
			if(in_ConeIndicator == in_OriginalNum)
			{
				in_ConeIndicator = Random.Range(0,6);
			}
			go_DrivingCones[in_ConeIndicator].SetActive(true);
			
			in_Points += 10;
			if(PlayerPrefs.GetInt("HighScoreCone") < in_Points)
			{
				PlayerPrefs.SetInt("HighScoreCone", in_Points);
			}
			
			HighScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScoreCone").ToString();
			CurrentScore.text = "Current Points: " + in_Points.ToString();

			fl_Countdown += 7.5F;
		}


	}
}
