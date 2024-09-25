using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject menu;
    private EventSystem eSystem;
    private PlayerInput pInput;
    private bool mOpen;

    public static Action<bool> PauseMenuActive = delegate { };

    #endregion



    #region Methods

    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        pInput.Player.Menu.performed += PauseMenuOpen;
    }



    private void Start()
    {
        Debug.Log("pause");
        eSystem = FindFirstObjectByType<EventSystem>();
    }



    public void PauseMenu()
    {
        if (!mOpen)
        {
            FreezeTime(true);
            mOpen = true;
            PauseMenuActive(true);
        }
        else if (menu.activeInHierarchy)
        {
            FreezeTime(false);
            mOpen = false;
            PauseMenuActive(false);
        }
    }

    private void FreezeTime(bool b)
    {
        if (b)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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