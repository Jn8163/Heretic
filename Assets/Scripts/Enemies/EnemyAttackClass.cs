using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAttackClass : MonoBehaviour
{
    /*public bool melee;
    public bool ranged;*/

    /*protected override void AttackPlayer()
    {
        Debug.Log("attack player called");
        if (playerInAttackRange)
        {
            melee = true;
        }
        *//*if (attackRange > 1)
        {
            ranged = true;
        }*//*
        base.AttackPlayer();
        if (melee == true)
        {
            MeleeAttack();
        }
        *//*if (ranged == true)
        {
            RangedAttack();
        }*//*
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

    protected virtual void RangedAttack()
    {
        Debug.Log("ranged triggered");
    }
}

