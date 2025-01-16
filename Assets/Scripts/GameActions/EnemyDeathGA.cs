using System.Collections;
using UnityEngine;
using System;

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
        // make sure to track stat for each enemy death
        StatTracker.killCount++;
        Debug.Log(StatTracker.killCount);
        // Destroy(gameObject);
        anim.SetBool("Dead", true);
        GetComponentInChildren<EnemyAudioCalls>().Die();
        StartCoroutine(nameof(DestroyEnemy));
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(50f/60f);
        Destroy(gameObject);
    }
}
