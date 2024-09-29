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

    private void Awake()
    {
        myEventSystem = FindAnyObjectByType<EventSystem>();

        if (GamepadActive && myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
    }



    private void OnEnable()
    {
        myEventSystem = FindAnyObjectByType<EventSystem>();

        if (GamepadActive && myEventSystem)
        {
                myEventSystem.SetSelectedGameObject(null);
                myEventSystem.SetSelectedGameObject(SelectedButton);
        }

        InputDeviceTracker.ControllerConnected += GamepadAdded;
    }



    private void OnDisable()
    {
        if (myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
        }
    }



    private void GamepadAdded(bool b)
    {
        GamepadActive = b;
        if (b && myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
    }

    #endregion
}