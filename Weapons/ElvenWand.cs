using UnityEngine;

public class ElvenWand : WeaponSystem
{
    [SerializeField]
    private float weapon_cooldown = 1.00f;
    [SerializeField]
    private int weapon_damage = 10;
    [SerializeField]
    private Transform attack_spawn;
    private Vector3 direction = Vector3.forward;
    
    public override void Start()
    {
        base.Start();

    }

    public override void Attack()
    {
        base.Attack();
        Debug.DrawRay(attack_spawn.position, direction);
        if (Physics.Raycast(attack_spawn.position, direction, out RaycastHit hit))
        {
            OnHit();
        }
    }

    public override void OnHit()
    {
        base.OnHit();
    }

    public override void OnWeaponSwap()
    {
        base.OnWeaponSwap();
    }

    private void Update()
    {
        Debug.DrawRay(attack_spawn.position, direction);
    }
}
