using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Tracks current weapon and swaps weapons as needed
/// </summary>
public class SwapWeapon : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private List<bool> weaponAquired = new List<bool>(){ false, false, false, false, false, false };
    [SerializeField] private List<bool> hasAmmo = new List<bool>() { true, false, false, false, false, false };
    /// <summary>
    /// Shows array index of current weapon. Ex: 0 through 5
    /// </summary>
    private int currentWeapon = 0;



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
    public void EnableWeapon(int weaponSlot)
    {
        if(weaponSlot >= 0 && weaponSlot < weapons.Count - 1)
        {
            weaponAquired[weaponSlot] = true;
            hasAmmo[weaponSlot] = true;
        }
    }



    /// <summary>
    /// Updates the weaponSlot to track if there is ammo
    /// </summary>
    /// <param name="hasAmmo"></param>
    /// <param name="weaponSlot"></param>
    public void UpdateAmmo(bool hasAmmo, int weaponSlot)
    {
        this.hasAmmo[weaponSlot] = hasAmmo;

        if (!hasAmmo)
        {
            for(int counter = this.hasAmmo.Count - 1; counter >= 0; counter--)
            {
                if(this.hasAmmo[counter])
                {
                    SwapToWeapon(counter);
                    return;
                }
            }
        }
    }



    /// <summary>
    /// Disables all weapons
    /// </summary>
    private void UnequipAll()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }



    /// <summary>
    /// Function that swaps the weapon to the provided weaponSlot
    /// </summary>
    /// <param name="weaponSlot"></param>
    private void SwapToWeapon(int weaponSlot)
    {
        Debug.Log("Change Weapon to slot #" + weaponSlot);

        //ensure the weaponslot # is part of the array
        if(weaponSlot < 0)
        {
            weaponSlot = weapons.Count - 1;
        }
        else if(weaponSlot > weapons.Count - 1)
        {
            weaponSlot = 0;
        }


        if (weaponAquired[weaponSlot])
        {
            UnequipAll();
            currentWeapon = weaponSlot;
            weapons[weaponSlot].SetActive(true);
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