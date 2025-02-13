using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EtherealCrossbow : RangedWeapon
{
    [SerializeField] private GameObject SmallboltPrefab;

    [SerializeField] private GameObject SmallBoltFiringpoint1;
    [SerializeField] private GameObject SmallBoltFiringpoint2;
    [SerializeField] private GameObject SmallBoltFiringpoint3;
    [SerializeField] private GameObject SmallBoltFiringpoint4;

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

        if (!ActivateTome.isCharged)
        {
            if (!cooldown && ammoSystem.UpdateAmmo(ammoType, ammoUsage, false))
            {
                StartCoroutine(nameof(WeaponCooldown));
                Projectile P = Instantiate(projectilePFab, transform.position, transform.rotation).GetComponent<Projectile>();
                P.SetProjectile(BigBoltSpeed, transform.forward);

                Projectile sP1 = Instantiate(SmallboltPrefab, SmallBoltFiringpoint1.transform.position, SmallBoltFiringpoint1.transform.rotation).GetComponent<Projectile>();
                sP1.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint1.transform.forward);

                Projectile sP2 = Instantiate(SmallboltPrefab, SmallBoltFiringpoint2.transform.position, SmallBoltFiringpoint2.transform.rotation).GetComponent<Projectile>();
                sP2.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint2.transform.forward);

                audioSource.Play();
            }
        }
        else
        {
			if (!cooldown && ammoSystem.UpdateAmmo(ammoType, ammoUsage, false))
			{
				StartCoroutine(nameof(WeaponCooldown));
				Projectile P1 = Instantiate(projectilePFab, transform.position, transform.rotation).GetComponent<Projectile>();
				P1.SetProjectile(BigBoltSpeed, transform.forward);

				Projectile P2 = Instantiate(projectilePFab, SmallBoltFiringpoint1.transform.position, SmallBoltFiringpoint1.transform.rotation).GetComponent<Projectile>();
				P2.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint1.transform.forward);

				Projectile P3 = Instantiate(projectilePFab, SmallBoltFiringpoint2.transform.position, SmallBoltFiringpoint2.transform.rotation).GetComponent<Projectile>();
				P3.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint2.transform.forward);

				Projectile sP1 = Instantiate(SmallboltPrefab, SmallBoltFiringpoint3.transform.position, SmallBoltFiringpoint3.transform.rotation).GetComponent<Projectile>();
				sP1.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint3.transform.forward);

				Projectile sP2 = Instantiate(SmallboltPrefab, SmallBoltFiringpoint4.transform.position, SmallBoltFiringpoint4.transform.rotation).GetComponent<Projectile>();
				sP2.SetProjectile(SmallBoltSpeed, SmallBoltFiringpoint4.transform.forward);
			}
		}
    }
    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }
}