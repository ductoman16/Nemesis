using UnityEngine;
using System.Collections;

/// <summary>
/// 	Controls the movement of a ship using player input.
/// </summary>
public class PlayerPilot : MonoBehaviour
{
	private Rigidbody shipRigidbody;
	private ShipEngine engine;
	private float thrusterInput;
	private float strafeInput;
	private float turnInput;
	private float pitchInput;
	private bool descendInput;
	private bool ascendInput;

	/// <summary>
	/// 	The look sensitivity for moving the mouse side-to-side.
	/// </summary>
	public int sensitivityX;
	/// <summary>
	/// 	The look sensitivity for moving the mouse up and down.
	/// </summary>
	public int sensitivityY;

	/// <summary>
	/// 	The current Y rotation.
	/// </summary>
	private float rotationY = 0F;
	
	void Awake ()
	{
		shipRigidbody = GetComponent<Rigidbody> ();
		engine = GetComponent<ShipEngine> ();
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
		thrusterInput = Input.GetAxis ("Vertical");
		strafeInput = Input.GetAxis ("Horizontal");

		turnInput = Input.GetAxis ("Mouse X");
		pitchInput = Input.GetAxis ("Mouse Y");

		descendInput = Input.GetKey (KeyCode.LeftShift);
		ascendInput = Input.GetKey (KeyCode.Space);
	}

	void FixedUpdate ()
	{
		doMovement ();
	}

	/// <summary>
	/// 	Checks for inputs that would move the player, and
	/// 	applies the movement.
	/// </summary>
	private void doMovement ()
	{
		//Strafe and thrust
		shipRigidbody.AddRelativeForce (
			strafeInput * engine.strafeSpeed, 
			(descendInput ? -engine.ascendSpeed : 0) + (ascendInput ? engine.ascendSpeed : 0), 
			thrusterInput * engine.thrustSpeed);
		
		//Mouse look 
		//		shipRigidbody.AddRelativeTorque (pitchInput * -pitchSpeed, turnInput * turnSpeed, 0f);
		
		//From MouseLook.cs //TODO: Smooth this
		float rotationX = transform.localEulerAngles.y + turnInput * sensitivityX;
		
		rotationY += pitchInput * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, -engine.maxPitchDegrees, engine.maxPitchDegrees);
		
		transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
	}
}
