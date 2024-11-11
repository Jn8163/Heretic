using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    #region Fields

    public static PauseSystem instance;
    private PlayerInput pInput;
    private bool destroy = false;
    public bool inactive = false, mOpen;

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
            inactive = false;
            mOpen = false;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            destroy = true;
            Destroy(gameObject);
        }
    }



    private void OnEnable()
    {
        if (!destroy)
        {
            pInput = new PlayerInput();
            pInput.Enable();

            pInput.Player.Menu.performed += PauseMenuOpen;
            MenuSystem.FreezeTime += FreezeTime;
            HealthSystem.GameOver += Death;
            MenuSystem.Resume += PauseMenu;
            SceneManager.sceneLoaded += NewScene;
        }
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



    private void Restart()
    {
        FreezeTime(false);
        mOpen = false;
    }



    private void Death()
    {
        PauseInactive(true);
    }

    #endregion
}