using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject selected;

    private void Awake()
    {
        DeSelect();
    }

    public void Select()
    {
        selected.SetActive(true);
    }

    public void DeSelect()
    {
        selected.SetActive(false);
    }

    public bool IsSelected()
    {
        return selected.activeInHierarchy;
    }
}
