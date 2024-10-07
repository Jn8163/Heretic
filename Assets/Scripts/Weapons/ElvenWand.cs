using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElvenWand : WeaponSystem
{
    [SerializeField]
    private Transform attack_spawn;
    [SerializeField]
    private PlayerUI playerUI;
    [SerializeField]
    private AudioSource attack_sound;
    private Vector3 direction = Vector3.forward;
    public int current_ammo = 10;
    public int max_ammo = 30;
    private Animator attackAnimation;
    private IEnumerator startAttackAnimation;

    private void Awake()
    {
        attackAnimation = transform.Find("Wand").GetComponent<Animator>();
    }

    public override void Start()
    {
        base.Start();
        reload_time = 1.00f;
    }

    public override void Attack()
    {
        if (attack_spawn != null)
        {
            if (current_ammo > 0)
            {
                current_ammo--;
                attack_sound.Play();

                startAttackAnimation = play_animation(attackAnimation);
                StartCoroutine(startAttackAnimation);
                
                base.Attack();

                if (Physics.Raycast(attack_spawn.position, direction, out RaycastHit hit))
                {
                    if (hit.collider.GetComponent<HealthSystem>() != null)
                    {
                        Debug.Log("Enemy Hit");
                        OnHit(hit.collider.GetComponent<HealthSystem>());
                    }
                }
            } else
            {
                Debug.Log("Out of ammo");
            }
        }
    }

    public override void OnHit(HealthSystem healthSystem)
    {

        base.OnHit(healthSystem);
    }

    public override void OnWeaponSwap()
    {
        base.OnWeaponSwap();
    }

    private void Update()
    {
        playerUI.ammo.text = current_ammo.ToString();
    }

    private IEnumerator play_animation(Animator attackAnimation)
    {
        attackAnimation.SetBool("firing", true);
        yield return new WaitForSeconds(reload_time);
        attackAnimation.SetBool("firing", false);

    }

}