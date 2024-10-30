using System.Collections;
using UnityEngine;

public class InventoryPickup : Item
{
    [SerializeField] GameObject pickupPrefab;
    protected override void Start()
    {
        base.Start();    
    }



    private void OnTriggerEnter(Collider other)
    {
        PickupItem();
    }



    protected override void PickupItem()
    {
        base.PickupItem();
        if(inventorySystem.AddItem(pickupPrefab))
        {
            StartCoroutine(nameof(DestroyPickup));
        }
    }

    protected override IEnumerator DestroyPickup()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}