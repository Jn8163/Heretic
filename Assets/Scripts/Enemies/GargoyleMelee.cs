using System.Collections;
using UnityEngine;

public class GargoyleMelee : EnemyAttackClass
{
    [SerializeField] private Collider claw;
    [SerializeField] private int claw_dmg = -5;
    [SerializeField] private float coolDown = .5f;
    private bool attacked;



    protected override void MeleeAttack()
    {
        base.MeleeAttack();
        claw.enabled = true;
        Debug.Log("claw hit");
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("claw hitbox");
        if(other.TryGetComponent(out HealthSystem hSystem))
        {
            OnHit(hSystem);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(!attacked && other.TryGetComponent(out HealthSystem hSystem))
        {
            OnHit(hSystem);
        }
    }



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