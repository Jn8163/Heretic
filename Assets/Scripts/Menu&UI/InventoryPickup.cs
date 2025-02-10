using System.Collections;
using UnityEngine;

public class InventoryPickup : Item
{
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] private GameObject mesh, sprite;
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
        if(inventorySystem.AddItem(pickupPrefab))
        {
			base.PickupItem();
			StartCoroutine(nameof(DestroyPickup));
        }
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