using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public GameObject inventoryitem;
    public bool itemSelected = false;
    public int slot = -1;



    public void ChangeItem(GameObject newItem)
    {
        DestroyItem();

        Instantiate(newItem, transform);
    }



    public int UseItem()
    {
        if (itemSelected)
        {
            transform.GetComponentInChildren<InventoryItem>().Action();
            DestroyItem();
        }
        return slot;
    }



    public void DestroyItem()
    {
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        if (item)
        {
            Destroy(item.gameObject);
        }
    }
}