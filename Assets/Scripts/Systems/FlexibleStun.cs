using UnityEngine;

public class FlexibleStun : MonoBehaviour
{
    [SerializeField] private int stunValue = 1;
    [SerializeField] private int knockbackValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StunSystem stunSystem))
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            stunSystem.TryStun(stunValue, knockbackValue, hitDirection);
        }
    }
}
