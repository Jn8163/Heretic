using System;
using System.Collections;
using UnityEngine;

public class EW_AmmoPickupGA : GameAction
{
	[SerializeField]
	private ElvenWand elvenWand;

	[SerializeField] private GameObject mesh, sprite;
	[SerializeField] private AudioSource audioSource;

	public int ammo_amount = 1;

    public override void Action()
    {
		elvenWand.current_ammo += ammo_amount;
		audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		audioSource.Play();
		StartCoroutine(DestroyPickup());
    }

    IEnumerator DestroyPickup()
	{
		this.GetComponent<Collider>().enabled = false;
		//mesh.SetActive(false);
		mesh.GetComponent<MeshRenderer>().enabled = false;
		sprite.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}
}
