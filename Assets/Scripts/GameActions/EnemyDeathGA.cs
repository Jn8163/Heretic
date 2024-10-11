using UnityEngine;

public class EnemyDeathGA : MonoBehaviour
{
    [SerializeField]
    private HealthSystem healthSystem;

    private void OnEnable()
    {
        healthSystem.EnemyDeath += KillEnemy;
    }
    private void OnDisable()
    {
        healthSystem.EnemyDeath -= KillEnemy;
    }
    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
