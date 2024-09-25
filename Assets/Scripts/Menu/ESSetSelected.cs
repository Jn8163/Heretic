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
        myEventSystem = FindAnyObjectByType<EventSystem>();
        myEventSystem.SetSelectedGameObject(null);
        myEventSystem.SetSelectedGameObject(SelectedButton);
        //Debug.Log(myEventSystem.currentSelectedGameObject);
    }



    private void OnEnable()
    {
        myEventSystem = FindAnyObjectByType<EventSystem>();
        myEventSystem.SetSelectedGameObject(null);
        if (myEventSystem)
        {
            myEventSystem.SetSelectedGameObject(SelectedButton);
        }
        //Debug.Log(myEventSystem.currentSelectedGameObject);
    }



    private void OnDisable()
    {
        myEventSystem.SetSelectedGameObject(null);
    }

    #endregion
}