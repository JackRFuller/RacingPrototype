using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceCoin : MonoBehaviour {

	public int MaxNumOfCoins;

	public GameObject CoinPrefab;
	public int CoinPointer;

	List<GameObject> Coins = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(CoinPointer < MaxNumOfCoins){
		}
	
	}
}
