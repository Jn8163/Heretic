using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwapWeapon : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField] private InputActionReference Weapon1, Weapon2, Weapon3;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
    }

    private void OnEnable()
    {
        Weapon1.action.performed += SwapToWeapon1;
        Weapon2.action.performed += SwapToWeapon2; 
    }

    private void OnDisable()
    {
        Weapon1.action.performed -= SwapToWeapon1;
        Weapon2.action.performed -= SwapToWeapon2;
    }

    private void SwapToWeapon1(InputAction.CallbackContext obj)
    {
        UnequipAll();
        weapons[0].SetActive(true);
    }
    private void SwapToWeapon2(InputAction.CallbackContext obj)
    {
        UnequipAll();
        weapons[1].SetActive(true);
    }

    private void UnequipAll()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
