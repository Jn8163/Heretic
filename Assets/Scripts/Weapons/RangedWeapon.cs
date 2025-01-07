using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Base class for ranged weapon types
/// </summary>
public abstract class RangedWeapon : Weapon
{
    [SerializeField] public Ammo ammoType = Ammo.wand;
    [SerializeField] protected GameObject projectilePFab;
    [SerializeField] protected int AmmoMax = 100, weaponRange = 50;
    [SerializeField] protected int ammoUsage = -1;

    protected AmmoSystem ammoSystem;



    protected override void OnEnable()
    {
        base.OnEnable();

        ammoSystem.UpdateAmmo(ammoType, 0, false);
    }



    protected override void Start()
    {
        base.Start();

        ammoSystem = FindAnyObjectByType<AmmoSystem>();

        if (currentWeapon)
        {
            ammoSystem.UpdateAmmo(ammoType, 0, false);
        }
    }



    protected override void OnDisable()
    {
        base.OnDisable();
    }



    protected override void Attack(InputAction.CallbackContext c)
    {
        base.Attack(c);
    }



    protected override IEnumerator WeaponCooldown()
    {
        return base.WeaponCooldown();
    }
}