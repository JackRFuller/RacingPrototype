using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteeringWheel : MonoBehaviour {

	public float fl_MaximumAngle; //Maximum angle the wheel can steer
	public float maximumAngle = 500f; // Maximum angle the steering wheel can rotate
	public float wheelSize = 256f; // Wheel's width (and height) as pixel
	public Vector2 deltaPivot = Vector2.zero; // If wheel not rotates around its center, this variable allows tweaking the pivot point
	public float wheelFreeSpeed= 200f; // Degrees per second the wheel rotates when released
	public Texture2D wheelTexture; // Wheel texture
	
	private float wheelAngle; // Wheel's angle in degrees
	
	private bool wheelBeingHeld; // Whether or not the steering wheel is being held
	private Rect wheelPosition; // Wheel's position on screen
	private Vector2 wheelCenter; // Wheel's center on screen coordinates (not Rect coordinates)
	private float wheelTempAngle; // A necessary variable
	private int touchId = -1; // The finger holding the wheel

	private CarController CC_Script;
	
	void Start()
	{
		// Initialize variables and calculate wheel's position on screen
		wheelBeingHeld = false;
		wheelPosition = new Rect( Screen.width - wheelSize - 75, Screen.height - wheelSize - 75, wheelSize, wheelSize );
		wheelCenter = new Vector2( wheelPosition.x + wheelPosition.width * 0.5f, Screen.height - wheelPosition.y - wheelPosition.height * 0.5f );
		wheelAngle = 0f;

		CC_Script = GameObject.Find("Car").GetComponent<CarController>();
	}
	
	// Returns the angle of the steering wheel in degrees. Can be used to rotate a car etc.
	public float GetAngle()
	{
		return wheelAngle;
	}
	
	void Update()
	{

#if UNITY_EDITOR
				
		// If the wheel is currently being held
		if( wheelBeingHeld )
		{
			Vector2 mousePosition;
			
			// Find the mouse position on screen
			mousePosition = Input.mousePosition;


			
			float wheelNewAngle = Vector2.Angle( Vector2.up, mousePosition - wheelCenter );
			
			// If mouse is very close to the steering wheel's center, do nothing
			if( Vector2.Distance( mousePosition, wheelCenter ) > 20f )
			{
				if( mousePosition.x > wheelCenter.x )
					wheelAngle -= wheelNewAngle - wheelTempAngle;
				else
					wheelAngle += wheelNewAngle - wheelTempAngle;
			}
			
			// Make sure that the wheelAngle does not exceed the maximumAngle
			if( wheelAngle > maximumAngle )
				wheelAngle = maximumAngle;
			else if( wheelAngle < -maximumAngle )
				wheelAngle = -maximumAngle;
			
			wheelTempAngle = wheelNewAngle;
			
			// If user releases the mouse, release the wheel
			if( Input.GetMouseButtonUp( 0 ) )
				wheelBeingHeld = false;
		}
		else // If wheel is not being held
		{
			// If user clicks on the wheel, update the status
			if( Input.GetMouseButtonDown(0) &&  wheelPosition.Contains( new Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y ) ) )
			{
				wheelBeingHeld = true;
				Vector2 TempValue = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
				wheelTempAngle = Vector2.Angle(Vector2.up, TempValue - wheelCenter);
			}
			
			// If the wheel is rotated and not being held, rotate it to its default angle (zero)
			if( !Mathf.Approximately( 0f, wheelAngle ) )
			{
				float deltaAngle = wheelFreeSpeed * Time.deltaTime;
				
				if( Mathf.Abs( deltaAngle ) > Mathf.Abs( wheelAngle ) )
				{
					wheelAngle = 0f;
					return;
				}
				
				if( wheelAngle > 0f )
					wheelAngle -= deltaAngle;
				else
					wheelAngle += deltaAngle;
			}
		}

#endif

#if UNITY_IOS



		// If the wheel is currently being held
		if( wheelBeingHeld )
		{
			Vector2 touchPosition = new Vector2(0,0);
			
			// Find the finger position on screen
			foreach(Touch t in Input.touches)
			{
				if(t.fingerId == touchId)
				{
					touchPosition = t.position;
					
					// If finger exists no more, release the wheel
					if( t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled )
					{
						wheelBeingHeld = false;
					}
				}
			}
			
			float wheelNewAngle = Vector2.Angle(Vector2.up, touchPosition - wheelCenter );
			
			// If touch is very close to the steering wheel's center, do nothing
			if( Vector2.Distance( touchPosition, wheelCenter ) > 20f )
			{
				if( touchPosition.x > wheelCenter.x )
					wheelAngle -= wheelNewAngle - wheelTempAngle;
				else
					wheelAngle += wheelNewAngle - wheelTempAngle;
			}
			
			// Make sure that the wheelAngle does not exceed the maximumAngle
			if( wheelAngle > maximumAngle )
				wheelAngle = maximumAngle;
			else if( wheelAngle < -maximumAngle )
				wheelAngle = -maximumAngle;
			
			wheelTempAngle = wheelNewAngle;
		}
		else // If wheel is not being held
		{
			// If a finger touches the wheel, update the status
			foreach(Touch t in Input.touches )
			{
				if( t.phase == TouchPhase.Began )
				{
					if( wheelPosition.Contains( new Vector2( t.position.x, Screen.height - t.position.y ) ) )
					{
						wheelBeingHeld = true;
						wheelTempAngle = Vector2.Angle( Vector2.up, t.position - wheelCenter );
						touchId = t.fingerId;
					}
				}
			}
			
			// If the wheel is rotated and not being held, rotate it to its default angle (zero)
			if( !Mathf.Approximately( 0f, wheelAngle ) )
			{
				float deltaAngle = wheelFreeSpeed * Time.deltaTime;
				
				if( Mathf.Abs( deltaAngle ) > Mathf.Abs( wheelAngle ) )
				{
					wheelAngle = 0f;
					return;
				}
				
				if( wheelAngle > 0f )
					wheelAngle -= deltaAngle;
				else
					wheelAngle += deltaAngle;
			}
		}
	

#endif
		float fl_TempAngle = wheelAngle / 700;
		CC_Script.Move(-fl_TempAngle, 1);

	}
	
	// Draw the steering wheel on screen
	void OnGUI()
	{
		// Uncomment the line below to see the bounds of the wheel
		// GUI.Box( wheelPosition, "" );
		
		Matrix4x4 theMatrix = GUI.matrix;
		GUIUtility.RotateAroundPivot( -wheelAngle, wheelPosition.center + deltaPivot );
		GUI.DrawTexture( wheelPosition, wheelTexture );
		GUI.matrix = theMatrix;
	}

}
