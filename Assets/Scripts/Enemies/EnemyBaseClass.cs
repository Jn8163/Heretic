using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class EnemyBaseClass : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    private HealthSystem healthSystem;
    protected Transform player;
    public LayerMask whatIsPlayer;

    // FOV System
    private FOV fov;

    // Patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange = 5f; // Controlled wandering distance
    public float patrolPauseTime = 3f; // Time between wander attempts
    private bool isPausing;

    // Enemy AI states
    protected Vector3 startingPos;
    public float sightRange, attackRange, chaseTimer;
    public bool playerInSightRange, playerInAttackRange, isChasing;
    public bool AgroYellDone;

    protected virtual void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<HealthSystem>();
        fov = GetComponent<FOV>(); // Get FOV component

        startingPos = transform.position;
    }

    protected virtual void Update()
    {
        if (healthSystem != null && healthSystem.bAlive)
        {
            // Check vision using FOV system
            playerInSightRange = fov.canSeePlayer;
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            // If the enemy sees the player or is attacking, reset chase state
            if (playerInSightRange || playerInAttackRange)
            {
                isChasing = true;
                chaseTimer = 0;
            }

            // If the player disappears, enemy waits for 5 sec before giving up
            if (!playerInSightRange && !playerInAttackRange && isChasing)
            {
                chaseTimer += Time.deltaTime;
                if (chaseTimer > 5)
                {
                    chaseTimer = 0;
                    isChasing = false;
                }
            }

            // Behavior priority: Attack > Chase > Return to Origin > Patrol
            if (playerInAttackRange)
            {
                AttackPlayer();
            }
            else if (isChasing)
            {
                ChasePlayer();
            }
            else
            {
                ReturnToOrigin();
                if (!isPausing)
                    StartCoroutine(PatrolPause());
            }
        }
        else if (!healthSystem.bAlive)
        {
            playerInAttackRange = false;
            playerInSightRange = false;
        }

        if (playerInSightRange && !AgroYellDone)
        {
            AgroYellDone = true;
            GetComponentInChildren<EnemyAudioCalls>().PlayYell();
        }
    }

    protected virtual void AttackPlayer()
    {
        if (agent != null)
        {
            agent.SetDestination(transform.position); // Stop moving while attacking
            StartCoroutine(ResumeChaseAfterAttack());
        }
    }

    IEnumerator ResumeChaseAfterAttack()
    {
        yield return new WaitForSeconds(1f); // Adjust based on attack animation length

        if (playerInSightRange)
        {
            isChasing = true;
            ChasePlayer(); // Resume chasing after attack
        }
        else
        {
            isChasing = false;
        }
    }

    protected virtual void ChasePlayer()
    {
        if (agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    protected virtual void ReturnToOrigin()
    {
        if (agent != null)
        {
            agent.SetDestination(startingPos);
        }
    }

    protected virtual void Patrolling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    protected virtual void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(startingPos.x + randomX, startingPos.y, startingPos.z + randomZ);

        if (NavMesh.SamplePosition(walkPoint, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            walkPoint = hit.position; // Ensure walk point is valid
            walkPointSet = true;
        }
        else
        {
            walkPointSet = false;
        }
    }

    protected virtual IEnumerator PatrolPause()
    {
        isPausing = true;
        yield return new WaitForSeconds(patrolPauseTime);
        Patrolling();
        isPausing = false;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startingPos, walkPointRange);
    }
}
