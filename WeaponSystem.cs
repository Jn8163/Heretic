using UnityEngine;
using UnityEngine.InputSystem;

public abstract class WeaponSystem : MonoBehaviour
{

    private InputSystem pInput;
    

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
    }

    public virtual void OnHit()
    {
        //Deal Damage
        Debug.Log("Hit!");
    }

    public virtual void OnWeaponSwap()
    {
        //Play Animation
    }

    private void FixedUpdate()
    {
        if (pInput.Player.Attack.IsPressed())
        {
            Attack();
        }
    }
}