using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {

	public string st_LevelName;
	public GameObject go_ResetText;

	// Use this for initialization
	void Start () {

		st_LevelName = Application.loadedLevelName;

	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp("r"))
		{
			Application.LoadLevel(st_LevelName);
		}
	
	}

	void OnMouseDown()
	{

		ValueManager VM_Script = GameObject.Find("ValueManager").GetComponent<ValueManager>();
		LapManager LM_Script = GameObject.Find ("Track").GetComponent<LapManager> ();

		LM_Script.NumofCheckpoints = 0;
		for(int i = 0; i < LM_Script.CheckpointHit.Length; i++)
		{
			LM_Script.CheckpointHit[i] = false;
		}
		VM_Script.EndRace();
		VM_Script.ResetLapTimes();
		StopCoroutine(VM_Script.TM_Script.StartCrash());
		VM_Script.TM_Script.GameOverText.enabled = false;
		VM_Script.TM_Script.bl_Crashed = false;
		LM_Script.CongratsText.enabled = false;


	}
}
