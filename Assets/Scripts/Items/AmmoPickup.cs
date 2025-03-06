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
        if (MenuSystem.instance && MenuSystem.instance.selectedDifficulty == 1 || MenuSystem.instance.selectedDifficulty == 5)
        {
            ammoAmount += ammoAmount / 2;
        }
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