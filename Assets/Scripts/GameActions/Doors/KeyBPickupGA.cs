using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class KeyBPickupGA : GameAction
{
	public static Action<bool> KeyBPickup = delegate { };

	[SerializeField] private GameObject mesh, sprite;
	[SerializeField] private AudioSource aSource;
	public override void Action()
	{
		aSource.Play();
		KeyBPickup(true);
		StartCoroutine(DestroyPickup());
	}

	IEnumerator DestroyPickup()
	{
		this.GetComponent<Collider>().enabled = false;
		mesh.GetComponent<MeshRenderer>().enabled = false;
		sprite.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}
}
