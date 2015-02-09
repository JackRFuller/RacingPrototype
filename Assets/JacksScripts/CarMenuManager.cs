using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarMenuManager : MonoBehaviour {

	public CarManager CM_Script;
	public GameObject AllStarSlog;
	public GameObject Figureof8;

	// Use this for initialization
	void Start () {

		CM_Script = GameObject.Find ("CarManager").GetComponent<CarManager> ();
		AllStarSlog = GameObject.Find ("AllStarSlog");
		Figureof8 = GameObject.Find ("Figureof8");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		gameObject.GetComponent<Text> ().color = Color.green;
		GameObject[] CarNames = GameObject.FindGameObjectsWithTag ("Car");
		foreach(GameObject Item in CarNames)
		{
			if(Item.name != gameObject.name)
			{
				Item.GetComponent<Text>().color = Color.white;
			}
		}
		GameObject PlayButton = GameObject.Find ("Play");
		AllStarSlog.collider.enabled = true;
		AllStarSlog.GetComponent<Text> ().enabled = true;

		Figureof8.collider.enabled = true;
		Figureof8.GetComponent<Text> ().enabled = true;

		CM_Script.SetValues (gameObject.name);
	}
}
