using UnityEngine;
using System.Collections;

public class BrakeManager : MonoBehaviour {

	private GameObject go_BrakeBar;
	public bool bl_PlayerBraking;

	//Brake Bar
	public float fl_ScaleFactor;
	public float fl_BrakeGrowthRate;
	public float fl_BrakeReduceRate;

	public float fl_DefaultValue;

	// Use this for initialization
	void Start () {

		go_BrakeBar = transform.FindChild("Brakebar").gameObject;

	
	}
	
	// Update is called once per frame
	void Update () {

		if(bl_PlayerBraking)
		{
			GenerateHeat();
		}
		if(!bl_PlayerBraking)
		{
			ReduceHeat();
		}
	
	}

	void GenerateHeat()
	{
		if(go_BrakeBar.transform.localScale.x < 56.65)
		{
			fl_ScaleFactor += fl_BrakeGrowthRate;
			
			go_BrakeBar.transform.localScale = new Vector3(fl_ScaleFactor,go_BrakeBar.transform.localScale.y,go_BrakeBar.transform.localScale.z);	

		}
		if(go_BrakeBar.transform.localScale.x >= 56.65)
		{
			bl_PlayerBraking = false;			
		}
	}

	void ReduceHeat()
	{
		if(go_BrakeBar.transform.localScale.x > 3)
		{
			fl_ScaleFactor -= fl_BrakeReduceRate;
			
			go_BrakeBar.transform.localScale = new Vector3(fl_ScaleFactor,go_BrakeBar.transform.localScale.y,go_BrakeBar.transform.localScale.z);		
			
		}
	}

	void OnMouseDown()
	{
		bl_PlayerBraking = true;
	}

	void OnMouseUp()
	{
		bl_PlayerBraking = false;
	}
}
