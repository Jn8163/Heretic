using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    [SerializeField] private List<AmmoType> ammoTypes;
    [SerializeField] private AmmoType startingAmmoType;

    public static Action<Ammo, int> UpdateAmmoUI = delegate { };
    public AmmoType currentAmmoType;


    private void Awake()
    {
        foreach (var ammoType in ammoTypes)
        {
            ammoType.currentAmmo = ammoType.startingAmmo;

            UpdateAmmoUI(ammoType.ammo, ammoType.currentAmmo);
        }
        currentAmmoType = startingAmmoType;
        UpdateAmmoUI(currentAmmoType.ammo, currentAmmoType.currentAmmo);
    }

    public void UpdateAmmoMax(List<int> ammoGained)
    {
        for (int i = 0; i < ammoTypes.Count; i++)
        {
            ammoTypes[i].ammoMax *= 2;
			UpdateAmmo((Ammo)i, ammoGained[i], true);
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
        adjustedAmmo = Mathf.Clamp(adjustedAmmo, -1, ammoTypes[(int)ammoType].ammoMax);

        //check if there's enough ammo.
        if(adjustedAmmo < 0)
        {
            return false;
        }

        ammoTypes[(int)ammoType].currentAmmo = adjustedAmmo;

        if (!AmmoPickup)
        {
            currentAmmoType = ammoTypes[(int)ammoType];
        }

        if (currentAmmoType.ammo == ammoType)
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