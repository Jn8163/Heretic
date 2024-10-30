using System.Collections;
using UnityEngine;

public abstract class ImmediatePickup : Item
{
    protected override void PickupItem()
    {
    }



    protected override IEnumerator DestroyPickup()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}