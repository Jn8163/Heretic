using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        //Event sub is done during awake to prevent any potential duplicate subscription issues in OnEnable
        SceneManager.sceneLoaded += NewScene;
    }



    private void OnEnable()
    {
        InputDeviceTracker.ControllerConnected += GamepadAdded;
        Initialization();
    }



    private void Start()
    {
        Initialization();
    }



    private void OnDisable()
    {
        if (myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
        }

        InputDeviceTracker.ControllerConnected -= GamepadAdded;
    }



    private void OnDestroy()
    {
        //Only unsubscribes during OnDestroy because the initial sub only happens in awake
        SceneManager.sceneLoaded -= NewScene;
    }



    private void GamepadAdded(bool b)
    {
        GamepadActive = b;

        if (myEventSystem)
        {
            if (b)
            {
                myEventSystem.SetSelectedGameObject(SelectedButton);
            }
            else
            {
                myEventSystem.SetSelectedGameObject(null);
            }
        }
    }



    private void NewScene(Scene scene, LoadSceneMode mode)
    {
        OnDisable();
        OnEnable();
        Initialization();
    }



    /// <summary>
    /// Initilization needed every time the script is enabled.
    /// </summary>
    private void Initialization()
    {
        myEventSystem = FindAnyObjectByType<EventSystem>();
        GamepadActive = InputDeviceTracker.gamepadConnected;

        if (GamepadActive && myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
    }

    #endregion
}