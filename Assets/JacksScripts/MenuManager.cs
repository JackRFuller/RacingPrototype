using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour {


	public GameObject go_AcclerationButton;
	public GameObject go_CarControlButton;
	public Text TX_PlayButton;

	private bool bl_Acceleration;
	private bool bl_CarControl;

	private bool bl_TouchInput;

	private GameObject go_GameManager;
	private GameManager GM_Script;
	private bool bl_BonusMode;

	// Use this for initialization
	void Start () {

		bl_Acceleration = false;
		bl_CarControl = false;
		bl_TouchInput = false;
		TX_PlayButton.enabled = false;

		go_GameManager = GameObject.Find("GameManager");
		GM_Script = go_GameManager.GetComponent<GameManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR

		if(Input.GetMouseButtonUp(0)){

			MouseClick();
		}


#endif

#if UNITY_IOS

		TouchButton();

		if(Input.touchCount <= 0)
		{
			bl_TouchInput = false;
		}

#endif

		if(bl_CarControl || bl_BonusMode)
		{
			TX_PlayButton.enabled = true;
		}

	
	}

	void MouseClick()
	{
		Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		if(Physics.Raycast(_ray, out hit))
		{
			
			if(hit.collider.tag == "CarControl")
			{
				if(hit.collider.gameObject != go_CarControlButton && !bl_TouchInput)
				{
					if(go_CarControlButton != null)
					{
						go_CarControlButton.GetComponent<Text>().color = Color.white;
						go_CarControlButton.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;
					}
					
					hit.collider.gameObject.GetComponent<Text>().color = Color.green;
					hit.collider.gameObject.transform.FindChild("Instructions").GetComponent<Text>().enabled = true;
					go_CarControlButton = hit.collider.gameObject;
					bl_CarControl = true;
					bl_TouchInput = true;
					GM_Script.st_CarControl = hit.collider.gameObject.name;
					GM_Script.bl_CarActivated = true;
					bl_BonusMode = false;
				}
				
				if(hit.collider.gameObject == go_CarControlButton && !bl_TouchInput)
				{
					hit.collider.gameObject.GetComponent<Text>().color = Color.white;
					hit.collider.gameObject.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;
					go_CarControlButton = null;
					bl_CarControl = false;
					bl_TouchInput = true;
					GM_Script.st_CarControl = null;
					GM_Script.bl_CarActivated = false;
				}
			}

			if(hit.collider.gameObject.name == "Bonus")
			{
				if(hit.collider.gameObject != go_CarControlButton && !bl_TouchInput)
				{
					if(go_CarControlButton != null)
					{						
						go_CarControlButton.GetComponent<Text>().color = Color.white;
						go_CarControlButton.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;
					}
					
					go_CarControlButton = hit.collider.gameObject;
					hit.collider.gameObject.GetComponent<Text>().color = Color.green;
					hit.collider.gameObject.transform.FindChild("Instructions").GetComponent<Text>().enabled = true;
					hit.collider.gameObject.GetComponent<Text>().color = Color.green;
					bl_TouchInput = true;
					bl_BonusMode = true;
				}

				if(hit.collider.gameObject == go_CarControlButton && !bl_TouchInput)
				{
					hit.collider.gameObject.GetComponent<Text>().color = Color.white;
					go_CarControlButton.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;				
					go_CarControlButton = null;
					bl_TouchInput = true;
					bl_BonusMode = false;
				}

			}
			
			if(hit.collider.gameObject.name == "PlayButton")
			{
				if(bl_BonusMode)
				{
					Application.LoadLevel("PointsScene");
				}
				if(!bl_BonusMode)
				{
					DontDestroyOnLoad(go_GameManager);
					Application.LoadLevel("RaceScene");
				}
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
				if(hit.collider.tag == "CarControl")
				{
					if(hit.collider.gameObject != go_CarControlButton && !bl_TouchInput)
					{
						if(go_CarControlButton != null)
						{
							go_CarControlButton.GetComponent<Text>().color = Color.white;
							go_CarControlButton.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;
						}
						
						hit.collider.gameObject.GetComponent<Text>().color = Color.green;
						hit.collider.gameObject.transform.FindChild("Instructions").GetComponent<Text>().enabled = true;
						go_CarControlButton = hit.collider.gameObject;
						bl_CarControl = true;
						bl_TouchInput = true;
						GM_Script.st_CarControl = hit.collider.gameObject.name;
						GM_Script.bl_CarActivated = true;
						bl_BonusMode = false;
					}
					
					if(hit.collider.gameObject == go_CarControlButton && !bl_TouchInput)
					{
						hit.collider.gameObject.GetComponent<Text>().color = Color.white;
						hit.collider.gameObject.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;
						go_CarControlButton = null;
						bl_CarControl = false;
						bl_TouchInput = true;
						GM_Script.st_CarControl = null;
						GM_Script.bl_CarActivated = false;
					}
				}
				if(hit.collider.gameObject.name == "Bonus")
				{
					if(hit.collider.gameObject != go_CarControlButton && !bl_TouchInput)
					{
						if(go_CarControlButton != null)
						{						
							go_CarControlButton.GetComponent<Text>().color = Color.white;
							go_CarControlButton.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;
						}
						
						go_CarControlButton = hit.collider.gameObject;
						hit.collider.gameObject.GetComponent<Text>().color = Color.green;
						hit.collider.gameObject.transform.FindChild("Instructions").GetComponent<Text>().enabled = true;
						hit.collider.gameObject.GetComponent<Text>().color = Color.green;
						bl_TouchInput = true;
						bl_BonusMode = true;
					}
					
					if(hit.collider.gameObject == go_CarControlButton && !bl_TouchInput)
					{
						hit.collider.gameObject.GetComponent<Text>().color = Color.white;
						go_CarControlButton.transform.FindChild("Instructions").GetComponent<Text>().enabled = false;				
						go_CarControlButton = null;
						bl_TouchInput = true;
						bl_BonusMode = false;
					}
					
				}
				if(hit.collider.gameObject.name == "PlayButton")
				{
					if(bl_BonusMode)
					{
						Application.LoadLevel("PointsScene");
					}
					if(!bl_BonusMode)
					{
						DontDestroyOnLoad(go_GameManager);
						Application.LoadLevel("RaceScene");
					}
				}
				if(hit.collider.gameObject.name == "Questionnaire")
				{
					Application.OpenURL("https://www.surveymonkey.com/s/YGJK6PS");
				}
			}
		}

	}
}
