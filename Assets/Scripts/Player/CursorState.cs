using UnityEngine;

/// <summary>
/// Responsible for locking/unlocking and hiding Cursor
/// </summary>
public class CursorState : MonoBehaviour
{
    #region Fields

    [SerializeField] private bool VisibleOnStart;
    private bool controller = false;

    #endregion



    #region Methods



    private void OnEnable()
    {
        PauseSystem.PauseMenuActive += CursorVisible;
        InputDeviceTracker.ControllerConnected += GamepadToggle;
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
    }



    private void CursorVisible(bool b)
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
        Debug.Log("Cursor " + b);
        controller = b;
        CursorVisible(!b);
    }

    #endregion
}