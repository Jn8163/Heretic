using UnityEngine;

public class DisableOnPause : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject target;

    #endregion



    #region Methods

    private void OnEnable()
    {
        PauseSystem.PauseMenuActive += Toggle;
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= Toggle;
    }



    private void Toggle(bool b)
    {
        target.SetActive(!b);
    }

    #endregion
}