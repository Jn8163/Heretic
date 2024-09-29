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

    #endregion



    #region Methods

    private void Awake()
    {
        if (InputDeviceTracker.currentDevice == "gamepad")
        {
            myEventSystem = FindAnyObjectByType<EventSystem>();
            myEventSystem.SetSelectedGameObject(null);
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
    }



    private void OnEnable()
    {
        if (InputDeviceTracker.currentDevice == "gamepad")
        {
            if (myEventSystem)
            {
                myEventSystem = FindAnyObjectByType<EventSystem>();
                myEventSystem.SetSelectedGameObject(null);
                myEventSystem.SetSelectedGameObject(SelectedButton);
            }
        }
    }



    private void OnDisable()
    {
        if (myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
        }
    }

    #endregion
}