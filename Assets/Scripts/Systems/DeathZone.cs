using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthSystem hSystem))
        {
            hSystem.UpdateHealth(-hSystem.currentHealth);
        }
    }
}
