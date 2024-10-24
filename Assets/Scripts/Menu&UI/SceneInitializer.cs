using System;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    #region Fields

    [SerializeField] private bool cursorVisible = false, pausingInactive = false, playerHUD = false;
    [SerializeField] private string menuToActivate;

    public static Action<bool> PauseSystemInactive = delegate { };
    public static Action<string> MenuActiveOnStart = delegate { };
    public static Action<bool> PlayerHUDActive = delegate { };
    public static Action<bool> MusicSystemDeactivate = delegate { };

    #endregion



    #region Methods

    private void Start()
    {
        CursorState.instance.CursorVisible(cursorVisible);
        PauseSystem.instance.PauseInactive(pausingInactive);
        MenuSystem.instance.PlayerHUDActive(playerHUD);
        
        if (menuToActivate != "")
        {
            MenuSystem.instance.SwitchMenu(menuToActivate);
        }
    }

    #endregion
}
