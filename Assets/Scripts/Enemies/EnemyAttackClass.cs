using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAttackClass : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public float attackRange;
    public bool playerInAttackRange;

    protected virtual void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (playerInAttackRange)
                MeleeAttack();
    }

	protected virtual void AttackPlayer()
	{

	}

    protected virtual void MeleeAttack()
    {
        Debug.Log("melee triggered");
    }
}

