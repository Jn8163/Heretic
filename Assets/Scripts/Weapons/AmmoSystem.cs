using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    int ElvenWandAmmo;
    int ElvenWandAmmoMax = 30;
    int EtherealCrossbowAmmo;
    int EtherealCrossbowAmmoMax;

    void Start()
    {
        ElvenWandAmmo = 10;
    }


    public virtual void UpdateAmmoSystem(int weapon_ammo, int updateAmmo)
    {
        weapon_ammo += updateAmmo;
    }
}
