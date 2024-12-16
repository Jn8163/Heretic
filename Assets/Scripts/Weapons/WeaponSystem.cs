using UnityEngine;
using System.Collections;

public abstract class WeaponSystem : MonoBehaviour
{
    public bool is_ammo = true;
    public int weapon_ammo = 10;
    public int weapon_damage = -10;
    private PlayerInput pInput;
    public float reload_time = 1.00f;
    private bool reloading = false;

    private IEnumerator coroutine;

    public virtual void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        Debug.Log("Started");
    }

    public virtual void Attack()
    {
        //Do Attack
        if (is_ammo) {
            // weapon_ammo--;
        }
        coroutine = start_cooldown(reload_time);
        StartCoroutine(coroutine);
    }

    public virtual void OnHit(HealthSystem healthSystem)
    {
        //Deal Damage
        Debug.Log("Hit!");
        healthSystem.UpdateHealth(weapon_damage);
        Debug.Log(healthSystem.currentHealth);
    }

    public virtual void OnWeaponSwap()
    {
        //Play Animation
    }

    private void FixedUpdate()
    {
        if (pInput.Player.Attack.IsPressed() && reloading == false)
        {
            Attack();
        }
    }

    private IEnumerator start_cooldown(float reload_time)
    {
        reloading = true;
        yield return new WaitForSeconds(reload_time);

        reloading = false;
    }

    public void UpdateAmmo(int updateAmmo)
    {
        weapon_ammo += updateAmmo;
    }

}