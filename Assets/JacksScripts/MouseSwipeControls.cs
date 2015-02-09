using UnityEngine;
using System.Collections;

public class MouseSwipeControls : MonoBehaviour {

	private Vector2 v2_FirstSwipePos;
	private Vector2 v2_SecondSwipePos;
	private Vector2 v2_CurrentSwipe;

	public GameObject go_SwipeDirection;

	// Use this for initialization
	void Start () {

		go_SwipeDirection = GameObject.Find("SwipeDirection");
	
	}
	
	// Update is called once per frame
	void Update () {

		Swipe();
	
	}

	public void Swipe()
	{

#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0))
		{
			//Saves First Touch Point
			v2_FirstSwipePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		}

		if(Input.GetMouseButtonUp(0))
		{
			//Saves End Touch Point
			v2_SecondSwipePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			v2_CurrentSwipe = new Vector2(v2_SecondSwipePos.x - v2_FirstSwipePos.x, v2_SecondSwipePos.y - v2_FirstSwipePos.y);

			if(v2_FirstSwipePos == v2_SecondSwipePos)
			{
				return;
			}

			v2_CurrentSwipe.Normalize();


			if(v2_CurrentSwipe.y > 0 && v2_CurrentSwipe.x > 0)
			{

				go_SwipeDirection.guiText.text = "Right Up Swipe";
			}
			if(v2_CurrentSwipe.y > 0 && v2_CurrentSwipe.x < 0)
			{

				go_SwipeDirection.guiText.text = "Left Up Swipe";
			}
			if(v2_CurrentSwipe.y < 0 && v2_CurrentSwipe.x < 0)
			{

				go_SwipeDirection.guiText.text = "Left Down Swipe";
			}
			if(v2_CurrentSwipe.y < 0 && v2_CurrentSwipe.x > 0)
			{

				go_SwipeDirection.guiText.text = "Right Down Swipe";
			}
		}
#endif

#if UNITY_IOS

		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);

			if(t.phase == TouchPhase.Began)
			{
				v2_FirstSwipePos = new Vector2(t.position.x, t.position.y);
			}

			if(t.phase == TouchPhase.Ended)
			{
				v2_SecondSwipePos = new Vector2(t.position.x, t.position.y);

				v2_CurrentSwipe = new Vector2(v2_SecondSwipePos.x - v2_FirstSwipePos.x, v2_SecondSwipePos.y - v2_FirstSwipePos.y);
				
				v2_CurrentSwipe.Normalize();

				if(v2_CurrentSwipe.y > 0 && v2_CurrentSwipe.x > 0)
				{
					
					go_SwipeDirection.guiText.text = "Right Up Swipe";
				}
				if(v2_CurrentSwipe.y > 0 && v2_CurrentSwipe.x < 0)
				{
					
					go_SwipeDirection.guiText.text = "Left Up Swipe";
				}
				if(v2_CurrentSwipe.y < 0 && v2_CurrentSwipe.x < 0)
				{
					
					go_SwipeDirection.guiText.text = "Left Down Swipe";
				}
				if(v2_CurrentSwipe.y < 0 && v2_CurrentSwipe.x > 0)
				{
					
					go_SwipeDirection.guiText.text = "Right Down Swipe";
				}
			}

		
		}

#endif
	}
}
