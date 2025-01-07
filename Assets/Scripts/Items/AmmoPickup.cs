using System.Collections;
using UnityEngine;

public class AmmoPickup : ImmediatePickup
{
    [SerializeField] private Ammo ammoType;
    [SerializeField] private int ammoAmount;
    [SerializeField] private GameObject mesh, sprite;
    private AmmoSystem ammoSystem;



    private void OnEnable()
    {
        ammoSystem = FindAnyObjectByType<AmmoSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!ammoSystem.CheckIfFull(ammoType))
            {
                PickupItem();
            }
        }
    }



    protected override void PickupItem()
    {
        ammoSystem.UpdateAmmo(ammoType, ammoAmount, true);
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