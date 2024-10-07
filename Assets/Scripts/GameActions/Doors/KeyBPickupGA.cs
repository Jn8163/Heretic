using System;
using System.Collections;
using UnityEngine;

public class KeyBPickupGA : GameAction
{
	public static Action<bool> KeyBPickup = delegate { };

	public override void Action()
	{
		KeyBPickup(true);
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
