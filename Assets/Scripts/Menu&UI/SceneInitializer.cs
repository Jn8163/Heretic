using System;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    #region Fields

    [SerializeField] private bool cursorVisible = false, pausingInactive = false;
    [SerializeField] private string menuToActivate;

    public static Action<bool> CursorVisibleOnStart = delegate { };
    public static Action<bool> PauseSystemInactive = delegate { };
    public static Action<string> MenuActiveOnStart = delegate { };

    #endregion



    #region Methods

    private void Start()
    {
        CursorVisibleOnStart(cursorVisible);
        PauseSystemInactive(pausingInactive);

        if (menuToActivate != "")
        {
            MenuActiveOnStart(menuToActivate);
        }
    }

    #endregion
}
