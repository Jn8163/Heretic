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
                Debug.Log("Player is in attack range!"); 

                if (distanceToPlayer <= meleeRange)
                {
                    Debug.Log("Undead Warrior is attacking with melee!"); 
                    AttackMelee();
                }
                else if (distanceToPlayer <= throwRange)
                {
                    Debug.Log("Undead Warrior is attacking with throw!"); 
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
        Debug.Log("Undead Warrior is attempting to throw!"); 
        GetComponent<UndeadWarriorThrow>().TriggerThrowAttack();
    }

}
