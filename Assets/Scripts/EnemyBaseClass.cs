using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseClass : MonoBehaviour
{
    public float eHealth, eSpeed, eAtkRate, eDamage;

    public NavMeshAgent agent;

	public Transform player;

	public LayerMask whatIsPlayer;

	// Patrolling
	public Vector3 walkPoint;
	public bool walkPointSet;
	public float walkPointRange;

	// Attacking

	// States
	public float sightRange, attackRange;
	public bool playerInSightRange, playerInAttackRange;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

		if (!playerInSightRange && !playerInAttackRange)
			Patrolling();
		if(playerInSightRange && !playerInAttackRange)
			ChasePlayer();
		if(playerInAttackRange && playerInSightRange)
			AttackPlayer();
	}

	private void Patrolling()
	{
		if (!walkPointSet)
			SearchWalkPoint();

		if (walkPointSet)
			agent.SetDestination(walkPoint);

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		// Walkpoint reached
		if(distanceToWalkPoint.magnitude < 1f)
			walkPointSet = false;
	}

	private void SearchWalkPoint()
	{
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);

		walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		walkPointSet = true;
	}

	private void ChasePlayer()
	{
		agent.SetDestination(player.position);
	}

	public void AttackPlayer()
	{
		agent.SetDestination(transform.position);
	}
}
