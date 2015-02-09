using UnityEngine;
using System.Collections;

public class ScaleBoostBar : MonoBehaviour {

	public float fl_ScaleFactor;
	public float fl_ScalingRate;


	private PointsManager PM_Script;

	// Use this for initialization
	void Start () {

		PM_Script = GameObject.Find("LevelManager").GetComponent<PointsManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.localScale.x <= 29.8)
		{
			fl_ScaleFactor += fl_ScalingRate;
			
			transform.localScale = new Vector3(fl_ScaleFactor,transform.localScale.y,transform.localScale.z);
		}
		else
		{
			PM_Script.bl_Boosting = true;
		}



	
	}
}
