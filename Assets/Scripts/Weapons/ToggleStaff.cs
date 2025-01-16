using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleStaff : MonoBehaviour
{
    [SerializeField] private GameObject staff, gauntlet;
    private bool staffActive = true;
    public bool gauntletUnlocked;

    private PlayerInput pInput;



    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Weapon1.performed += Toggle;
    }



    private void OnDisable()
    {
        pInput.Disable();
        pInput.Player.Weapon1.performed -= Toggle;
    }



    /// <summary>
    /// Toggles between staff/gauntlet weapon in first weapon slot.
    /// </summary>
    /// <param name="c"></param>
    private void Toggle(InputAction.CallbackContext c)
    {
        if (gauntletUnlocked)
        {
            staffActive = !staffActive;

            staff.SetActive(staffActive);
            gauntlet.SetActive(!staffActive);
        }
    }
}