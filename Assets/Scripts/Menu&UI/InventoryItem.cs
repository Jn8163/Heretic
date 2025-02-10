using UnityEngine;

public abstract class InventoryItem : MonoBehaviour
{
    public GameObject itemPrefab;
    private bool canUseItem = true;
    public virtual void Action()
    {

    }
}