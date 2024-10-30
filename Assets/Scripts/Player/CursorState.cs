using UnityEngine;

/// <summary>
/// Responsible for locking/unlocking and hiding Cursor
/// </summary>
public class CursorState : MonoBehaviour
{
    #region Fields

    public static CursorState instance;
    private bool controller = false, menuActive = false;

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
        PauseSystem.PauseMenuActive += MenuActive;
        InputDeviceTracker.ControllerConnected += GamepadToggle;
        MenuSystem.MenuActive += MenuActive;
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= MenuActive;
        InputDeviceTracker.ControllerConnected -= GamepadToggle;
        MenuSystem.MenuActive -= MenuActive;
    }



    public void CursorVisible(bool b)
    {
        if (b && !controller)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }



    private void GamepadToggle(bool b)
    {
        controller = b;
        if (menuActive)
        {
            CursorVisible(!b);
        }
        else
        {
            CursorVisible(false);
        }
    }



    private void MenuActive(bool b)
    {
        menuActive = b;
        if (!controller)
        {
            CursorVisible(b);
        }
        else
        {
            CursorVisible(false);
        }
    }

    #endregion
}