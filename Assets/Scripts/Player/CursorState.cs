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
        HealthSystem.GameOver += EnableCursor;
    }



    private void Start()
    {
        if (!VisibleOnStart)
        {
            CursorVisible(false);
        }
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= CursorVisible;
        HealthSystem.GameOver -= EnableCursor;
    }



    private void CursorVisible(bool b)
    {
        if (b)
        {
            Debug.Log("visable");
            Cursor.visible = true;
        }
        else
        {
            Debug.Log("Invisable");

            Cursor.visible = false;
        }
    }



    private void EnableCursor()
    {
        CursorVisible(true);
    }

    #endregion
}