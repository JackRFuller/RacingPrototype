using UnityEngine;
using System.Collections;

public class RaceStart : MonoBehaviour {

	public SpriteRenderer[] Lights;
	public Color Green;
	public Color Red;

	private float fl_CountdownTime;

	public bool bl_RaceStarted;


	// Use this for initialization
	void Start () {

		fl_CountdownTime = 3;
	
	}
	
	// Update is called once per frame
	void Update () {

		Countdown();
	
	}

	void Countdown()
	{
		fl_CountdownTime -= Time.deltaTime;

		if(fl_CountdownTime < 3)
		{
			Lights[0].color = Red;
		}

		if(fl_CountdownTime < 2)
		{
			Lights[1].color = Red;
		}

		if(fl_CountdownTime < 1)
		{
			Lights[2].color = Red;
		}

		if(fl_CountdownTime < 0)
		{
			foreach(SpriteRenderer SpriteLight in Lights)
			{
				SpriteLight.color = Green;
			}

			bl_RaceStarted = true;
		}

		if(fl_CountdownTime < -1)
		{
			foreach(SpriteRenderer SpriteLight in Lights)
			{
				SpriteLight.enabled = false;
			}
		}
	}
}
