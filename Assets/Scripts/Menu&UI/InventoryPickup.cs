using UnityEngine;

public class InventoryPickup : Item
{
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
        inventorySystem.AddItem(item, gameObject);
    }
}