using System;
using System.Collections;
using UnityEngine;

public class Health_PickupGA : GameAction
{
	[SerializeField]
	private HealthSystem healthSystem;
	public int heal_amount = 20;
    public override void Action()
    {
		healthSystem.UpdateHealth(heal_amount);
		StartCoroutine(DestroyPickup());
    }

    IEnumerator DestroyPickup()
	{
		this.GetComponent<Collider>().enabled = false;
		this.GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}
}
