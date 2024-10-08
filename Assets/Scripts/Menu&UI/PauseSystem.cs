using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    #region Fields

    private static PauseSystem instance;
    private EventSystem eSystem;
    private PlayerInput pInput;
    private bool mOpen, inActive = false;

    public static Action<bool> PauseMenuActive = delegate { };

    #endregion



    #region Methods

    private void Awake()
    {
        //Ensures only one instance is active in scene at all times.
        //DDOL to preserve states
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }



    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Menu.performed += PauseMenuOpen;
        MenuSystem.FreezeTime += FreezeTime;
        HealthSystem.GameOver += Death;
        SceneInitializer.PauseSystemInactive += PauseInactive;
        MenuSystem.Resume += PauseMenu;
    }



    private void Start()
    {
        eSystem = FindFirstObjectByType<EventSystem>();
    }



    public void PauseMenu()
    {
        if (!inActive)
        {
            if (!mOpen)
            {
                FreezeTime(true);
                mOpen = true;
                PauseMenuActive(true);
            }
            else
            {
                FreezeTime(false);
                mOpen = false;
                PauseMenuActive(false);
            }
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
        FreezeTime(false);
        mOpen = false;
        PauseMenuActive(false);
    }



    private void PauseInactive(bool b)
    {
        inActive = b;
    }



    private void Death()
    {
        PauseInactive(true);
    }



    private void OnDisable()
    {
        pInput.Disable();
        pInput.Player.Menu.performed -= PauseMenuOpen;
        MenuSystem.FreezeTime -= FreezeTime;
        HealthSystem.GameOver -= Death;
        SceneInitializer.PauseSystemInactive -= PauseInactive;
        MenuSystem.Resume -= PauseMenu;
    }

    #endregion
}