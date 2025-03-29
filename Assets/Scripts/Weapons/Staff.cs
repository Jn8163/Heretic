using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Staff : Weapon
{
    [SerializeField] private float hitRange = 2.5f;
    [SerializeField] private Vector3 hitBoxHalfSize = Vector3.one;
    [SerializeField] private int damage = -1;
    [SerializeField] private int stunValue = 1;
    [SerializeField] private int knockbackValue = 1;
    [SerializeField] private GameObject staffSprite;
    [SerializeField] private GameObject staffMesh;
    [SerializeField] private int chargedDamage = -20;
    [SerializeField] private int chargedStunValue = 2;
    [SerializeField] private int chargedKnockbackValue = 3;
    [SerializeField] private AudioSource normalHit, chargedHit, electricity;
    [SerializeField] private GameObject staffHit, chargedStaffHit;

    protected override void OnEnable()
    {
        base.OnEnable();

        MeleeWeaponActive();
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

            if (!cooldown)
            {
                if (Physics.BoxCast(transform.position - (transform.forward * .25f), hitBoxHalfSize, transform.forward, out RaycastHit hit, Quaternion.identity, hitRange, detectableLayers, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.TryGetComponent<HealthSystem>(out HealthSystem hSystem))
                    {
                        if (!ActivateTome.isCharged)
                        {
                            hSystem.UpdateHealth(damage);
                            normalHit.pitch = Random.Range(0.95f, 1.05f);
                            normalHit.Play();
                            Vector3 offset = -transform.forward * .25f;
                            Instantiate(staffHit, hit.point + offset, Quaternion.identity);
                        }
                        else
                        {
                            hSystem.UpdateHealth(chargedDamage);
                            chargedHit.pitch = Random.Range(0.95f, 1.05f);
                            chargedHit.Play();
                            Vector3 offset = -transform.forward * .25f;
                            Instantiate(chargedStaffHit, hit.point + offset, Quaternion.identity);
                        }
                    }
                    if (hit.transform.TryGetComponent<StunSystem>(out StunSystem sSystem))
                    {
                        Vector3 hitDirection = hit.transform.position - transform.position;

                        if (!ActivateTome.isCharged)
                            sSystem.TryStun(stunValue, knockbackValue, hitDirection);
                        else
                            sSystem.TryStun(chargedStunValue, chargedKnockbackValue, hitDirection);
                    }

                }
                StartCoroutine(nameof(WeaponCooldown));
            }
        }
    }

    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }

	private void Update()
	{
        if (ActivateTome.isCharged)
        {
            staffSprite.GetComponent<Animator>().SetBool("Supercharged", true);
            if (Random.Range(0, 120) == 119)
            {
                electricity.pitch = Random.Range(0.9f, 1.1f);
				electricity.Play();
            }
        }
        else
        {
			staffSprite.GetComponent<Animator>().SetBool("Supercharged", false);
		}
	}
}