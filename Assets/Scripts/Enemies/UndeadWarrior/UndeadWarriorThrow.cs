using System.Collections;
using UnityEngine;

public class UndeadWarriorThrow : EnemyAttackClass
{
    [SerializeField] private GameObject throwPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Animator animMesh, animSprite;

    private bool attacked;
    private Transform player; //  Store player reference

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform; //  Get player reference
        if (player == null)
        {
            Debug.LogError("UndeadWarriorThrow: Player not found in scene!");
        }
    }

    public void TriggerThrowAttack()
    {
        if (!attacked)
        {
            Debug.Log("Undead Warrior is throwing!"); //  Debug Log
            StartCoroutine(nameof(ThrowAttackRoutine));
        }
    }

    private IEnumerator ThrowAttackRoutine()
    {
        if (player == null) yield break; //  Prevent null reference error

        attacked = true;
        animMesh.SetBool("isAttacking", true);
        animSprite.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.5f); // Throw animation delay

        if (throwPrefab == null)
        {
            Debug.LogError("UndeadWarriorThrow: No throwPrefab assigned!");
            yield break;
        }

        if (throwPoint == null)
        {
            Debug.LogError("UndeadWarriorThrow: No throwPoint assigned!");
            yield break;
        }

        // ✅ Create and throw the projectile
        GameObject projectile = Instantiate(throwPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = (player.position - throwPoint.position).normalized * throwForce;
        }
        else
        {
            Debug.LogError("UndeadWarriorThrow: ThrowPrefab missing Rigidbody!");
        }

        animMesh.SetBool("isAttacking", false);
        animSprite.SetBool("isAttacking", false);

        yield return new WaitForSeconds(attackCooldown);
        attacked = false;
    }
}
