using System;
using System.Collections;
using UnityEngine;

public class KeyGPickupGA : GameAction
{
	public static Action<bool> KeyGPickup = delegate { };

	public override void Action()
	{
		KeyGPickup(true);
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
