using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public float fl_Speed;
	public bool bl_Drifting;
	public Transform[] DriftingWaypoints;
	public int in_DriftingPointA;
	public int in_DriftingPointB;
	public int in_SwipingPoint;
	public Transform[] SwipingPoint;
	public string[] RequiredSwipe;

	private float fl_Distance;

	public GameObject go_SwipeDirection;

	public float fl_JourneyTime = 1.0F;
	private float fl_StartTime;



	// Use this for initialization
	void Start () {

		in_DriftingPointA = 0;
		in_DriftingPointB = 1;

		in_SwipingPoint = 0;

		fl_StartTime = Time.time;

	
	}
	
	// Update is called once per frame
	void Update () {

		fl_JourneyTime = Vector3.Distance(DriftingWaypoints[in_DriftingPointA].position, DriftingWaypoints[in_DriftingPointB].position);

		if(!bl_Drifting)
		{
			MoveCar();
		}

		DetectSwipingZone();	

		if(bl_Drifting)
		{
			BeginDrift();
		}
	}

	void BeginDrift()
	{
		float distCovered = (Time.time - fl_StartTime) * 1F;
		float fracJourney = distCovered / fl_JourneyTime;
		transform.position = Vector3.Lerp(DriftingWaypoints[in_DriftingPointA].position, DriftingWaypoints[in_DriftingPointB].position, fracJourney);
	}

	void DetectSwipe()
	{

		if(go_SwipeDirection.guiText.text == RequiredSwipe[in_SwipingPoint])
		{
			bl_Drifting = true;
		}

	}

	void DetectSwipingZone()
	{
		fl_Distance = Vector3.Distance(gameObject.transform.position, SwipingPoint[in_SwipingPoint].transform.position);
		if(fl_Distance < 10)
		{
			DetectSwipe();
		}


	}

	void MoveCar()
	{
		transform.Translate(0,0,fl_Speed * Time.deltaTime);
	}
}
