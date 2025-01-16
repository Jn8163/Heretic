using System.Collections;
using UnityEngine;

public class AmmoPickup : ImmediatePickup
{
    [SerializeField] private Ammo ammoType;
    [SerializeField] private int ammoAmount;
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
        base.PickupItem();
        ammoSystem.UpdateAmmo(ammoType, ammoAmount, true);
    }



    protected override IEnumerator DestroyPickup()
    {
        return base.DestroyPickup();
    }
}