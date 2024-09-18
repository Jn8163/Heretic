using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackClass : EnemyBaseClass
{
    public bool melee;
    public bool ranged;

    protected override void AttackPlayer()
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

