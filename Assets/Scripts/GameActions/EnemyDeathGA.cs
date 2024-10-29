using System.Collections;
using UnityEngine;

public class EnemyDeathGA : MonoBehaviour
{
    [SerializeField]
    private HealthSystem healthSystem;
    [SerializeField]
    private Animator anim;

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
        // Destroy(gameObject);
        anim.SetBool("Dead", true);
        StartCoroutine(nameof(DestroyEnemy));
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(50f/60f);
        Destroy(gameObject);
    }
}
