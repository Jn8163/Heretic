using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Finds Event System and Updates selected object to SelectedButton.
/// </summary>
public class ESSetSelected : MonoBehaviour
{

    #region Fields

    [SerializeField] private GameObject SelectedButton;
    private EventSystem myEventSystem;
    private bool GamepadActive;

    #endregion



    #region Methods

    private void OnEnable()
    {
        InputDeviceTracker.ControllerConnected += GamepadAdded;

        myEventSystem = FindAnyObjectByType<EventSystem>();
        GamepadActive = InputDeviceTracker.gamepadConnected;

        if (GamepadActive && myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
    }



    private void OnDisable()
    {
        if (myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
        }

        InputDeviceTracker.ControllerConnected -= GamepadAdded;
    }



    private void GamepadAdded(bool b)
    {
        GamepadActive = b;
        if (b && myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
        else if(myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
        }
    }

    #endregion
}