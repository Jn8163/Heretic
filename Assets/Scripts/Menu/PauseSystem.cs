using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject menu;
    private EventSystem eSystem;
    private InputSystem pInput;
    private bool mOpen;

    public static Action<bool> PauseMenuActive = delegate { };

    #endregion



    #region Methods

    private void Start()
    {
        pInput = new InputSystem();
        pInput.Enable();
        pInput.Player.Menu.performed += PauseMenuOpen;
        eSystem = FindFirstObjectByType<EventSystem>();
    }



    public void PauseMenu()
    {
        if (!mOpen)
        {
            Time.timeScale = 0;
            mOpen = true;
            PauseMenuActive(true);
        }
        else if (menu.activeInHierarchy)
        {
            Time.timeScale = 1;
            mOpen = false;
            PauseMenuActive(false);
        }
    }



    private void PauseMenuOpen(InputAction.CallbackContext c)
    {
        PauseMenu();
    }



    private void Restart()
    {
        Time.timeScale = 1;
        mOpen = false;
        PauseMenuActive(false);
    }



    private void OnDisable()
    {
        pInput.Disable();
        pInput.Player.Menu.performed -= PauseMenuOpen;
    }

    #endregion
}