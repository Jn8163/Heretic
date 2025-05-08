using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;

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
#if UNITY_EDITOR
        Debug.Log(StatTracker.killCount);
#endif
        // Destroy(gameObject);
        anim.SetBool("Dead", true);
        GetComponentInChildren<EnemyAudioCalls>().Die();
        GetComponent<NavMeshAgent>().speed = 0;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<EnemyAttackClass>().enabled = false;

        StartCoroutine(nameof(DisableEnemy));
	}

    private IEnumerator DisableEnemy()
    {
        yield return new WaitForSeconds(.25f);
        gameObject.SetActive(false);
    }
}
