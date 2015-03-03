using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public GameObject ammo;
	public Transform weaponMount;
	public float fireForce = 10;
	public float fireRate = .25f;
	private float nextFire;

	public void Shoot ()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			var shot = Instantiate (ammo, weaponMount.position, weaponMount.rotation) as GameObject;
			var r = shot.GetComponent<Rigidbody>();
			r.velocity = this.rigidbody.velocity;
			r.AddRelativeForce (Vector3.up * fireForce);
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

}
