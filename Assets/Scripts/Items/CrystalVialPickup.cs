using System.Collections;
using UnityEngine;

public class CrystalVialPickup : ImmediatePickup
{
    //10 is the value in OG Heretic
    [SerializeField] private int heal_amount = 10;
    private HealthSystem healthSystem;



    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<HealthSystem>(out HealthSystem hSystem))
        {
            if (hSystem.bPlayer)
            {
                healthSystem = hSystem;
                PickupItem();
            }
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<HealthSystem>(out HealthSystem hSystem))
        {
            if (hSystem.bPlayer)
            {
                healthSystem = hSystem;
                PickupItem();
            }
        }
    }



    protected override void PickupItem()
    {
        if (healthSystem.GetMissingHealth() > 0)
        {
			base.PickupItem();
			healthSystem.UpdateHealth(heal_amount);
            StartCoroutine(DestroyPickup());
        }
    }



    protected override IEnumerator DestroyPickup()
    {
        return base.DestroyPickup();
    }
}