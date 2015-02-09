using UnityEngine;
using System.Collections;

public class CoinBehaviour : MonoBehaviour {

	public float fl_RotRate;
	private GameObject go_CoinMesh;
	private PointsManager PM_Script;
    private ItemPlacement IP_Script;

	// Use this for initialization
	void Start () {


		go_CoinMesh = gameObject.transform.FindChild("Points").gameObject;

        if (Application.loadedLevelName == "PointsScene")
        {
            PM_Script = GameObject.Find("LevelManager").GetComponent<PointsManager>();	
        }
        if (Application.loadedLevelName == "CoinPlacement")
        {
            IP_Script = GameObject.Find("Main Camera").GetComponent<ItemPlacement>();
        }
		
	}
	
	// Update is called once per frame
	void Update () {

		RotateCoin();
	
	}

	void RotateCoin()
	{
		go_CoinMesh.transform.Rotate(fl_RotRate * Time.deltaTime,0,0);
	}

	void OnTriggerEnter(Collider cc_Hit)
	{
		if(cc_Hit.gameObject.tag == "Car")
		{
			go_CoinMesh.renderer.enabled = false;

            if (Application.loadedLevelName == "PointsScene")
            {
                PM_Script.AddOnPoints();
                PM_Script.MoveCoin();
            }

            if (Application.loadedLevelName == "CoinPlacement")
            {
                IP_Script.CollectedCoins += 1;
                if (IP_Script.CollectedCoins == IP_Script.NumofCoins)
                {
                    IP_Script.bl_AllCoinsCollected = true;
                }
            }
		}
	}
}
