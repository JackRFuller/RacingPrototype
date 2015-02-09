using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MechanicManager : MonoBehaviour {

	public Text[] TX_GUI_Items;
	public bool bl_BoostActive;
	public bool bl_BrakeActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_IOS

		if(Input.touchCount > 0)
		{
			MouseClick();
		}
#endif
	
	}

	void MouseClick()
	{
		Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		if(Physics.Raycast(_ray, out hit))
		{
			if(hit.collider.gameObject.name == "Brake")
			{
				if(bl_BrakeActive)
				{
					TX_GUI_Items[4].color = Color.white;
					TX_GUI_Items[1].enabled = false;
					TX_GUI_Items[0].enabled = false;
					TX_GUI_Items[2].enabled = false;
				}
				else
				{

				}

				TX_GUI_Items[4].color = Color.green;
				TX_GUI_Items[3].color = Color.white;
				TX_GUI_Items[1].enabled = true;
				TX_GUI_Items[0].enabled = false;
				TX_GUI_Items[2].enabled = true;
			}
			if(hit.collider.gameObject.name == "Boost")
			{
				TX_GUI_Items[4].color = Color.white;
				TX_GUI_Items[3].color = Color.green;
				TX_GUI_Items[1].enabled = false;
				TX_GUI_Items[0].enabled = true;
				TX_GUI_Items[2].enabled = true;
			}
		}
	}


}
