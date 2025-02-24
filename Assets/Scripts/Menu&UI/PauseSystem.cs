using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    #region Fields

    public static PauseSystem instance;
    private PlayerInput pInput;
    public bool inactive = false, mOpen;

    public static Action<bool> PauseMenuActive = delegate { };

    #endregion



    #region Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        SceneManager.sceneLoaded += NewScene;
    }



    private void OnDisable()
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



    private void PauseMenuOpen(InputAction.CallbackContext c)
    {
        PauseMenu();
    }



    public void PauseMenu()
    {
        if (!inactive)
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



    public void PauseInactive(bool b)
    {
        inactive = b;
    }



    private void NewScene(Scene scene, LoadSceneMode mode)
    {
        OnDisable();
        OnEnable();
    }



    private void Death()
    {
        PauseInactive(true);
    }

    #endregion
}