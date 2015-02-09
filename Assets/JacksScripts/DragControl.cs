using UnityEngine;
using System.Collections;

public class DragControl : MonoBehaviour {

	private float fl_Distance;
	private Vector3 v3_Offset;
	private Plane plane;

	public GameObject go_StartingPos;
	public GameObject go_FinishPos;

	public float fl_Weight;
	private RaceStart RS_Script;
	private LevelManager LM_Script;
	private bool bl_StartEnded;

	private bool bl_Settings;

	// Use this for initialization
	void Start () {

		transform.position = go_StartingPos.transform.position;
		LM_Script = GameObject.Find("LevelManager").GetComponent<LevelManager>();


	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(LM_Script.bl_LevelSetUp && !bl_Settings)
		{
			RS_Script = GameObject.Find("LevelManager").GetComponent<RaceStart>();
			bl_Settings = true;
			Debug.Log("B");
		}

		if(bl_Settings)
		{
			if(RS_Script.bl_RaceStarted && !LM_Script.bl_RaceStarted)
			{
				rigidbody.velocity = Vector3.zero;
				CalculateBoost();
				LM_Script.bl_RaceStarted = true;
			}
		}


	
	}

	void CalculateBoost()
	{
		float Distance = Vector3.Distance(go_FinishPos.transform.position, transform.position);

		if(Distance < 10)
		{
			rigidbody.AddForce(Vector3.right * 1);
		}

		if(Distance < 6)
		{
			rigidbody.AddForce(Vector3.right * 3);
		}


		if(Distance < 4)
		{
			rigidbody.AddForce(Vector3.right * 7);
		}

		if(Distance < 2)
		{
			rigidbody.AddForce(Vector3.right * 10);
		}
	}


	void OnMouseDown()
	{
		plane.SetNormalAndPosition(Camera.main.transform.forward, transform.position);

		Ray ray;

#if UNITY_EDITOR

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

#endif

#if UNITY_IOS

		ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

#endif


		float _distance;

		plane.Raycast(ray, out _distance);

		v3_Offset = transform.position - ray.GetPoint(_distance);

		v3_Offset.y = go_StartingPos.transform.position.y;

		rigidbody.freezeRotation = true;

	}

	void OnMouseDrag() {

		if(bl_Settings && !RS_Script.bl_RaceStarted)
		{

			Ray ray;

			#if UNITY_EDITOR
			
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			#endif
			
			#if UNITY_IOS
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			#endif

			float _distance;
			
			plane.Raycast (ray, out _distance);
			
			Vector3 v3Pos = ray.GetPoint (_distance);
			
			v3Pos.y = go_StartingPos.transform.position.y;
			v3Pos.z = go_StartingPos.transform.position.z;
			
			if(transform.position.x > go_FinishPos.transform.position.x)
			{
				rigidbody.AddForce(Vector3.left * fl_Weight);
			}
			
			rigidbody.freezeRotation = true;
		}
			


	}
}
