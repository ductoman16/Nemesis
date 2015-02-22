using UnityEngine;
using System.Collections;

public class ShipMovementController : MonoBehaviour
{	
//	public float tilt;
	public int speed;
	public int turnSpeed;
	public int strafeSpeed;
	public int pitchSpeed;

	/// <summary>
	/// 	The look sensitivity for moving the mouse side-to-side.
	/// </summary>
	public int sensitivityX;
	/// <summary>
	/// 	The look sensitivity for moving the mouse up and down.
	/// </summary>
	public int sensitivityY;

	/// <summary>
	/// 	The maximum pitch of the ship, in degrees.
	/// </summary>
	public int maxPitchDegrees;

	/// <summary>
	/// 	The maximum roll of the ship, in degrees.
	/// </summary>
	public int maxRollDegrees;
	private Rigidbody shipRigidbody;
	private float thrusterInput;
	private float strafeInput;
	private float turnInput;
	private float pitchInput;

	void Awake ()
	{
		shipRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		thrusterInput = Input.GetAxis ("Vertical");
		strafeInput = Input.GetAxis ("Horizontal");

		turnInput = Input.GetAxis ("Mouse X");
		pitchInput = Input.GetAxis ("Mouse Y");
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
//		float moveY = 0;
//		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
//			moveY -= 1;
//		if (Input.GetKey (KeyCode.Space))
//			moveY += 1;
//
		//Local velocity
//		Vector3 locVel = new Vector3 (moveX, moveY, moveZ);
//		//Transform the local velocity to world velocity (rigidbody velocity is relative to the world)
//		rigidbody.velocity = transform.TransformDirection (locVel * shipController.speed);

		shipRigidbody.AddRelativeForce (strafeInput * strafeSpeed, 0, thrusterInput * speed);
		shipRigidbody.AddRelativeTorque (pitchInput * -pitchSpeed, turnInput * turnSpeed, 0f);

		//Mouse look rotation
//		float rotateX = Input.GetAxis ("Mouse Y"); //Rotating around the X axis moves the ship up and down.
//		float rotateY = Input.GetAxis ("Mouse X"); //Rotating around the Y axis moves the ship side to side.
		
		//Rotate based on look input
//		transform.Rotate (new Vector3 (rotateX * -sensitivityY, rotateY * sensitivityX, 0));


		//Get rotations in small angles
		float localX = makeAngleSmall (transform.localEulerAngles.x);
		float localZ = makeAngleSmall (transform.localEulerAngles.z);

		//Make sure rotation is within limits
		var clampedX = Mathf.Clamp (localX, -maxPitchDegrees, maxPitchDegrees);
		var clampedZ = Mathf.Clamp (localZ, -maxRollDegrees, maxRollDegrees);
//		Debug.Log ("rotateX: " + localX);
//		Debug.Log ("clampedX: " + clampedX);
		transform.localEulerAngles = new Vector3 (clampedX, transform.localEulerAngles.y, clampedZ);
	}

	/// <summary>
	/// 	Normalizes the angle between -180 and 180.
	/// </summary>
	/// <param name="angle">Angle.</param>
	private float makeAngleSmall (float angle)
	{
		var small = angle % 360;
		if (small > 180)
			small -= 360;
		return small;
	}
}








