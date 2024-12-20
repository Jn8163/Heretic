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

    protected bool currentWeapon, cooldown;


    PlayerInput pInput;




    protected virtual void OnEnable()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.Player.Attack.performed += Attack;
    }



    protected virtual void Start()
    {
        GameObject swapWeapon = (GameObject)FindAnyObjectByType(typeof(SwapWeapon));
        if (swapWeapon)
        {
            swapWeapon.GetComponent<SwapWeapon>().EnableWeapon(weaponSlot);
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
    /// <param name="equipped"></param>
    public virtual void ToggleWeapon(bool equipped)
    {
        currentWeapon = equipped;
        gameObject.GetComponent<GraphicsSwapDelegate>().ToggleVisibility(equipped);
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