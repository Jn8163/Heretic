using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Item_PickupGA : GameAction
{
	[SerializeField] private GameObject mesh, sprite;
    public override void Action()
    {

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
