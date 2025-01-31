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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += NewScene;
        InputDeviceTracker.ControllerConnected += GamepadAdded;
        Initialization();
    }



    private void Start()
    {
        Initialization();
    }



    private void Update()
    {
        if (GamepadActive && myEventSystem.currentSelectedGameObject == null)
        {
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
        Initialization();
    }



    /// <summary>
    /// Initilization needed every time the script is enabled.
    /// </summary>
    private void Initialization()
    {
        myEventSystem = FindAnyObjectByType<EventSystem>();
        GamepadActive = InputDeviceTracker.gamepadActive;

        if (GamepadActive && myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(null);
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
    }

    #endregion
}