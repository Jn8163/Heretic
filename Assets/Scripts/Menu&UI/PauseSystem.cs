using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    #region Fields

    public static PauseSystem instance;
    private PlayerInput pInput;
    private bool mOpen, destroy = false;
    public bool isActive = false;

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
        else if(instance != this)
        {
            destroy = true;
            gameObject.SetActive(false);
        }
    }



    private void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Menu.performed += PauseMenuOpen;
        MenuSystem.FreezeTime += FreezeTime;
        HealthSystem.GameOver += Death;
        MenuSystem.Resume += PauseMenu;
    }



    public void PauseMenu()
    {
        if (!isActive)
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



    public void PauseInactive(bool b)
    {
        isActive = b;
    }



    private void Death()
    {
        PauseInactive(true);
    }



    private void OnDisable()
    {
        if (!destroy)
        {
            FreezeTime(false);
            mOpen = false;
            PauseMenuActive(false);

            pInput.Disable();
            pInput.Player.Menu.performed -= PauseMenuOpen;
            MenuSystem.FreezeTime -= FreezeTime;
            HealthSystem.GameOver -= Death;
            MenuSystem.Resume -= PauseMenu;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
}