using UnityEngine;

public class FlexibleHP : MonoBehaviour
{
    #region Fields

    [SerializeField] private int flexhealth = 5;

    #endregion



    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.UpdateHealth(flexhealth);
        }
    }

    #endregion
}