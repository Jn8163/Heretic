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
        HealthSystem.GameOver += Disable;
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= Toggle;
        HealthSystem.GameOver -= Disable;
    }



    private void Toggle(bool b)
    {
        target.SetActive(!b);
    }



    private void Disable()
    {
        Toggle(true);
    }

    #endregion
}