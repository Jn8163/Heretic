using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gauntlet : Weapon
{
    [SerializeField] private float hitRange = 2.5f;
    [SerializeField] private Vector3 hitBoxHalfSize = Vector3.one;
    [SerializeField] private int damage = -1;
    private bool attacking;



    protected override void OnEnable()
    {
        base.OnEnable();

        pInput.Player.Attack.canceled += AttackCanceled;

        MeleeWeaponActive();
        attacking = false;
    }



    protected override void Start()
    {
        base.Start();
    }



    private void Update()
    {
        if (attacking)
        {
            if (!cooldown)
            {

                if (Physics.BoxCast(transform.position - (transform.forward * .25f), hitBoxHalfSize, transform.forward, out RaycastHit hit, Quaternion.identity, hitRange, detectableLayers, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.TryGetComponent<HealthSystem>(out HealthSystem hSystem))
                    {
                        hSystem.UpdateHealth(damage);
                        FindAnyObjectByType<PlayerMovement>().TargetPosition(hit.transform.position + hit.transform.forward);
                    }
                }
            }
        }
    }



    protected override void OnDisable()
    {
        base.OnDisable();
        pInput.Player.Attack.canceled -= AttackCanceled;

    }



    protected override void Attack(InputAction.CallbackContext c)
    {
        base.Attack(c);

        StartCoroutine(nameof(WeaponCooldown));
    }



    private void AttackCanceled(InputAction.CallbackContext c)
    {
        animator2D.SetBool("Attacking", false);
        animator3D.SetBool("Attacking", false);
        attacking = false;
    }



    protected override IEnumerator WeaponCooldown()
    {
        cooldown = true;
        attacking = true;
        animator2D.SetBool("Attacking", true);
        animator3D.SetBool("Attacking", true);
        yield return new WaitForSeconds(reloadTime);
        cooldown = false;
    }
}