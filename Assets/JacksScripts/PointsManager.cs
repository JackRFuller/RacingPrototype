using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {

	public float fl_Countdown;
	public Text Timer;
	public Text Points;
	public int in_NumofPoints;

	public bool bl_RegisterPoints;

	public Transform[] CoinPositions;

	private GameObject go_Coin;
	public int in_CoinPos;
	private int in_LastPos;
	public Text GameOver;
	public int in_HighScore;
	private Text HighScoreText;

	public bool bl_Boosting;
	public int in_BoostAvailable;

   




	// Use this for initialization
	void Start () {

		go_Coin = GameObject.Find("GoldenCoin");
		
		in_CoinPos = Random.Range(1,14);
		in_LastPos = in_CoinPos;
		
		go_Coin.transform.position = CoinPositions[in_CoinPos].transform.position;
		GameOver.enabled = false;
		
		if(PlayerPrefs.GetInt("Start") == 0)
		{
			PlayerPrefs.SetInt("HighScore", 0);
			PlayerPrefs.SetInt("Start", 1);
		}
		HighScoreText = GameObject.Find("HighScore").GetComponent<Text>();
		HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
		
		bl_Boosting = false;
		in_BoostAvailable = 0;

		fl_Countdown = 10;
		in_NumofPoints = 0;
		in_CoinPos = 0;

		Points = GameObject.Find("Points").GetComponent<Text>();
		Timer = GameObject.Find("Timer").GetComponent<Text>();
		Points.text = "Current Points: " + in_NumofPoints.ToString();
		bl_RegisterPoints = true;


	
	}
	
	// Update is called once per frame
	void Update () {

		if(bl_RegisterPoints)
		{
			RunTimer();
		}

	
	}

	public void MoveCoin()
	{
		go_Coin.gameObject.transform.FindChild("Points").renderer.enabled = true;

		in_CoinPos = Random.Range(1,14);

		if(in_CoinPos != in_LastPos)
		{
			go_Coin.transform.position = CoinPositions[in_CoinPos].transform.position;
			in_LastPos = in_CoinPos;
		}
		else
		{
			if(in_CoinPos == in_LastPos)
			{
				MoveCoin();
			}
		}

	}

	public void AddOnPoints()
	{
		if(bl_RegisterPoints)
		{
			in_NumofPoints += 10;
			fl_Countdown += 5;
			
			Points.text = "Current Points: " + in_NumofPoints.ToString();

			if(PlayerPrefs.GetInt("HighScore") < in_NumofPoints)
			{
				PlayerPrefs.SetInt("HighScore", in_NumofPoints);
				HighScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
			}
		}

	}

	void RunTimer()
	{
		if(fl_Countdown >= 0)
		{
			fl_Countdown -= Time.deltaTime;
			Timer.text = "Time Left: " + fl_Countdown.ToString("F2");
		}

		if(fl_Countdown < 0)
		{

			bl_RegisterPoints = false;
			GameOver.enabled = true;
			Timer.text = "Time Left: 00.00";
		}


	}
}
