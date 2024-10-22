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
    public int current_ammo = 10;
    public int max_ammo = 30;
    private int bullets_used = -1;
    [SerializeField]
    private Animator attackAnimation;
    private IEnumerator startAttackAnimation;
    private AmmoSystem ammoSystem = new AmmoSystem();

    private void Awake()
    {
        playerUI.ammo.text = current_ammo.ToString();
        //attackAnimation = transform.Find("Wand").GetComponent<Animator>();
    }

    public override void Start()
    {
        base.Start();
        reload_time = 1.00f;
    }

    public override void Attack()
    {
        if (wandBulletSpawn != null)
        {
            if (current_ammo > 0)
            {
                //current_ammo--;
                UpdateAmmo(current_ammo,bullets_used);
                attack_sound.Play();

                startAttackAnimation = play_animation(attackAnimation);
                StartCoroutine(startAttackAnimation);
                
                base.Attack();
                /*
                if (Physics.Raycast(attack_spawn.position, direction, out RaycastHit hit))
                {
                    if (hit.collider.GetComponent<HealthSystem>() != null)
                    {
                        Debug.Log("Enemy Hit");
                        OnHit(hit.collider.GetComponent<HealthSystem>());
                    }
                }
                */
                

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

    public void UpdateAmmo(int current_ammo,int bullets_used)
    {
        ammoSystem.UpdateAmmoSystem(current_ammo,bullets_used);
        playerUI.ammo.text = current_ammo.ToString();
    }

    private IEnumerator play_animation(Animator attackAnimation)
    {
        attackAnimation.SetBool("firing", true);
        yield return new WaitForSeconds(reload_time);
        attackAnimation.SetBool("firing", false);

    }

}