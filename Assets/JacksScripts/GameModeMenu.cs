using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameModeMenu : MonoBehaviour {

	private ValueManager VM_Script;
	public Text[] GUI_Items;
	private GameModeManager GMM_Script;
	private GameObject GameModeMan;

	private bool bl_NewMovement;

	// Use this for initialization
	void Start () {

		GameModeMan = GameObject.Find("GM_Manager");
		GMM_Script = GameModeMan.GetComponent<GameModeManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonUp(0))
		{
			MenuMouseInput();

		}

		MenuInput();	
	}

	void MenuMouseInput()
	{
				
		Ray _ray = gameObject.camera.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		if(Physics.Raycast(_ray, out hit))
		{	

			if(hit.collider.gameObject.name == "AllStar")
			{
				GUI_Items[5].color = Color.green;
//				GUI_Items[2].color = Color.white;
//				GUI_Items[1].color = Color.white;

				GUI_Items[0].enabled = true;
				GUI_Items[0].collider.enabled = true;

				GUI_Items[4].enabled = false;
				GUI_Items[3].enabled = false;
				GUI_Items[6].enabled = true;

				bl_NewMovement = true;

			}
			if(hit.collider.gameObject.name == "RightWay")
			{
				GUI_Items[0].enabled = true;
				GUI_Items[0].collider.enabled = true;
				
				GUI_Items[2].color = Color.green;
				GUI_Items[1].color = Color.white;
				GUI_Items[5].color = Color.white;
				
				GUI_Items[4].enabled = true;
				GUI_Items[3].enabled = false;
				
				GMM_Script.st_GameMode = "Right Way";
				bl_NewMovement = false;
			}
			if(hit.collider.gameObject.name == "PlayButton")
			{
				if(bl_NewMovement)
				{
					Application.LoadLevel("TestScene");
				}
				else
				{
					Application.LoadLevel("PointsScene");
					DontDestroyOnLoad(GameModeMan);
				}

				
			}

		}

	}

	void MenuInput()
	{
		if(Input.touchCount > 0)
		{			
			Ray _ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			
			RaycastHit hit;
			
			if(Physics.Raycast(_ray, out hit))
			{	

				if(hit.collider.gameObject.name == "AllStar")
				{
					GUI_Items[5].color = Color.green;
//					GUI_Items[2].color = Color.white;
//					GUI_Items[1].color = Color.white;
					
					GUI_Items[0].enabled = true;
					GUI_Items[0].collider.enabled = true;
					
					GUI_Items[4].enabled = false;
					GUI_Items[3].enabled = false;
					GUI_Items[6].enabled = true;
					
					bl_NewMovement = true;
					
				}
				if(hit.collider.gameObject.name == "RightWay")
				{
					GUI_Items[0].enabled = true;
					GUI_Items[0].collider.enabled = true;
					
					GUI_Items[2].color = Color.green;
					GUI_Items[1].color = Color.white;
					GUI_Items[5].color = Color.white;
					
					GUI_Items[4].enabled = true;
					GUI_Items[3].enabled = false;
					GUI_Items[6].enabled = false;
					
					GMM_Script.st_GameMode = "Right Way";
					bl_NewMovement = false;
				}
				if(hit.collider.gameObject.name == "PlayButton")
				{
					if(bl_NewMovement)
					{
						Application.LoadLevel("TestScene");
					}
					else
					{
						Application.LoadLevel("PointsScene");
						DontDestroyOnLoad(GameModeMan);
					}
					
					
				}
		}
	}
}
}
