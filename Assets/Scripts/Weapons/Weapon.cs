using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    public bool currentWeapon;
    [SerializeField] protected LayerMask detectableLayers;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] protected AudioSource attack_sound;
    [SerializeField] protected Animator animator2D;
    [SerializeField] protected float reloadTime = 1f;
    [Tooltip("WeaponSlot for swapping weapons, should be between 0 and 5 (Array Index)")]
    [SerializeField] protected int weaponSlot;

    public static Action MeleeWeaponActive = delegate { };

    protected bool cooldown, RangedWeapon;


    protected PlayerInput pInput;




    protected virtual void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Attack.performed += Attack;

        cooldown = false;

        currentWeapon = true;
        GraphicsSwapDelegate g = gameObject.GetComponent<GraphicsSwapDelegate>();
        g.ToggleVisibility(GraphicsSwapDelegate.spriteInactive);
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

        currentWeapon = false;
    }



    protected virtual void Attack(InputAction.CallbackContext c)
    {
        PauseSystem ps = FindFirstObjectByType<PauseSystem>();

        if (ps.mOpen)
        {
            return;
        }
    }



    protected virtual IEnumerator WeaponCooldown()
    {
        cooldown = true;
        animator2D.SetBool("Attacking", true);
        yield return new WaitForSeconds(reloadTime);
        animator2D.SetBool("Attacking", false);
        cooldown = false;
    }
}