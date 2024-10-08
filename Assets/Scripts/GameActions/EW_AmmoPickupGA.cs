using System;
using System.Collections;
using UnityEngine;

public class EW_AmmoPickupGA : GameAction
{
	[SerializeField]
	private ElvenWand elvenWand;

	public int ammo_amount = 1;

    public override void Action()
    {
		elvenWand.current_ammo += ammo_amount;
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
