using System.Collections;
using UnityEngine;

public class CrystalVialPickup : ImmediatePickup
{
    //10 is the value in OG Heretic
    [SerializeField] private int heal_amount = 10;
    [SerializeField] private GameObject mesh, sprite;

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
        base.PickupItem();
        if (healthSystem.GetMissingHealth() > 0)
        {
            healthSystem.UpdateHealth(heal_amount);
            StartCoroutine(DestroyPickup());
        }
    }



    protected override IEnumerator DestroyPickup()
    {
        GetComponent<Collider>().enabled = false;
        mesh.GetComponent<MeshRenderer>().enabled = false;
        sprite.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}