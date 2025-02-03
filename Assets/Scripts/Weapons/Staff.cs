using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Staff : Weapon
{
    [SerializeField] private float hitRange = 2.5f;
    [SerializeField] private Vector3 hitBoxHalfSize = Vector3.one;
    [SerializeField] private int damage = -1;
    [SerializeField] private int stunValue = 2;



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
        base.Attack(c);

        if (!cooldown)
        {
            if(Physics.BoxCast(transform.position - (transform.forward * .25f), hitBoxHalfSize, transform.forward, out RaycastHit hit, Quaternion.identity, hitRange, detectableLayers, QueryTriggerInteraction.Ignore))
            {
                if(hit.transform.TryGetComponent<HealthSystem>(out HealthSystem hSystem))
                {
                    hSystem.UpdateHealth(damage);
                }
                if (hit.transform.TryGetComponent<StunSystem>(out StunSystem sSystem))
                {
                    sSystem.TryStun(stunValue, 1);
                }
            }
            StartCoroutine(nameof(WeaponCooldown));
        }
    }



    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }
}