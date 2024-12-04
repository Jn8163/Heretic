using System.Collections;
using UnityEngine;
public class GolemMelee : EnemyAttackClass
{
    [SerializeField] private Collider fist;
    [SerializeField] private int fist_dmg = -5;
    [SerializeField] private float coolDown = .5f;
    [SerializeField] private Animator animMesh, animSprite;
    private bool attacked;


    protected void Start()
    {
        fist.enabled = false;
    }
    protected override void MeleeAttack()
    {
        base.MeleeAttack();
        if (!attacked)
        {
            StartCoroutine(nameof(HurtBox));
        }

    }

    IEnumerator HurtBox()
    {
        attacked = true;
        fist.enabled = true;
        animMesh.SetBool("isAttacking", true);
        animSprite.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1);
        animMesh.SetBool("isAttacking", false);
        animSprite.SetBool("isAttacking", false);
        yield return new WaitForSeconds(1);
        fist.enabled = false;
        attacked = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthSystem hSystem))
        {
            OnHit(hSystem);
        }
    }

    private void OnHit(HealthSystem healthSystem)
    {
        //Deal Damage
        healthSystem.UpdateHealth(fist_dmg);
    }



    private IEnumerator AttackCooldown()
    {
        attacked = true;
        yield return new WaitForSeconds(coolDown);
        attacked = false;
    }
}
