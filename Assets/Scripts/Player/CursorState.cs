using UnityEngine;

/// <summary>
/// Responsible for locking/unlocking and hiding Cursor
/// </summary>
public class CursorState : MonoBehaviour
{
    #region Fields

    [SerializeField] private bool VisibleOnStart;
    private bool controller = false, cursorActive = false;

    #endregion



    #region Methods



    private void OnEnable()
    {
        PauseSystem.PauseMenuActive += CursorVisible;
        InputDeviceTracker.ControllerConnected += GamepadToggle;
        MenuSystem.MenuActive += CursorVisible;
    }



    private void Start()
    {
        if (!VisibleOnStart || controller)
        {
            CursorVisible(false);
        }
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= CursorVisible;
        InputDeviceTracker.ControllerConnected -= GamepadToggle;
        MenuSystem.MenuActive -= CursorVisible;
    }



    private void CursorVisible(bool b)
    {
        Debug.Log("Cursor: " + b);
        if (b && !controller)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorActive = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cursorActive = false;
        }
    }



    private void GamepadToggle(bool b)
    {
        controller = b;
        if (cursorActive)
        {
            CursorVisible(!b);
        }
    }

    #endregion
}