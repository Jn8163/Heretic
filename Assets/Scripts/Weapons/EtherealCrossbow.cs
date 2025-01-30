using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EtherealCrossbow : RangedWeapon
{
    [SerializeField] private float BigBoltSpeed;
    [SerializeField] private float SmallBoltSpeed;

    protected override void OnEnable()
    {
        base.OnEnable();

        cooldown = false;
    }



    protected override void Start()
    {
        base.Start();
    }



    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Attack(InputAction.CallbackContext c)
    {
        base.Attack(c);

        if (!cooldown && ammoSystem.UpdateAmmo(ammoType, ammoUsage, false))
        {
            StartCoroutine(nameof(WeaponCooldown));
            Projectile P = Instantiate(projectilePFab, transform.position, transform.rotation).GetComponent<Projectile>();
            P.SetProjectile(BigBoltSpeed, transform.forward);
        }
    }
    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }
}