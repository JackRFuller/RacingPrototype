using UnityEngine;
using System.Collections;

public class GameModeLoader : MonoBehaviour {

	private GameModeManager GM_Script;

	public GameObject LevelManager;

	private PointsManager PM_Script;
	public GameObject go_Coins;
	
	//Cone Variables
	private ConeManager CM_Script;
	public GameObject go_Cones;

	// Use this for initialization
	void Start () {

		if(Application.loadedLevelName != "CoinPlacement")
		{
			GM_Script = GameObject.Find("GM_Manager").GetComponent<GameModeManager>();
			
			if(GM_Script.st_GameMode == "Coin Hunt")
			{
				go_Coins.SetActive(true);
				PM_Script = LevelManager.GetComponent<PointsManager>();
				PM_Script.enabled = true;
			}
			if(GM_Script.st_GameMode == "Right Way")
			{
				go_Cones.SetActive(true);
				CM_Script = LevelManager.GetComponent<ConeManager>();
				CM_Script.enabled = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
