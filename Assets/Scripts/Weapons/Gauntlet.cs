using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


//Implement player being stuck to enemy whenever they attack, they cannot move

public class Gauntlet : Weapon
{
    [SerializeField] private float hitRange = 2.5f;
    [SerializeField] private Vector3 hitBoxHalfSize = Vector3.one;
    [SerializeField] private int damage = -1;
    [SerializeField] private int stunValue = 2;
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
                Debug.DrawLine(transform.position - transform.forward, (transform.position - (transform.forward * .5f)) + transform.forward * hitRange);
                if (Physics.BoxCast(transform.position - transform.forward, hitBoxHalfSize, transform.forward, out RaycastHit hit, Quaternion.identity, hitRange, detectableLayers, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.TryGetComponent<HealthSystem>(out HealthSystem hSystem))
                    {
                        hSystem.UpdateHealth(damage);

                        Vector3 enemyDir = transform.position - hit.transform.position;
                        FindAnyObjectByType<PlayerMovement>().TargetPosition(hit.transform.position + enemyDir.normalized * 2);
                        FindAnyObjectByType<PlayerMovement>().PlayerMovementLocked(true);
                        StartCoroutine(nameof(WeaponCooldown));
                    }
                    if (hit.transform.TryGetComponent<StunSystem>(out StunSystem sSystem))
                    {
                        sSystem.TryStun(stunValue, 0);
                    }
                }
                else
                {
                    FindAnyObjectByType<PlayerMovement>().PlayerMovementLocked(false);
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
        FindAnyObjectByType<PlayerMovement>().PlayerMovementLocked(false);
        attacking = false;
    }



    protected override IEnumerator WeaponCooldown()
    {
        cooldown = true;
        attacking = true;
        animator2D.SetBool("Attacking", true);
        yield return new WaitForSeconds(reloadTime);
        cooldown = false;
    }
}