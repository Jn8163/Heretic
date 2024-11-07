using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthSystem hSystem))
        {
            hSystem.UpdateHealth(-hSystem.currentHealth);
        }
    }
}
