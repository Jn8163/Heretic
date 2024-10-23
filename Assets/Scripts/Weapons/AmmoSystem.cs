using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public int ElvenWandAmmo;
    public int ElvenWandAmmoMax = 30;
    public int EtherealCrossbowAmmo;
    public int EtherealCrossbowAmmoMax;

    void Start()
    {
        ElvenWandAmmo = 10;
    }


    public virtual void UpdateAmmoSystem(int weapon_ammo, int updateAmmo)
    {
        weapon_ammo += updateAmmo;
    }
}
