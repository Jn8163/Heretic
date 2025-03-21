using System.Collections;
using UnityEngine;

public class GargoyleMelee : EnemyAttackClass
{
    [SerializeField] private Collider claw;
    [SerializeField] private int claw_dmg = -1;
    [SerializeField] private float coolDown = .5f;
    [SerializeField] private Animator animMesh, animSprite;
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
            int x = Random.Range(0, 2);
            if (x < 1)
            {
                GameObject game = GetComponentInParent<EnemyGargoyleAI>().gameObject;
                game.GetComponentInChildren<EnemyAudioCalls>().PlayAone();
            }
            else
            {
                GameObject game = GetComponentInParent<EnemyGargoyleAI>().gameObject;
                game.GetComponentInChildren<EnemyAudioCalls>().PlayAtwo();
            }
        }
        
    }

    IEnumerator HurtBox()
    {
        attacked = true;
		claw.enabled = true;
        animMesh.SetBool("isAttacking", true);
        animSprite.SetBool("isAttacking", true);
        yield return new WaitForSeconds(coolDown);
        if (!playerInAttackRange)
        {
			animMesh.SetBool("isAttacking", false);
			animSprite.SetBool("isAttacking", false);
		}
		// yield return new WaitForSeconds(coolDown);
        claw.enabled = false;
		attacked = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out HealthSystem hSystem))
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
        healthSystem.UpdateHealth(claw_dmg);
    }



    private IEnumerator AttackCooldown()
    {
        attacked = true;
        yield return new WaitForSeconds(coolDown);
        attacked = false;
    }
}