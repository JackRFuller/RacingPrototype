using UnityEngine;
using System.Collections;

public class ShowArrow : MonoBehaviour {

	public GameObject go_RightArrow;
	public GameObject go_LeftArrow;
	

	// Use this for initialization
	void Start () {



	
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR

		if(Input.GetMouseButtonDown(0)){
			
			WhenMouseClick();
		}
		if(Input.GetMouseButtonUp(0)){
			
			go_LeftArrow.GetComponent<SpriteRenderer>().enabled = false;
			go_RightArrow.GetComponent<SpriteRenderer>().enabled = false;
		}

#endif

#if UNITY_IOS

		TouchButton();
		if(Input.touchCount <= 0)
		{
			go_LeftArrow.GetComponent<SpriteRenderer>().enabled = false;
			go_RightArrow.GetComponent<SpriteRenderer>().enabled = false;
		}
	
#endif
	}

	void WhenMouseClick()
	{
		Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		if(Physics.Raycast(_ray, out hit))
		{
			if(hit.collider.gameObject.name == "RightInput")
			{
				go_RightArrow.transform.position = hit.point;
				go_RightArrow.GetComponent<SpriteRenderer>().enabled = true;
			}
			if(hit.collider.gameObject.name == "LeftInput")
			{
				go_LeftArrow.transform.position = hit.point;
				go_LeftArrow.GetComponent<SpriteRenderer>().enabled = true;
			}			
		}
	}
	

	void TouchButton()
	{
		if(Input.touchCount > 0)
		{
			Ray _ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			
			RaycastHit hit;

			if(Physics.Raycast(_ray, out hit))
			{	
				if(hit.collider.gameObject.name == "RightInput")
				{
					go_RightArrow.transform.position = new Vector3(hit.point.x, go_RightArrow.transform.position.y, hit.point.z);
					go_RightArrow.GetComponent<SpriteRenderer>().enabled = true;
				}
				if(hit.collider.gameObject.name == "LeftInput")
				{
					go_LeftArrow.transform.position = new Vector3(hit.point.x, go_LeftArrow.transform.position.y, hit.point.z);
					go_LeftArrow.GetComponent<SpriteRenderer>().enabled = true;
				}			
			

			}
		}
	}
}
