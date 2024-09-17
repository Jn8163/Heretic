using UnityEngine;

/// <summary>
/// Responsible for locking/unlocking and hiding Cursor
/// </summary>
public class CursorState : MonoBehaviour
{
    #region Fields

    [SerializeField] private bool VisibleOnStart;

    #endregion



    #region Methods

    private void OnEnable()
    {
        PauseSystem.PauseMenuActive += CursorVisible;
    }



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        if (!VisibleOnStart)
        {
            CursorVisible(false);
        }
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= CursorVisible;
    }



    private void CursorVisible(bool b)
    {
        if (b)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    #endregion
}