using System;
using System.Collections;
using UnityEngine;

public class EW_AmmoPickupGA : GameAction
{
	[SerializeField]
	private ElvenWand elvenWand;

	[SerializeField] private GameObject mesh, sprite;
	[SerializeField] private AudioSource audioSource;

	public int ammo_amount;
    private AmmoSystem ammoSystem;

    private void Start()
    {
        GameObject ammoObject = GameObject.Find("Player");
        if (ammoObject != null)
        {
            ammoSystem = ammoObject.GetComponent<AmmoSystem>();
        }
    }

    public override void Action()
    {
		if (ammoSystem.ElvenWandAmmo != ammoSystem.ElvenWandAmmoMax)
		{
			ammoSystem.ElvenWandAmmo += ammo_amount;
			ammoSystem.ElvenWandAmmo = Mathf.Clamp(ammoSystem.ElvenWandAmmo, 0, ammoSystem.ElvenWandAmmoMax);
			audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
			audioSource.Play();
			StartCoroutine(DestroyPickup());
		}
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
