using UnityEngine;

public class Ripper : MonoBehaviour
{
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private int damageLow, damageHigh;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 0)
        {
            Instantiate(hitPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            if (other.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.UpdateHealth(-Random.Range(damageLow, damageHigh + 1));
            }
        }
    }
}
