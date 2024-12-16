using System.Collections;
using UnityEngine;

public class AmmoPickup : ImmediatePickup
{
    [SerializeField] private Ammo ammoType;
    [SerializeField] private int ammoAmount;
    [SerializeField] private GameObject mesh, sprite;
    private GameObject targetWeapon, ElvenWand;



    private void OnEnable()
    {
        ElvenWand = FindAnyObjectByType<ElvenWand>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckWeapons();
        }
    }





    private void CheckWeapons()
    {
        switch (ammoType)
        {
            case Ammo.wand:
                targetWeapon = ElvenWand;
                break;
        }

        if (targetWeapon)
        {
            PickupItem();
        }
    }



    protected override void PickupItem()
    {
        targetWeapon.GetComponent<RangedWeapon>().UpdateAmmo(ammoType, ammoAmount);
        StartCoroutine(nameof(DestroyPickup));
    }



    protected override IEnumerator DestroyPickup()
    {
        GetComponent<Collider>().enabled = false;
        mesh.GetComponent<MeshRenderer>().enabled = false;
        sprite.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}