using UnityEngine;
using System.Collections;

public class ShipEngine : MonoBehaviour
{	
	public int speed;
	public int turnSpeed;
	public int strafeSpeed;
	public int pitchSpeed;
	public float rightingMultiplier = 0.1f;

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
	private float rotationY = 0F;

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
		//Strafe and thrust
		shipRigidbody.AddRelativeForce (strafeInput * strafeSpeed, 0, thrusterInput * speed);

		//Mouse look 
//		shipRigidbody.AddRelativeTorque (pitchInput * -pitchSpeed, turnInput * turnSpeed, 0f);

		//From MouseLook.cs //TODO: Smooth this
		float rotationX = transform.localEulerAngles.y + turnInput * sensitivityX;
		
		rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, -maxPitchDegrees, maxPitchDegrees);
		
		transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);


		//Get current rotations in small angles
		float localX = makeAngleSmall (transform.localEulerAngles.x);
		float localZ = makeAngleSmall (transform.localEulerAngles.z);

		//Apply forces to keep the ship upright (0X, 0Z rotation is upright)
		float rightingZ = localZ * -rightingMultiplier;
		float rightingX = localX * -rightingMultiplier;

		shipRigidbody.AddRelativeTorque (0, 0, rightingZ);
		shipRigidbody.AddRelativeTorque (rightingX, 0, 0);
		

//		Make sure rotation is within limits
		var clampedX = Mathf.Clamp (localX, -maxPitchDegrees, maxPitchDegrees);
		var clampedZ = Mathf.Clamp (localZ, -maxRollDegrees, maxRollDegrees);
//		Debug.Log ("rotateX: " + localX);
//		Debug.Log ("clampedX: " + clampedX);
		transform.localEulerAngles = new Vector3 (clampedX, transform.localEulerAngles.y, transform.localEulerAngles.z);
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








