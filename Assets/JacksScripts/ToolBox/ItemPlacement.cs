using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemPlacement : MonoBehaviour {



    public Text[] UI_Items;

    public GameObject CoinPrefab;
    public GameObject Corner;

    public int NumofCoins;
    public int NumofCorners;

    public int MaxNumofCoins;
    public int MaxNumofCorners;

    public Vector3 ItemSpawnPos;

    public float CurrentTimer;
    public float BestTime;
    

    List<GameObject> PlacedItems = new List<GameObject>();
    private CarController CC_Script;

    private bool bl_ResetTouch;
    private bool bl_TimeSet;

    //LevelSpecificVariables
    public bool bl_AllCoinsCollected;
    public int CollectedCoins = 0;

	private bool bl_PlaceCoin;

    


	// Use this for initialization
	void Start () {

        CC_Script = GameObject.Find("Car1").GetComponent<CarController>();

        PlacedItems.Clear();
        bl_ResetTouch = true;

        CurrentTimer = 0;
        bl_TimeSet = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (bl_AllCoinsCollected)
        {
            SetBestTimes();
        }

        if (!CC_Script.bl_CarReady)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                PlaceRacingItemsMouse();
            }
            if (Input.GetMouseButtonUp(0))
            {
                bl_ResetTouch = true;
            }
#endif

#if UNITY_IOS

            PlaceRacingItemsTouch();

            if (Input.touchCount <= 0)
            {
                bl_ResetTouch = true;
            }
#endif
        }

        if (CC_Script.bl_CarReady)
        {
            CurrentTimer += Time.deltaTime;
            UI_Items[1].text = "Current Time: " + CurrentTimer.ToString("F2");
            if (!bl_TimeSet)
            {
                UI_Items[0].text = "Best Time: --:--";
            }
        }
	}

    void SetBestTimes()
    {
        if (PlayerPrefs.GetInt("Played") == 1)
        {
            if (CurrentTimer < BestTime)
            {
                CC_Script.bl_CarReady = false;
                BestTime = CurrentTimer;
                UI_Items[0].text = "Best Time: " + BestTime.ToString("F2");
            }
        }
        if (PlayerPrefs.GetInt("Played") == 0)
        {
            CC_Script.bl_CarReady = false;
            BestTime = CurrentTimer;
            UI_Items[0].text = "Best Time: " + BestTime.ToString("F2");
            PlayerPrefs.SetInt("Played", 1);
        }
    }

    void PlaceRacingItemsTouch()
    {
        if (Input.touchCount > 0)
        {
            Ray _ray = gameObject.camera.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit))
            {
                if (hit.collider.gameObject.name == "CoinInput" && bl_ResetTouch)
                {
					if(NumofCoins < MaxNumofCoins)
					{
						bl_PlaceCoin = true;
						bl_ResetTouch = false;
						ItemSpawnPos = new Vector3(hit.point.x, 2.0F, hit.point.z);
						GameObject Coin;
						
						if(PlacedItems.Count > 0)
						{
							foreach(GameObject Coins in PlacedItems)
							{
								if(Coins.transform.position == ItemSpawnPos)
								{
									Debug.Log("Fail");
									bl_PlaceCoin = false;
								}
							}
							if(bl_PlaceCoin)
							{
								Coin = Instantiate(CoinPrefab, ItemSpawnPos, CoinPrefab.transform.rotation) as GameObject;
								PlacedItems.Add(Coin);
								NumofCoins += 1;
							}
						}
						else
						{
							Coin = Instantiate(CoinPrefab, ItemSpawnPos, CoinPrefab.transform.rotation) as GameObject;
							PlacedItems.Add(Coin);
							NumofCoins += 1;
						}
					}
                }
            }
        }
    }

    void PlaceRacingItemsMouse()
    {
        Ray _ray = gameObject.camera.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		if(Physics.Raycast(_ray, out hit))
		{
            if (hit.collider.gameObject.name == "CoinInput" && bl_ResetTouch)
            {
                if (NumofCoins < MaxNumofCoins)
                {
					bl_PlaceCoin = true;
                    bl_ResetTouch = false;
                    ItemSpawnPos = new Vector3(hit.point.x, 2.0F, hit.point.z);
                    GameObject Coin;

					if(PlacedItems.Count > 0)
					{
						foreach(GameObject Coins in PlacedItems)
						{
							if(Coins.transform.position == ItemSpawnPos)
							{
								Debug.Log("Fail");
								bl_PlaceCoin = false;
							}
						}

						if(bl_PlaceCoin)
						{
							Coin = Instantiate(CoinPrefab, ItemSpawnPos, CoinPrefab.transform.rotation) as GameObject;
							PlacedItems.Add(Coin);
							NumofCoins += 1;
						}
					}
					if(PlacedItems.Count == 0)
					{
						Coin = Instantiate(CoinPrefab, ItemSpawnPos, CoinPrefab.transform.rotation) as GameObject;
						PlacedItems.Add(Coin);
						NumofCoins += 1;
					}
					

                }
               
            }
		}

    }
}
