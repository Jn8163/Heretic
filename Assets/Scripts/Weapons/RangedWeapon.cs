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
    [Tooltip("Set in inspector to starting ammo amount")]
    [SerializeField] protected int currentAmmo = 0;

    public static Action<Ammo, int> UpdateAmmoUI = delegate { };

    public static List<GameObject> weapons = new List<GameObject>();



    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateUI(ammoType, currentAmmo);
    }



    protected override void Start()
    {
        base.Start();
    }



    protected override void OnDisable()
    {
        base.OnDisable();
    }



    public bool IsAmmoFull()
    {
        return currentAmmo >= AmmoMax;
    }



    /// <summary>
    /// Function called to update range weapon's ammo count
    /// </summary>
    /// <param name="ammoType"></param>
    /// <param name="ammoAdjustment"></param>
    public void UpdateAmmo(Ammo ammoType, int ammoAdjustment)
    {
        if (ammoType == this.ammoType)
        {
            currentAmmo += ammoAdjustment;
            currentAmmo = Mathf.Clamp(currentAmmo, 0, AmmoMax);

            UpdateUI(ammoType, currentAmmo);
        }
    }



    public void UpdateUI(Ammo ammoType, int ammoAmount)
    {
        UpdateAmmoUI(ammoType, ammoAmount);
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

public enum Ammo
{
    wand = 0,
    crossbow = 1,
    claw = 2,
    staff = 3,
    rod = 4
}