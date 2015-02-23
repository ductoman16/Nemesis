using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
	public int carryWeight = 30;
	public int maxHealth = 100;
	public Weapon primaryWeapon;
	public Weapon secondaryWeapon;
	private int currentHealth;
	private bool fireInput1;
	private bool fireInput2;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		fireInput1 = Input.GetButton ("Fire1");
		fireInput2 = Input.GetButton ("Fire2");
	}

	void FixedUpdate ()
	{
		if (fireInput1) {
			primaryWeapon.Shoot ();
		}
		if (fireInput2) {
			secondaryWeapon.Shoot ();
		}
	}
}
