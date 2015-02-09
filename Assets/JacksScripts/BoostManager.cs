using UnityEngine;
using System.Collections;

public class BoostManager : MonoBehaviour {

	private GameObject go_BoostBar;
	public bool bl_PlayerBoosting;
	public float fl_BoostAvailable;

	public float fl_BoostGrowthRate;
	public float fl_BoostReduceRate;

	public float fl_Speed;
	private GameObject go_Car;

	//Boost Bar
	public float fl_ScaleFactor;

	// Use this for initialization
	void Start () {

		go_BoostBar = transform.FindChild("Boostbar").gameObject;
		go_Car = GameObject.Find("Car");
		fl_BoostAvailable = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(!bl_PlayerBoosting)
		{
			GenerateBoost();

		}
		if(bl_PlayerBoosting)
		{
			ReduceBoost();
			if(go_BoostBar.transform.localScale.x < 0.1)
			{
				bl_PlayerBoosting = false;
			}

		}
		if(fl_BoostAvailable >= 98)
		{
			fl_BoostAvailable = 100;
		}	
	}



	void OnMouseDown()
	{
		if(go_BoostBar.transform.localScale.x > 0.5)
		{
			bl_PlayerBoosting = true;
		}
		else
		{
			bl_PlayerBoosting = false;
			Debug.Log("Hit");
		}

	}

	void OnMouseUp()
	{
		bl_PlayerBoosting = false;
	}

	void ReduceBoost()
	{
		if(go_BoostBar.transform.localScale.x >= 0.1)
		{
			fl_ScaleFactor -= fl_BoostReduceRate;
			
			go_BoostBar.transform.localScale = new Vector3(fl_ScaleFactor,go_BoostBar.transform.localScale.y,go_BoostBar.transform.localScale.z);
			
			if(fl_BoostAvailable > 0)
			{
				fl_BoostAvailable -= 0.215F;
			}
		}
	}

	void GenerateBoost()
	{
		if(go_BoostBar.transform.localScale.x < 4.1)
		{
			fl_ScaleFactor += fl_BoostGrowthRate;
				
			go_BoostBar.transform.localScale = new Vector3(fl_ScaleFactor,go_BoostBar.transform.localScale.y,go_BoostBar.transform.localScale.z);

			if(fl_BoostAvailable < 100)
			{
				fl_BoostAvailable += 0.215F;
			}
		}
	}
}
