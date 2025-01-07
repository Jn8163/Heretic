using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElvenWand : RangedWeapon
{



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

        if(!cooldown && ammoSystem.UpdateAmmo(ammoType, ammoUsage, false) && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, weaponRange, detectableLayers))
        {
            Debug.Log(hit.transform.gameObject);
            StartCoroutine(nameof(WeaponCooldown));
            Vector3 offset = -transform.forward * .25f;
            Instantiate(projectilePFab, hit.point + offset, Quaternion.identity);
        }
    }



    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }
}