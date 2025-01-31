using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// System for keeping track of and using items. Any Inventory action should
/// go through this system to ensure proper trcaking.
/// </summary>
public class InventorySystem : MonoBehaviour
{
    #region Fields

    public static InventorySystem instance;

    [Header("Inventory Sections & Variables")]
    [SerializeField] private float VisibleDuration = 3f;
    [SerializeField] private GameObject fInventory;
    [SerializeField] private SelectedItem previewSlot;
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private int[] itemAmounts;
    [SerializeField] private GameObject[] itemList;
    [SerializeField] private TextMeshProUGUI itemCount;

    private PlayerInput pInput;
    private int selectedSlot = 0;

    #endregion



    #region Methods

    #region UnityMethods

    private void Awake()
    {
        instance = this;
        
        //Event sub is done during awake to prevent any potential duplicate subscription issues in OnEnable
        SceneManager.sceneLoaded += NewSceneLoaded;
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

    #endregion



    /// <summary>
    /// For using currently selected item. 
    /// Can call from any script to use current item.
    /// </summary>
    public void UseSelectedItem()
    {
        int itemslot = previewSlot.UseItem();
        if(itemslot >= 0 && itemslot < inventorySlots.Length)
        { 
            if (itemAmounts[itemslot] == 1)
            {
				itemAmounts[itemslot]--;
				UpdateItemCountUI(itemAmounts[itemslot]);
				previewSlot.DestroyItem();
				Destroy(inventorySlots[itemslot].GetComponentInChildren<InventoryItem>().gameObject);
                itemList[itemslot] = null;
			}
            else if (itemAmounts[itemslot] > 1)
            {
				itemAmounts[itemslot]--;
				UpdateItemCountUI(itemAmounts[itemslot]);
			}
            
        }
    }

    public void UpdateItemCountUI(int amt)
    {
        if (amt <= 1)
        {
            itemCount.text = "";
        }
        else
        {
			itemCount.text = amt.ToString();
		}
    }

    private void OnDestroy()
    {
        //Only unsubscribes during OnDestroy because the initial sub only happens in awake
        SceneManager.sceneLoaded -= NewSceneLoaded;
    }



    /// <summary>
    /// Checks if inventory slot is available for adding an item
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns>bool indicating if the item was used</returns>
    public bool AddItem(GameObject prefab)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
            if (prefab == itemList[i])
            {
                if (itemAmounts[i] >= 9) // Can only hold up to 9 of one item in the original game
                    return false;
                itemAmounts[i]++;
                ChangeSelectedSlot(i);
                return true;
            }
            if (!slotItem)
            {
                Instantiate(prefab, slot.transform);
                itemAmounts[i]++;
                itemList[i] = prefab;
                ChangeSelectedSlot(i);
                return true;
            }
        }
        return false;
    }



    /// <summary>
    /// Changes selected slot based on parameter provided.
    /// Only changes inventory slots one at a time.
    /// </summary>
    /// <param name="increment"></param>
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



    /// <summary>
    /// Updates preview slot and inventoryslots selected function to
    /// properly show which item is selected.
    /// </summary>
    /// <param name="currentSlot"></param>
    private void ChangeSelectedSlot(int currentSlot)
    {
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            inventorySlots[selectedSlot].DeSelect();
        }

        selectedSlot = currentSlot;
        ChangePreviewSlot(selectedSlot);
        inventorySlots[selectedSlot].Select();
        UpdateItemCountUI(itemAmounts[selectedSlot]);
    }



    /// <summary>
    /// Change preview slot to currently selected slot.
    /// </summary>
    public void UpdatePreviewSlot()
    {
        ChangePreviewSlot(selectedSlot);
    }



    /// <summary>
    /// Updates all parameters of the preview slot.
    /// </summary>
    /// <param name="slot"></param>
    private void ChangePreviewSlot(int slot)
    {
        InventoryItem item = inventorySlots[slot].GetComponentInChildren<InventoryItem>();
        if (item)
        {
            previewSlot.itemSelected = true;
            previewSlot.slot = slot;
            previewSlot.ChangeItem(item.itemPrefab);
            UpdateItemCountUI(itemAmounts[slot]);
        }
        else
        {
            ClearPreviewSlot();
        }
    }



    /// <summary>
    /// Clears any values out of preview slot.
    /// </summary>
    private void ClearPreviewSlot()
    {
        previewSlot.itemSelected = false;
        previewSlot.slot = -1;
        previewSlot.DestroyItem();
    }



    /// <summary>
    /// Method subscribed to player input to enable inventory after detected input.
    /// Inventory disables after 'VisibleDuration' seconds (as it does in OG Heretic)
    /// </summary>
    /// <param name="c"></param>
    private void ShowFullInventory(InputAction.CallbackContext c)
    {
        StopAllCoroutines();
        StartCoroutine(nameof(DeactivateInventory));

        if (!PauseSystem.instance.mOpen) {
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



    /// <summary>
    /// Uses Selected Item on PlayerInput
    /// </summary>
    /// <param name="c"></param>
    private void UsePreviewedItem(InputAction.CallbackContext c)
    {
        UseSelectedItem();
    }



    /// <summary>
    /// Refreshes event subscriptions when new scene is loaded.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void NewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnDisable();
        OnEnable();
    }



    /// <summary>
    /// Keeps Inventory Open for duration of time (OG Heretic mimic)
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeactivateInventory()
    {
        fInventory.SetActive(true);
        yield return new WaitForSeconds(VisibleDuration);
        fInventory.SetActive(false);
    }

    #endregion
}