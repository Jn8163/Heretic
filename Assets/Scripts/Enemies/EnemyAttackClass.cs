using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAttackClass : MonoBehaviour
{
    /*protected override void AttackPlayer()
    {
        Debug.Log("attack player called");
        if (attackRange == 1)
        {
            melee = true;
        }
        if (attackRange > 1)
        {
            ranged = true;
        }
        base.AttackPlayer();
        if (melee == true)
        {
            MeleeAttack();
        }
        if (ranged == true)
        {
            RangedAttack();
        }
    }*/

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

