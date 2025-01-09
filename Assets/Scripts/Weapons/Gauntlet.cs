using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gauntlet : Weapon
{

    protected override void OnEnable()
    {
        base.OnEnable();

        pInput.Player.Attack.canceled += AttackCanceled;

        MeleeWeaponActive();
    }



    protected override void Start()
    {
        base.Start();
    }



    protected override void OnDisable()
    {
        base.OnDisable();
        pInput.Player.Attack.canceled -= AttackCanceled;

    }



    protected override void Attack(InputAction.CallbackContext c)
    {
        base.Attack(c);

        animator2D.SetBool("Attacking", true);
        animator3D.SetBool("Attacking", true);
    }



    private void AttackCanceled(InputAction.CallbackContext c)
    {
        animator2D.SetBool("Attacking", false);
        animator3D.SetBool("Attacking", false);
    }
}