using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    #region Fields

    public GameObject inventoryitem;
    public bool itemSelected = false;
    public int slot = -1;

    #endregion



    #region Methods

    /// <summary>
    /// Updates the Item in the preview Slot,
    /// needs to be done to use item.
    /// ONLY CALL FROM INVENTORYSYSTEM SCRIPT.
    /// </summary>
    /// <param name="newItem"></param>
    public void ChangeItem(GameObject newItem)
    {
        DestroyItem();

        Instantiate(newItem, transform);
    }



    /// <summary>
    /// Finds a child that is an InventoryItem and uses it's Action, 
    /// then calls the DestroyItem function to remove the item from the preview slot.
    /// ONLY CALL FROM INVENTORYSYSTEM SCRIPT.
    /// </summary>
    /// <returns>Inventory slot number of item that was used</returns>
    public int UseItem()
    {
        if (itemSelected)
        {
            transform.GetComponentInChildren<InventoryItem>().Action();
            // DestroyItem();
        }
        return slot;
    }



    /// <summary>
    /// Clear Preview Slot without using item's action.
    /// ONLY CALL FROM INVENTORYSYSTEM SCRIPT.
    /// </summary>
    public void DestroyItem()
    {
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        if (item)
        {
            Destroy(item.gameObject);
        }
    }

    #endregion
}