using UnityEngine;

/// <summary>
/// Responsible for locking/unlocking and hiding Cursor
/// </summary>
public class CursorState : MonoBehaviour
{
    #region Methods

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion
}