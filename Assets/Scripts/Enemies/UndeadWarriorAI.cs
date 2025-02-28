using UnityEngine;

public class UndeadWarriorAI : EnemyBaseClass
{
    [Header("Undead Warrior AI Settings")]
    [SerializeField] private float attackRange = 3f; // Melee attack range
    [SerializeField] private float rangedAttackRange = 8f; // Minimum distance to throw axe

    private UndeadWarriorMelee meleeAttack;
   // private UndeadWarriorRanged rangedAttack;

    protected override void Awake()
    {
        base.Awake();
        meleeAttack = GetComponent<UndeadWarriorMelee>();
     //   rangedAttack = GetComponent<UndeadWarriorRanged>();
    }

    protected override void Update()
    {
        base.Update();

        if (isChasing)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                meleeAttack.TryMeleeAttack(); //Calls Melee Attack from `UndeadWarriorMelee.cs`
            }
            else if (Vector3.Distance(transform.position, player.position) <= rangedAttackRange)
            {
                //rangedAttack.TryRangedAttack(); //Calls Ranged Attack from `UndeadWarriorRanged.cs`
            }
            else
            {
                ChasePlayer(); // Continue moving toward player
            }
        }
    }
}
