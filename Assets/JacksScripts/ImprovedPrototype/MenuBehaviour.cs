using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour {

	public Vector2 MoveDirection;
	public float Speed;


	public GameObject PivotPoint;
	private float Y_Pos;

	public Vector2[] SnappingPoints;

	public GameObject[] Sliders;
	public bool SlidersActive;

	public GameObject ClosestSnap;

	public string LevelToLoad;

	public Text[] HighScores;
	public bool Sliding;

	// Use this for initialization
	void Start () {

		PivotPoint = GameObject.Find ("Pivot");
		PivotPoint.transform.localPosition = new Vector3 (0, 0, 0);
		Y_Pos = PivotPoint.transform.position.y;	

		if(PlayerPrefs.GetFloat ("BestAllStarTime") > 0)
		{
			HighScores [0].text = "Best Time: " + PlayerPrefs.GetFloat ("BestAllStarTime").ToString ("F2");
		}
		else
		{
			HighScores[0].text = "Best Time: --:--";
		}

		if(PlayerPrefs.GetFloat ("BestFigureof8Time") > 0)
		{
			HighScores [1].text = "Best Time: " + PlayerPrefs.GetFloat ("BestFigureof8Time").ToString ("F2");
		}
		else
		{
			HighScores[1].text = "Best Time: --:--";
		}

		if(PlayerPrefs.GetFloat("BestFuelLeakScore") > 0)
		{
			HighScores [2].text = "High Score: " + PlayerPrefs.GetFloat ("BestFuelLeakScore").ToString ();
		}
		else
		{
			HighScores[2].text = "High Score: --:--";
		}






	}
	
	// Update is called once per frame
	void Update () {

		FindClosest();

		if(Input.touchCount > 0 && !SlidersActive)
		{
			RegisterTouch ();
			Sliding = true;
		}

		if(Input.touchCount == 0)
		{
			if(ClosestSnap)
			{
				PivotPoint.transform.position = new Vector3(ClosestSnap.transform.position.x,PivotPoint.transform.position.y,PivotPoint.transform.position.z);
			}
			Sliding = false;
		}
	}

	GameObject FindClosest()
	{
		GameObject[] SnapPoints;
		SnapPoints = GameObject.FindGameObjectsWithTag ("Snap");

		float distance = Mathf.Infinity;

		Vector3 position = PivotPoint.transform.position;
		foreach(GameObject currentObject in SnapPoints)
		{
			Vector3 distanceCheck = currentObject.transform.position - position;
			float currentDistance = distanceCheck.sqrMagnitude;

			if(currentDistance < distance)
			{
				ClosestSnap = currentObject;
				distance = currentDistance;
			}
		}
		//Debug.Log (ClosestSnap.name);
		return ClosestSnap;
	}

	public void TuneCar()
	{
		foreach(GameObject Item in Sliders)
		{
			Item.SetActive(true);
		}
	}



	void RegisterTouch()
	{
		if(Input.touches[0].phase == TouchPhase.Moved)
		{
			MoveDirection = Input.touches[0].deltaPosition.normalized;
			//Speed = Input.touches[0].deltaPosition.magnitude;
			PivotPoint.transform.Translate(MoveDirection * Speed, Space.Self);
			PivotPoint.transform.position = new Vector3(PivotPoint.transform.position.x,Y_Pos,PivotPoint.transform.position.z);
		}
	}


}
