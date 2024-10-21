using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EC_AmmoPickupGA : GameAction
{
	[SerializeField] private GameObject mesh, sprite;
    [SerializeField] private AudioSource audioSource;

    public int ammo_amount = 10;

    public override void Action()
    {
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.Play();
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
