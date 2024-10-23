using UnityEngine;

/// <summary>
/// Responsible for locking/unlocking and hiding Cursor
/// </summary>
public class CursorState : MonoBehaviour
{
    #region Fields

    public static CursorState instance;
    private bool controller = false;

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
        PauseSystem.PauseMenuActive += CursorVisible;
        InputDeviceTracker.ControllerConnected += GamepadToggle;
        MenuSystem.MenuActive += CursorVisible;
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= CursorVisible;
        InputDeviceTracker.ControllerConnected -= GamepadToggle;
        MenuSystem.MenuActive -= CursorVisible;
    }



    public void CursorVisible(bool b)
    {
        Debug.Log("Cursor: " + b);
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
        CursorVisible(!b);
    }

    #endregion
}