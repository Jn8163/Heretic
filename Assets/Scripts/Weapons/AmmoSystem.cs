using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    [SerializeField] private List<AmmoType> ammoTypes;

    public static Action<Ammo, int> UpdateAmmoUI = delegate { };


    private void Awake()
    {
        foreach (var ammoType in ammoTypes)
        {
            ammoType.currentAmmo = ammoType.startingAmmo;

            UpdateAmmoUI(ammoType.ammo, ammoType.currentAmmo);
        }
    }



    /// <summary>
    /// Returns true if ammo was successfully updated, false if the weapon is out of ammo.
    /// </summary>
    /// <param name="ammoType"></param>
    /// <param name="ammoUsed"></param>
    /// <returns></returns>
    public bool UpdateAmmo(Ammo ammoType, int ammoUsed, bool AmmoPickup)
    {
        int adjustedAmmo = ammoTypes[(int)ammoType].currentAmmo + ammoUsed;
        math.clamp(adjustedAmmo, 0, ammoTypes[(int)ammoType].ammoMax);

        //check if there's enough ammo.
        if(adjustedAmmo < 0)
        {
            return false;
        }

        ammoTypes[(int)ammoType].currentAmmo = adjustedAmmo;

        if (!AmmoPickup)
        {
            UpdateAmmoUI(ammoType, adjustedAmmo);
        }

        return true;
    }



    /// <summary>
    /// returns if the ammotype is empty.
    /// </summary>
    /// <param name="ammoType"></param>
    /// <returns></returns>
    public bool CheckForAmmo(Ammo ammoType)
    {
        return ammoTypes[(int)ammoType].currentAmmo > 0;
    }



    public bool CheckIfFull(Ammo ammoType)
    {
        return ammoTypes[(int)ammoType].currentAmmo >= ammoTypes[(int)ammoType].ammoMax;
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