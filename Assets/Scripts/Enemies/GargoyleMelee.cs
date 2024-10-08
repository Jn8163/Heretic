using System.Collections;
using UnityEngine;

public class GargoyleMelee : EnemyAttackClass
{
    [SerializeField] private Collider claw;
    [SerializeField] private int claw_dmg = -1;
    [SerializeField] private float coolDown = .5f;
    [SerializeField] private Animator anim;
    private bool attacked;


    protected void Start()
    {
        claw.enabled = false;
	}
    protected override void MeleeAttack()
    {
        base.MeleeAttack();
        if (!attacked)
        {
            StartCoroutine(nameof(HurtBox));
        }
        
        Debug.Log("claw hit");
    }

    IEnumerator HurtBox()
    {
        attacked = true;
        Debug.Log("hurtbox timer test");
		claw.enabled = true;
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1);
		anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(1);
        claw.enabled = false;
		attacked = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("claw hitbox");
        if(other.TryGetComponent(out HealthSystem hSystem))
        {
            OnHit(hSystem);
        }
    }



    /*private void OnTriggerStay(Collider other)
    {
        if(!attacked && other.TryGetComponent(out HealthSystem hSystem))
        {
            if(other.CompareTag("Player"))
                OnHit(hSystem);
        }
    }*/



    private void OnHit(HealthSystem healthSystem)
    {
        //Deal Damage
        Debug.Log("Hit!");
        healthSystem.UpdateHealth(claw_dmg);
        Debug.Log(healthSystem.currentHealth);
    }



    private IEnumerator AttackCooldown()
    {
        attacked = true;
        yield return new WaitForSeconds(coolDown);
        attacked = false;
    }
}