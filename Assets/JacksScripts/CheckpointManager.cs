using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	public int CheckpointID;
	private LapManager LM_Script;

	// Use this for initialization
	void Start () {

		LM_Script = GameObject.Find ("Track").GetComponent<LapManager> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Car")
		{
			if(CheckpointID != 0)
			{
				if(LM_Script.CheckpointHit[CheckpointID - 1])
				{
					LM_Script.CheckpointHit[CheckpointID] = true;
				}
			}
			else
			{
				LM_Script.CheckpointHit[CheckpointID] = true;
			}

		}
	}


}
