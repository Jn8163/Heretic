using System.Collections;
using UnityEngine;

public class WeaponPickup : ImmediatePickup
{
    [SerializeField] private int WeaponSlot;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			bool weaponAdded = FindAnyObjectByType<SwapWeapon>().EnableWeapon(WeaponSlot);
			Debug.Log(weaponAdded);
			if (weaponAdded)
			{
				PickupItem();
			}
		}
    }



    protected override void PickupItem()
    {
        base.PickupItem();
    }



    protected override IEnumerator DestroyPickup()
    {
        return base.DestroyPickup();
    }
}
