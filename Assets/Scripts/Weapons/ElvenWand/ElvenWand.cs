using UnityEngine;

public class ElvenWand : WeaponSystem
{
    [SerializeField]
    private Transform attack_spawn;
    private Vector3 direction = Vector3.forward;
    public int current_ammo = 10;
    public int max_ammo = 30;

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
                Debug.Log("Current Ammo: " + current_ammo);
                base.Attack();
                Debug.DrawRay(attack_spawn.position, direction);
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
        
    }

  
}