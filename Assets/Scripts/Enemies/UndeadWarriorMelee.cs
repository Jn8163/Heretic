using System.Collections;
using UnityEngine;

public class UndeadWarriorMelee : EnemyAttackClass
{
    [SerializeField] private Collider swordCollider; // Hurt box for melee attack
    [SerializeField] private int swordDamage = -10;
    [SerializeField] private float attackCooldown = 0.7f;
    [SerializeField] private Animator animMesh, animSprite;

    private bool attacked;

    protected void Start()
    {
        swordCollider.enabled = false; // Ensure hitbox is disabled at start
    }

    public void TryMeleeAttack()
    {
        if (!attacked)
        {
            attacked = true;
            animMesh.SetBool("isAttacking", true);
            animSprite.SetBool("isAttacking", true);
        }
    }

    // 🔹 Enable Hurt Box (Animation Event)
    public void EnableHurtBox()
    {
        swordCollider.enabled = true;
    }

    // 🔹 Disable Hurt Box (Animation Event)
    public void DisableHurtBox()
    {
        swordCollider.enabled = false;
    }

    // 🔹 Called at the end of the attack animation
    public void EndAttack()
    {
        animMesh.SetBool("isAttacking", false);
        animSprite.SetBool("isAttacking", false);
        StartCoroutine(AttackCooldown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out HealthSystem hSystem))
        {
            OnHit(hSystem);
        }
    }

    private void OnHit(HealthSystem healthSystem)
    {
        healthSystem.UpdateHealth(swordDamage);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attacked = false; // Allow attacking again
    }
}
