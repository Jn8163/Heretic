using System;
using System.Collections;
using UnityEngine;

public class KeyYPickupGA : GameAction
{
	public static Action<bool> KeyYPickup = delegate { };

	public override void Action()
	{
		KeyYPickup(true);
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
