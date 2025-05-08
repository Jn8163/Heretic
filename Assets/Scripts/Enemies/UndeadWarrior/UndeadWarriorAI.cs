using UnityEngine;
using System.Collections;

public class UndeadWarriorAI : EnemyBaseClass
{
    [SerializeField] private float throwRange = 10f;
    [SerializeField] private float meleeRange = 3f;
    [SerializeField] private GameObject throwPrefab;
    [SerializeField] private Transform throwPoint;

    protected override void Update()
    {

        if (healthSystem != null && healthSystem.bAlive)
        {
            base.Update();

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (playerInAttackRange)
            {
#if UNITY_EDITOR
                Debug.Log("Player is in attack range!");
#endif

                if (distanceToPlayer <= meleeRange)
                {
#if UNITY_EDITOR
                    Debug.Log("Undead Warrior is attacking with melee!");
#endif
                    AttackMelee();
                }
                else if (distanceToPlayer <= throwRange)
                {
#if UNITY_EDITOR
                    Debug.Log("Undead Warrior is attacking with throw!");
#endif
                    AttackThrow();
                }
            }
        }
    }


    private void AttackMelee()
    {
        GetComponent<UndeadWarriorMelee>().TriggerMeleeAttack();
    }

    private void AttackThrow()
    {
#if UNITY_EDITOR
        Debug.Log("Undead Warrior is attempting to throw!");
#endif
        GetComponent<UndeadWarriorThrow>().TriggerThrowAttack();
    }

}
