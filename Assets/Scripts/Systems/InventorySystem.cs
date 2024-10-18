using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//add deactivation of inventory if pause menu is opened.
public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private Image previewSlot;
    [SerializeField] private GameObject inventoryItemPrefab;

    [Header("Input Detection")]
    [SerializeField] private GameObject fInventory;
    [SerializeField] private float VisibleDuration = 3f;

    private PlayerInput pInput;
    private int selectedSlot = -1;
    

    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.InventorySelection.performed += ShowFullInventory;
        PickupTrigger.PickUpObject += AddItem;
    }

    private void OnDisable()
    {
        PickupTrigger.PickUpObject -= AddItem;
        pInput.Player.InventorySelection.performed -= ShowFullInventory;
        pInput.Disable();
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
        if (slotItem)
        {
            Item itemType = slotItem.item;
            if (use)
            {
                Destroy(slotItem.gameObject);
            }
            return itemType;
        }
        return null;
    }

    private void ChangeSelectedSlot(int increment)
    {
        if(selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            inventorySlots[selectedSlot].DeSelect();
        }

        selectedSlot += increment;

        if(selectedSlot < 0)
        {
            selectedSlot = inventorySlots.Length - 1;
        }else if(selectedSlot >= inventorySlots.Length)
        {
            selectedSlot = 0;
        }

        ChangePreviewSlot(selectedSlot);
        inventorySlots[selectedSlot].Select();
    }

    private void ChangePreviewSlot(int slot)
    {
        InventoryItem item = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
        if (item)
        {
            previewSlot.sprite = item.gameObject.GetComponent<Image>().sprite;
            previewSlot.color = item.gameObject.GetComponent<Image>().color;
        }
        else
        {
            previewSlot.sprite = null;
            previewSlot.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void AddItem(Item newItem, GameObject go)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
            if (!slotItem)
            {
                SpawnNewItem(newItem, slot);
                go.SetActive(false);
                return;
            }
        }
    }

    private void SpawnNewItem(Item newItem, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(newItem);
    }

    //Method subscribed to player input to enable inventory after detected input.
    //Inventory disables after 'VisibleDuration' seconds (as it does in og Heretic)
    private void ShowFullInventory(InputAction.CallbackContext c)
    {
        StopAllCoroutines();
        StartCoroutine(nameof(DeactivateInventory));

        float input = pInput.Player.InventorySelection.ReadValue<float>();

        if(input < 0)
        {
            ChangeSelectedSlot(-1);
        }
        else
        {
            ChangeSelectedSlot(1);
        }
    }

    private IEnumerator DeactivateInventory()
    {
        fInventory.SetActive(true);
        yield return new WaitForSeconds(VisibleDuration);
        fInventory.SetActive(false);
    }
}