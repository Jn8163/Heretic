using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Health_PickupGA : GameAction
{
	[SerializeField]
	private HealthSystem healthSystem;

	[SerializeField] private GameObject mesh, sprite;

	public int heal_amount = 20;
    public override void Action()
    {
		healthSystem.UpdateHealth(heal_amount);
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
