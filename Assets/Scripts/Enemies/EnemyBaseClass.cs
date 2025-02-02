using UnityEngine;
using UnityEngine.AI;

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
    public float walkPointRange;
    public Vector3 startingPos;

    // States
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
            // Check vision using FOV component
            playerInSightRange = fov.canSeePlayer;
			playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (playerInSightRange || playerInAttackRange)
            {
                isChasing = true;
                chaseTimer = 0;
            }

            if ((!playerInSightRange && !playerInAttackRange) && isChasing)
            {
                chaseTimer += Time.deltaTime;
                if (chaseTimer > 5)
                {
                    chaseTimer = 0;
                    isChasing = false;
                }
            }

            if (isChasing && !playerInAttackRange)
                ChasePlayer();
            if (playerInAttackRange && playerInSightRange)
                AttackPlayer();
            if (!isChasing)
            {
                ReturnToOrigin();
                Patrolling();
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

    protected virtual void Patrolling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    protected virtual void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(startingPos.x + randomX, startingPos.y, startingPos.z + randomZ);
        walkPointSet = true;
    }

    protected virtual void ChasePlayer()
    {
        if (agent != null && playerInSightRange)
        {
            isChasing = true;
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

    protected virtual void AttackPlayer()
    {
        if (agent != null)
        {
            agent.SetDestination(transform.position);
        }
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
