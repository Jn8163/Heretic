using Unity.VisualScripting;
using UnityEngine;

public class GargoyleMelee : EnemyAttackClass
{
    [SerializeField]
    Collider claw;
    [SerializeField]
    int claw_dmg = -5;
    protected override void MeleeAttack()
    {
        base.MeleeAttack();
        claw.enabled = true;
        Debug.Log("claw hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("claw hitbox");
        if(other.GetComponent<HealthSystem>() != null)
        {
            OnHit(other.GetComponent<HealthSystem>());
        }
    }
    private void OnHit(HealthSystem healthSystem)
    {
        //Deal Damage
        Debug.Log("Hit!");
        healthSystem.UpdateHealth(claw_dmg);
        Debug.Log(healthSystem.currentHealth);
    }
}
