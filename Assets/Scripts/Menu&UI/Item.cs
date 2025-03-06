using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    protected InventorySystem inventorySystem;
    [SerializeField] protected float destroyDelay = 1.0f;



    protected virtual void Start()
    {
        inventorySystem = InventorySystem.instance;
    }



    protected virtual void PickupItem()
    {
        transform.parent.GetComponent<Spawn>().targetActive = false;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }



    protected virtual IEnumerator DestroyPickup()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}