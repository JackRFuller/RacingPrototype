using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour {

	public float speed = 0;
	private Slider SpeedSlider;

	// Use this for initialization
	void Start () {

		SpeedSlider = GameObject.Find ("Slider").GetComponent<Slider> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(SpeedSlider.value);
		speed = SpeedSlider.value;
	
	}

	public void ValueUpdate()
	{

	}
}
