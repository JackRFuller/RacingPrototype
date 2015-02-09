using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrackManager : MonoBehaviour {

	private MainMenuManager MMM_Script;

	// Use this for initialization
	void Start () {

		MMM_Script = GameObject.Find ("Play").GetComponent<MainMenuManager> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		gameObject.GetComponent<Text> ().color = Color.green;
		GameObject[] CarNames = GameObject.FindGameObjectsWithTag ("Button");
		foreach(GameObject Item in CarNames)
		{
			if(Item.name != gameObject.name)
			{
				Item.GetComponent<Text>().color = Color.white;
			}
		}
		if(gameObject.name == "AllStarSlog")
		{
			MMM_Script.LevelToLoad = "TestTrack";
		}
		if(gameObject.name == "Figureof8")
		{
			MMM_Script.LevelToLoad = "BigTrack";
		}
		GameObject PlayButton = GameObject.Find ("Play");
		PlayButton.GetComponent<Text> ().enabled = true;
		PlayButton.collider.enabled = true;
	}
}
