using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private AudioSource attack_sound;
    [SerializeField] private Animator attackAnimation;
    [SerializeField] protected float reloadTime = 1f;
    [Tooltip("WeaponSlot for swapping weapons, should be between 0 and 5 (Array Index)")]
    [SerializeField] protected int weaponSlot;

    public static Action<bool> RangedWeaponActive = delegate { };

    protected bool currentWeapon, cooldown, RangedWeapon;


    PlayerInput pInput;




    protected virtual void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Attack.performed += Attack;

        RangedWeaponActive(RangedWeapon);
    }



    protected virtual void Start()
    {
        SwapWeapon swapWeapon = FindAnyObjectByType<SwapWeapon>();
        if (swapWeapon)
        {
            swapWeapon.EnableWeapon(weaponSlot);
        }
    }



    protected virtual void OnDisable()
    {
        pInput.Player.Attack.performed -= Attack;

        pInput.Disable();
    }



    /// <summary>
    /// Updates weapon's status of being active.
    /// </summary>
    /// <param name="isActive"></param>
    public virtual void ToggleWeapon(bool isActive)
    {
        currentWeapon = isActive;
        gameObject.GetComponent<GraphicsSwapDelegate>().ToggleVisibility(isActive);
    }



    protected virtual void Attack(InputAction.CallbackContext c)
    {
    }



    protected virtual IEnumerator WeaponCooldown()
    {
        cooldown = true;
        attackAnimation.SetBool("Attacking", true);
        yield return new WaitForSeconds(reloadTime);
        attackAnimation.SetBool("Attacking", false);
        cooldown = false;
    }
}