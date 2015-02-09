using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetBehaviour : MonoBehaviour {

	private ConeManager CM_Script;
	public GameObject go_RightSide;

	// Use this for initialization
	void Start () {

		CM_Script = GameObject.Find("LevelManager").GetComponent<ConeManager>();
		go_RightSide = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Reenage()
	{
		yield return new WaitForSeconds(1.5F);
		go_RightSide.collider.enabled = true;
	}

	void OnTriggerEnter(Collider cc_Hit)
	{
		if(cc_Hit.gameObject.tag == "Car")
		{
			if(gameObject.name == "RightSide")
			{
				CM_Script.AddPoints();
			}
			if(gameObject.name == "WrongSide")
			{
				go_RightSide.collider.enabled = false;
			}
		}
	}

	void OnTriggerExit(Collider cc_hit)
	{
		if(cc_hit.gameObject.tag == "Car")
		{
			if(gameObject.name == "WrongSide")
			{
				StartCoroutine(Reenage());
			}
		}
	}
}
