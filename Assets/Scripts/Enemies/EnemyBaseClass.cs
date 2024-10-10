using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseClass : MonoBehaviour
{
    // protected float eHealth, eSpeed, eAtkRate, eDamage;
	[SerializeField]
    private NavMeshAgent agent;

	protected Transform player;

	public LayerMask whatIsPlayer;

	// Patrolling
	public Vector3 walkPoint;
	public bool walkPointSet;
	public float walkPointRange;
	public Vector3 startingPos;

	// Attacking

	// States
	public float sightRange, attackRange;
	public bool playerInSightRange, playerInAttackRange;

	protected virtual void Awake()
	{
		player = GameObject.Find("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		startingPos = transform.position;
	}

	protected virtual void Update()
	{
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

		/*if (!playerInSightRange && !playerInAttackRange)
			Patrolling();*/
		if(Physics.CheckSphere(transform.position, sightRange, whatIsPlayer) && !playerInAttackRange)
			ChasePlayer();
		if(playerInAttackRange && playerInSightRange)
			AttackPlayer();
		if (!playerInAttackRange && !playerInSightRange)
			ReturnToOrigin();
	}

	/*protected virtual void Patrolling()
	{
		if (!walkPointSet)
			SearchWalkPoint();

		if (walkPointSet)
			agent.SetDestination(walkPoint);

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		// Walkpoint reached
		if(distanceToWalkPoint.magnitude < 1f)
			walkPointSet = false;
	}*/

	protected virtual void SearchWalkPoint()
	{
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);

		walkPoint = new Vector3(startingPos.x + randomX, startingPos.y, startingPos.z + randomZ);

		walkPointSet = true;
	}

	protected virtual void ChasePlayer()
	{
		if (GetComponent<NavMeshAgent>() != null)
		{
			agent.SetDestination(player.position);
		}
		
	}

	protected virtual void ReturnToOrigin()
	{
		if (GetComponent<NavMeshAgent>() != null)
		{
			agent.SetDestination(startingPos);
		}
	}

	protected virtual void AttackPlayer()
	{
		if (GetComponent<NavMeshAgent>() != null)
		{
			Debug.Log("Attack!");
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
