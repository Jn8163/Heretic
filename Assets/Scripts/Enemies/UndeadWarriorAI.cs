using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UndeadWarriorAI : EnemyBaseClass
{
    [Header("Undead Warrior Settings")]
    [SerializeField] private GameObject axePrefab; // The projectile axe
    [SerializeField] private Transform throwPoint; // Where the axe is thrown from
    [SerializeField] private float axeSpeed = 15f; // Speed of the thrown axe
    [SerializeField] private float rangedAttackCooldown = 3f;
    [SerializeField] private Animator anim;

    private bool canThrowAxe = true;

    protected override void Update()
    {
        base.Update();

        if (isChasing)
        {
            if (playerInAttackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    protected override void AttackPlayer()
    {
        // If the player is close, do a melee attack
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(MeleeAttack());
        }
        // If far, throw an axe
        else if (canThrowAxe)
        {
            StartCoroutine(RangedAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        agent.isStopped = true;
        anim.SetTrigger("MeleeAttack");

        yield return new WaitForSeconds(1f); // Attack wind-up

        if (playerInAttackRange)
        {
            player.GetComponent<HealthSystem>().UpdateHealth(-10); // Deal damage
        }

        yield return new WaitForSeconds(0.5f); // Attack recovery
        agent.isStopped = false;
    }

    IEnumerator RangedAttack()
    {
        canThrowAxe = false;
        agent.isStopped = true;
        anim.SetTrigger("ThrowAxe");

        yield return new WaitForSeconds(0.8f); // Throw wind-up

        // Create and throw the axe
        GameObject axe = Instantiate(axePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = axe.GetComponent<Rigidbody>();
        rb.linearVelocity = (player.position - throwPoint.position).normalized * axeSpeed;

        yield return new WaitForSeconds(0.5f); // Throw recovery
        agent.isStopped = false;

        yield return new WaitForSeconds(rangedAttackCooldown); // Cooldown before throwing again
        canThrowAxe = true;
    }

    protected override void ChasePlayer()
    {
        base.ChasePlayer();
    }
}
