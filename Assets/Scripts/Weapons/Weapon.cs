using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    public bool currentWeapon;
    [SerializeField] protected LayerMask detectableLayers;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private AudioSource attack_sound;
    [SerializeField] private Animator animator2D, animator3D;
    [SerializeField] protected float reloadTime = 1f;
    [Tooltip("WeaponSlot for swapping weapons, should be between 0 and 5 (Array Index)")]
    [SerializeField] protected int weaponSlot;

    public static Action MeleeWeaponActive = delegate { };

    protected bool cooldown, RangedWeapon;


    PlayerInput pInput;




    protected virtual void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Attack.performed += Attack;

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
    }



    protected virtual IEnumerator WeaponCooldown()
    {
        cooldown = true;
        animator2D.SetBool("Attacking", true);
        animator3D.SetBool("Attacking", true);
        yield return new WaitForSeconds(reloadTime);
        animator2D.SetBool("Attacking", false);
        animator3D.SetBool("Attacking", false);
        cooldown = false;
    }
}