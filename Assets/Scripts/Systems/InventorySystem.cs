using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
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
    [SerializeField] private Image previewSlot;
    [SerializeField] private GameObject inventoryItemPrefab;

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
    public void UseSelectedItem(bool use)
    {
        int tmp = previewSlot.GetComponent<SelectedItem>().UseItem();
        if(tmp != -1)
        {
            Destroy(inventorySlots[tmp].GetComponentInChildren<InventoryItem>().gameObject);
            ClearPreviewSlot();
        }
    }



    //Checks if inventory slot is available for adding an item
    //If available- disables item and calls function to actually add item to system
    //If unavailable- does nothing
    public void AddItem(Item item, GameObject go)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Item slotItem = slot.GetComponentInChildren<Item>();
            Debug.Log("acheived");
            if (!slotItem)
            {
                SpawnNewItem(item, slot);
                go.SetActive(false);
                ChangePreviewSlot(selectedSlot);
                return;
            }
        }
    }



    //Changes selected slot based on player input
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



    //Updates preview slot on slot change
    private void ChangePreviewSlot(int slot)
    {
        InventoryItem item = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
        if (item)
        {
            previewSlot.GetComponent<SelectedItem>().item = item.item;
            previewSlot.GetComponent<SelectedItem>().itemSelected = true;
            previewSlot.GetComponent<SelectedItem>().slot = slot;

            previewSlot.sprite = item.gameObject.GetComponent<Image>().sprite;
            previewSlot.color = item.gameObject.GetComponent<Image>().color;
        }
        else
        {
            ClearPreviewSlot();
        }
    }



    //Adds item to inventory system
    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);

    }



    private void ClearPreviewSlot()
    {
        previewSlot.GetComponent<SelectedItem>().item = null;
        previewSlot.GetComponent<SelectedItem>().itemSelected = false;
        previewSlot.GetComponent<SelectedItem>().slot = -1;


        previewSlot.sprite = null;
        previewSlot.color = new Color(1f, 1f, 1f, 0f);
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
                ChangeSelectedSlot(-1);
            }
            else
            {
                ChangeSelectedSlot(1);
            }
        }
    }



    private void UsePreviewedItem(InputAction.CallbackContext c)
    {
        UseSelectedItem(true);
    }



    //Keeps Inventory Open for duration of time (OG Heretic mimic)
    private IEnumerator DeactivateInventory()
    {
        fInventory.SetActive(true);
        yield return new WaitForSeconds(VisibleDuration);
        fInventory.SetActive(false);
    }
}