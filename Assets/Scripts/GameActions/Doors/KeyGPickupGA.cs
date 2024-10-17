using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class KeyGPickupGA : GameAction
{
	public static Action<bool> KeyGPickup = delegate { };

	[SerializeField] private GameObject mesh, sprite;

	public override void Action()
	{
		KeyGPickup(true);
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
