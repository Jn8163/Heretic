using System;
using System.Collections;
using UnityEngine;

public class Item_PickupGA : GameAction
{
    public override void Action()
    {

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
