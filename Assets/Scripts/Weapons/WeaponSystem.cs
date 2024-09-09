using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public abstract class WeaponSystem : MonoBehaviour
{
    public int weapon_damage = -10;
    private InputSystem pInput;
    public float reload_time = 1.00f;
    private bool reloading = false;

    private IEnumerator coroutine;

    public virtual void Start()
    {
        pInput = new InputSystem();
        pInput.Enable();
        Debug.Log("Started");
    }

    public virtual void Attack()
    {
        //Do Attack
        Debug.Log("Attack Casted");
        coroutine = start_reloading(reload_time);
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

    private IEnumerator start_reloading(float reload_time)
    {
        reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reload_time);

        reloading = false;
    }
    
}