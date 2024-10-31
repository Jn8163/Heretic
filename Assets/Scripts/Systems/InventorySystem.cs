using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//Update get selected item for preview slot 
public class InventorySystem : MonoBehaviour
{
    #region Fields

    public static InventorySystem instance;

    [Header("Input Detection")]
    [SerializeField] private GameObject fInventory;
    [SerializeField] private float VisibleDuration = 3f;

    private PlayerInput pInput;
    private int selectedSlot = 0;

    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private SelectedItem previewSlot;

    #endregion



    private void Awake()
    {
        instance = this;
    }



    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.InventorySelection.performed += ShowFullInventory;
        pInput.Player.UseItem.performed += UsePreviewedItem;

        fInventory.SetActive(false);
    }



    private void OnDisable()
    {
        pInput.Player.InventorySelection.performed -= ShowFullInventory;
        pInput.Player.UseItem.performed -= UsePreviewedItem;

        pInput.Disable();
    }



    //For retreiving and using inventory items if use is set to true
    public void UseSelectedItem()
    {
        int itemslot = previewSlot.UseItem();
        if(itemslot >= 0 && itemslot < inventorySlots.Length)
        {
            Destroy(inventorySlots[itemslot].GetComponentInChildren<InventoryItem>().gameObject);
        }
    }



    //Checks if inventory slot is available for adding an item
    public bool AddItem(GameObject prefab)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
            if (!slotItem)
            {
                Instantiate(prefab, slot.transform);
                ChangeSelectedSlot(i);
                return true;
            }
        }
        return false;
    }



    //Changes selected slot based on player input
    private void IncrementSelectedSlot(int increment)
    {
        increment += selectedSlot;

        if(increment < 0)
        {
            increment = inventorySlots.Length - 1;
        }else if(increment >= inventorySlots.Length)
        {
            increment = 0;
        }



        ChangeSelectedSlot(increment);
    }



    private void ChangeSelectedSlot(int currentSlot)
    {
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            inventorySlots[selectedSlot].DeSelect();
        }

        selectedSlot = currentSlot;
        ChangePreviewSlot(selectedSlot);
        inventorySlots[selectedSlot].Select();
    }



    public void UpdatePreviewSlot()
    {
        ChangePreviewSlot(selectedSlot);
    }



    //Updates preview slot on slot change
    private void ChangePreviewSlot(int slot)
    {
        InventoryItem item = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
        if (item)
        {
            previewSlot.itemSelected = true;
            previewSlot.slot = slot;
            previewSlot.ChangeItem(item.itemPrefab);
        }
        else
        {
            ClearPreviewSlot();
        }
    }



    private void ClearPreviewSlot()
    {
        previewSlot.itemSelected = false;
        previewSlot.slot = -1;
        previewSlot.DestroyItem();
    }



    //Method subscribed to player input to enable inventory after detected input.
    //Inventory disables after 'VisibleDuration' seconds (as it does in OG Heretic)
    private void ShowFullInventory(InputAction.CallbackContext c)
    {
        StopAllCoroutines();
        StartCoroutine(nameof(DeactivateInventory));

        if (!PauseSystem.instance.isActive) {
            float input = pInput.Player.InventorySelection.ReadValue<float>();

            if (input < 0)
            {
                IncrementSelectedSlot(-1);
            }
            else
            {
                IncrementSelectedSlot(1);
            }
        }
    }



    private void UsePreviewedItem(InputAction.CallbackContext c)
    {
        UseSelectedItem();
    }



    //Keeps Inventory Open for duration of time (OG Heretic mimic)
    private IEnumerator DeactivateInventory()
    {
        fInventory.SetActive(true);
        yield return new WaitForSeconds(VisibleDuration);
        fInventory.SetActive(false);
    }
}