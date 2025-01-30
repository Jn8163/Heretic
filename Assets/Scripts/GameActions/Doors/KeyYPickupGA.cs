using System;
using System.Collections;
using UnityEngine;

public class KeyYPickupGA : GameAction
{
	public static Action<bool> KeyYPickup = delegate { };

	[SerializeField] private GameObject mesh, sprite;
	[SerializeField] private AudioSource aSource;

	public override void Action()
	{
		aSource.Play();
		KeyYPickup(true);
		StartCoroutine(DestroyPickup());
	}

	IEnumerator DestroyPickup()
	{
		GetComponent<Collider>().enabled = false;
		mesh.GetComponent<MeshRenderer>().enabled = false;
		sprite.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}
}
