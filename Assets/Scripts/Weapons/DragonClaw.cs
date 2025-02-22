using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragonClaw : RangedWeapon
{
    private bool attacking, wait;
    [SerializeField] private GameObject projectilePFabMiss;

    protected override void OnEnable()
    {
        base.OnEnable();

        pInput.Player.Attack.canceled += AttackCanceled;

        attacking = false;
        cooldown = false;
        wait = false;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        pInput.Player.Attack.canceled -= AttackCanceled;
    }

    private void Update()
    {
        if (attacking && !wait)
        {
            if (!cooldown)
            {
                if (!ActivateTome.isCharged)
                {
                    // normal mode
                    if (ammoSystem.UpdateAmmo(ammoType, ammoUsage, false))
                    {
                        StartCoroutine(nameof(WeaponCooldown));
                        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weaponRange, LayerMask.GetMask("Enemies"))) // hits enemy
                        {
                            Vector3 offset = -transform.forward * .25f;
                            Instantiate(projectilePFab, hit.point + offset, Quaternion.identity);
                        }
                        else if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit2, weaponRange, LayerMask.GetMask("Default"))) // hits wall
                        {
                            Vector3 offset = -transform.forward * .25f;
                            Instantiate(projectilePFabMiss, hit2.point + offset, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    // tomed mode
                }
            }
        }
    }

    protected override void Attack(InputAction.CallbackContext c)
    {
        if (!wait)
        {
            base.Attack(c);

            StartCoroutine(nameof(WeaponCooldown));
        }
    }

    private void AttackCanceled(InputAction.CallbackContext c)
    {
        animator2D.SetBool("Attacking", false);
        attacking = false;
        cooldown = true;
        StartCoroutine(nameof(WaitAfterAttackStop));
    }
    private IEnumerator WaitAfterAttackStop()
    {
        wait = true;
        yield return new WaitForSeconds(reloadTime);
        if(!attacking)
            wait = false;
    }

    protected override IEnumerator WeaponCooldown()
    {
        if (ammoSystem.CheckForAmmo(ammoType))
        {
            audioSource.Play();
            cooldown = true;
            attacking = true;
            animator2D.SetBool("Attacking", true);
            yield return new WaitForSeconds(reloadTime);
            cooldown = false;
        }
        else
        {
            animator2D.SetBool("Attacking", false);
        }
    }
}
