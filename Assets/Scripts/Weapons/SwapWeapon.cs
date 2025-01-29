using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Tracks current weapon and swaps weapons as needed
/// </summary>
public class SwapWeapon : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField] private int startingSlot = 1;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private List<bool> weaponAquired = new List<bool>(){ false, true, false, false, false, false, false};
    /// <summary>
    /// Shows array index of current weapon. Ex: 0 through 5
    /// </summary>
    private int currentWeapon = 1;



    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.SwitchWeapon.performed += SwitchWeapon;
        pInput.Player.Weapon1.performed += SwapToWeapon1;
        pInput.Player.Weapon2.performed += SwapToWeapon2;
        pInput.Player.Weapon3.performed += SwapToWeapon3;
        pInput.Player.Weapon4.performed += SwapToWeapon4;
        pInput.Player.Weapon5.performed += SwapToWeapon5;
        pInput.Player.Weapon6.performed += SwapToWeapon6;
    }



    private void Start()
    {
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }

        weapons[startingSlot].SetActive(true);
    }



    private void OnDisable()
    {
        pInput.Player.SwitchWeapon.performed -= SwitchWeapon;
        pInput.Player.Weapon1.performed -= SwapToWeapon1;
        pInput.Player.Weapon2.performed -= SwapToWeapon2;
        pInput.Player.Weapon3.performed -= SwapToWeapon3;
        pInput.Player.Weapon4.performed -= SwapToWeapon4;
        pInput.Player.Weapon5.performed -= SwapToWeapon5;
        pInput.Player.Weapon6.performed -= SwapToWeapon6;

        pInput.Disable();
    }



    /// <summary>
    /// Tracks that a weapon has been obtained for provided weaponSlot.
    /// weaponSlot must be within 0 to 5
    /// </summary>
    /// <param name="weaponSlot"></param>
    public bool EnableWeapon(int weaponSlot)
    {
        if(weaponSlot >= 0 && weaponSlot < weaponAquired.Count)
        {
            if (!weaponAquired[weaponSlot])
            {
                weaponAquired[weaponSlot] = true;

                if(weaponSlot == 6)
                {
                    weapons[0].GetComponent<ToggleStaff>().gauntletUnlocked = true;
                }

                return true;
            }
        }
        return false;
    }



    /// <summary>
    /// Disables all weapons
    /// </summary>
    private void UnequipAll()
    {
        for(int weaponSlot = 0; weaponSlot < weapons.Count; weaponSlot++)
        {
            if (weaponSlot != currentWeapon)
            {
                weapons[weaponSlot].SetActive(false);
            }
            else
            {
                Debug.Log("Don't disable");
            }
        }
    }



    /// <summary>
    /// Function that swaps the weapon to the provided weaponSlot
    /// </summary>
    /// <param name="weaponSlot"></param>
    private void SwapToWeapon(int weaponSlot)
    {
        if (Time.timeScale == 1)
        {
            Debug.Log("Change Weapon to slot #" + weaponSlot);

            //ensure the weaponslot # is part of the array
            if (weaponSlot < 0)
            {
                weaponSlot = weapons.Count - 1;
            }
            else if (weaponSlot > weapons.Count - 1)
            {
                weaponSlot = 0;
            }


            if (weaponAquired[weaponSlot])
            {
                currentWeapon = weaponSlot;
                UnequipAll();
                weapons[weaponSlot].SetActive(true);
            }
        }
    }



    #region WeaponInputFunctions

    private void SwitchWeapon(InputAction.CallbackContext c)
    {
        if (!PauseSystem.instance.mOpen)
        {
            float input = pInput.Player.SwitchWeapon.ReadValue<float>();

            if (input < 0)
            {
                SwapToWeapon(currentWeapon - 1);
            }
            else
            {
                SwapToWeapon(currentWeapon + 1);
            }
        }
    }



    private void SwapToWeapon1(InputAction.CallbackContext c)
    {
        SwapToWeapon(0);
    }



    private void SwapToWeapon2(InputAction.CallbackContext c)
    {
        SwapToWeapon(1);
    }



    private void SwapToWeapon3(InputAction.CallbackContext c)
    {
        SwapToWeapon(2);
    }



    private void SwapToWeapon4(InputAction.CallbackContext c)
    {
        SwapToWeapon(3);
    }



    private void SwapToWeapon5(InputAction.CallbackContext c)
    {
        SwapToWeapon(4);
    }



    private void SwapToWeapon6(InputAction.CallbackContext c)
    {
        SwapToWeapon(5);
    }

    #endregion

}