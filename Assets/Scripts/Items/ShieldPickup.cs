using System.Collections;
using UnityEngine;

public class ShieldPickup : ImmediatePickup
{
    //10 is the value in OG Heretic
    [SerializeField] private int shieldAmount = 100;



    private void OnTriggerEnter(Collider other)
    {
        if (ArmorSystem.instance)
        {
            PickupItem();
        }
    }



    protected override void PickupItem()
    {
        base.PickupItem();
        if (ArmorSystem.instance.CreateShield(shieldAmount))
        {
            StartCoroutine(DestroyPickup());
        }
    }



    protected override IEnumerator DestroyPickup()
    {
        return base.DestroyPickup();
    }
}