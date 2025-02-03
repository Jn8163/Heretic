using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EtherealCrossbow : RangedWeapon
{
    [SerializeField] private GameObject SmallboltPrefab;

    [SerializeField] private GameObject SmallBoltFiringpoint1;
    [SerializeField] private GameObject SmallBoltFiringpoint2;

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

            Projectile sP1 = Instantiate(SmallboltPrefab, SmallBoltFiringpoint1.transform.position, SmallBoltFiringpoint1.transform.rotation).GetComponent<Projectile>();
            sP1.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint1.transform.forward);

            Projectile sP2 = Instantiate(SmallboltPrefab, SmallBoltFiringpoint2.transform.position, SmallBoltFiringpoint2.transform.rotation).GetComponent<Projectile>();
            sP2.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint2.transform.forward);
        }
    }
    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }
}