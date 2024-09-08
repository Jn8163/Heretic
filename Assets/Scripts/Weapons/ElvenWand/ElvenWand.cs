using UnityEngine;

public class ElvenWand : WeaponSystem
{
    [SerializeField]
    private Transform attack_spawn;
    private Vector3 direction = Vector3.forward;

    public override void Start()
    {
        base.Start();
        reload_time = 1.00f;
    }

    public override void Attack()
    {
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