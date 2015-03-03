using UnityEngine;
using System.Collections;

/// <summary>
/// 	Controls the movement capabilities of a ship.
/// </summary>
public class ShipEngine : MonoBehaviour
{	
	/// <summary>
	/// 	The speed that the ship moves forward and back.
	/// </summary>
	public float thrustSpeed;

	/// <summary>
	/// 	The speed that the ship can turn.
	/// </summary>
	public float turnSpeed;

	/// <summary>
	/// 	The speed that the ship moves side-to-side.
	/// </summary>
	public float strafeSpeed;

//	public float pitchSpeed;

	/// <summary>
	/// 	Force multiplier that the ship will use to keep itself upright.
	/// </summary>
	public float rightingMultiplier = 0.1f;

	/// <summary>
	/// 	The speed for ascending/descending
	/// </summary>
	public float ascendSpeed;

	/// <summary>
	/// 	The maximum pitch of the ship, in degrees.
	/// </summary>
	public int maxPitchDegrees;

	/// <summary>
	/// 	The maximum roll of the ship, in degrees.
	/// </summary>
	public int maxRollDegrees;
	private Rigidbody shipRigidbody;

	void Awake ()
	{
		shipRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void FixedUpdate ()
	{
		doEngines ();
	}

	private void doEngines ()
	{
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








