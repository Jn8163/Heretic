using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElvenWand : WeaponSystem
{
    [SerializeField]
    private PlayerUI playerUI;
    [SerializeField]
    private AudioSource attack_sound;
    [SerializeField]
    private GameObject wandBullet;
    [SerializeField]
    private GameObject wandBulletSpawn;
    [SerializeField]
    private float projectile_speed = 600;
    private Vector3 direction = Vector3.forward;
    private int bullets_used = -1;
    [SerializeField]
    private Animator attackAnimation;
    private IEnumerator startAttackAnimation;
    public AmmoSystem ammoSystem;

    private void Awake()
    {
        //attackAnimation = transform.Find("Wand").GetComponent<Animator>();
    }

    public override void Start()
    {
        GameObject ammoObject = GameObject.Find("Player");
        if (ammoObject != null)
        {
            ammoSystem = ammoObject.GetComponent<AmmoSystem>();
        }
        base.Start();
        reload_time = 1.00f;

    }

    public override void Attack()
    {
        if (wandBulletSpawn != null)
        {
            if (ammoSystem.ElvenWandAmmo > 0)
            {
                //current_ammo--;
                UpdateAmmo(bullets_used);
                attack_sound.Play();

                startAttackAnimation = play_animation(attackAnimation);
                StartCoroutine(startAttackAnimation);
                
                base.Attack();

                GameObject spell = Instantiate(wandBullet, wandBulletSpawn.transform.position, wandBulletSpawn.transform.rotation);
                Vector3 launchDirection = wandBulletSpawn.transform.forward;
                spell.GetComponent<Rigidbody>().AddForce(launchDirection * projectile_speed);

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

    public void UpdateWandAmmo(int bullets_used)
    {
        ammoSystem.UpdateAmmoSystem(ammoSystem.ElvenWandAmmo,bullets_used);
        playerUI.ammo.text = ammoSystem.ElvenWandAmmo.ToString();
        Debug.Log($"update elven wand ammo, {ammoSystem.ElvenWandAmmo}");
    }

    private IEnumerator play_animation(Animator attackAnimation)
    {
        attackAnimation.SetBool("firing", true);
        yield return new WaitForSeconds(reload_time);
        attackAnimation.SetBool("firing", false);

    }

}