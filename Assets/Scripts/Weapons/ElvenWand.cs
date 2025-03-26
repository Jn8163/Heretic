using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElvenWand : RangedWeapon
{
	[SerializeField] private GameObject chargedHitEffectPFab, chargedProjectilePFab;
	[SerializeField] private float chargedBulletSpeed;
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
        PauseSystem ps = FindFirstObjectByType<PauseSystem>();

		if (ps.mOpen)
		{
			return;
		}
		else
		{

			base.Attack(c);

			if (!ActivateTome.isCharged)
			{
				if (!cooldown && ammoSystem.UpdateAmmo(ammoType, ammoUsage, false) && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weaponRange, detectableLayers))
				{
					StartCoroutine(nameof(WeaponCooldown));
					Vector3 offset = -transform.forward * .25f;
					Instantiate(projectilePFab, hit.point + offset, Quaternion.identity);
					audioSource.Play();
				}
			}
			else
			{
				if (!cooldown)
				{
					if (ammoSystem.UpdateAmmo(ammoType, ammoUsage, false) && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weaponRange, detectableLayers))
					{
						StartCoroutine(nameof(WeaponCooldown));
						Vector3 offset = -transform.forward * .25f;
						Instantiate(chargedHitEffectPFab, hit.point + offset, Quaternion.identity);
						audioSource.Play();

						if (Physics.Raycast(transform.position, transform.forward - transform.right * 0.04f, out RaycastHit hit2, weaponRange, detectableLayers))
						{
							offset = -transform.forward * .25f;
							Instantiate(chargedHitEffectPFab, hit2.point + offset, Quaternion.identity);
						}
						if (Physics.Raycast(transform.position, transform.forward - transform.right * 0.08f, out RaycastHit hit3, weaponRange, detectableLayers))
						{
							offset = -transform.forward * .25f;
							Instantiate(chargedHitEffectPFab, hit3.point + offset, Quaternion.identity);
						}
						if (Physics.Raycast(transform.position, transform.forward + transform.right * 0.04f, out RaycastHit hit4, weaponRange, detectableLayers))
						{
							offset = -transform.forward * .25f;
							Instantiate(chargedHitEffectPFab, hit4.point + offset, Quaternion.identity);
						}
						if (Physics.Raycast(transform.position, transform.forward + transform.right * 0.08f, out RaycastHit hit5, weaponRange, detectableLayers))
						{
							offset = -transform.forward * .25f;
							Instantiate(chargedHitEffectPFab, hit5.point + offset, Quaternion.identity);
						}
					}
					// non-hitscan
					Projectile PL = Instantiate(chargedProjectilePFab, transform.position, transform.rotation).GetComponent<Projectile>();
					PL.SetProjectile(chargedBulletSpeed, transform.forward - transform.right * 0.08f);
					Projectile PR = Instantiate(chargedProjectilePFab, transform.position, transform.rotation).GetComponent<Projectile>();
					PR.SetProjectile(chargedBulletSpeed, transform.forward + transform.right * 0.08f);
				}

			}
		}
    }



    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }

	private void Update()
	{
		/*Debug.DrawLine(transform.position, transform.position + transform.forward * weaponRange, Color.blue);
		Debug.DrawLine(transform.position, transform.position + transform.forward * weaponRange - transform.right * 4, Color.red);
		Debug.DrawLine(transform.position, transform.position + transform.forward * weaponRange - transform.right * 8, Color.red);
		Debug.DrawLine(transform.position, transform.position + transform.forward * weaponRange + transform.right * 4, Color.red);
		Debug.DrawLine(transform.position, transform.position + transform.forward * weaponRange + transform.right * 8, Color.red);*/
	}
}