using System.Collections;
using UnityEngine;

public class UndeadWarriorMelee : EnemyAttackClass
{
    [SerializeField] private float attackRadius = 2f; // Radius of the melee attack
    [SerializeField] private int attackDamage = -5;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animMesh, animSprite;

    private bool attacked;
    private Collider attackCollider; // Reference to the sphere collider

    private void Start()
    {
        attackCollider = GetComponent<SphereCollider>();
        if (attackCollider == null)
        {
#if UNITY_EDITOR
            Debug.LogError("UndeadWarriorMelee: No SphereCollider found on the Undead Warrior!");
#endif
        }
    }

    public void TriggerMeleeAttack()
    {
#if UNITY_EDITOR
        Debug.Log("Undead warrior is attacking");
#endif
        if (!attacked)
        {
            StartCoroutine(nameof(MeleeAttackRoutine));
        }
    }

    private IEnumerator MeleeAttackRoutine()
    {
        attacked = true;
        animMesh.SetBool("isAttacking", true);
        animSprite.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.2f); // Delay before dealing damage

        // Detect the player inside the sphere collider
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);
        foreach (Collider player in hitPlayers)
        {
            if (player.TryGetComponent(out HealthSystem hSystem))
            {
                hSystem.UpdateHealth(attackDamage);
            }
        }

        yield return new WaitForSeconds(0.5f); // Attack animation duration

        animMesh.SetBool("isAttacking", false);
        animSprite.SetBool("isAttacking", false);

        yield return new WaitForSeconds(attackCooldown);
        attacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
