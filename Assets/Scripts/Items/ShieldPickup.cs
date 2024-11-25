using System.Collections;
using UnityEngine;

public class ShieldPickup : ImmediatePickup
{
    //10 is the value in OG Heretic
    [SerializeField] private int shieldAmount = 100;
    [SerializeField] private GameObject mesh, sprite;

    private HealthSystem healthSystem;



    private void OnTriggerEnter(Collider other)
    {
        if (ArmorSystem.instance)
        {
            PickupItem();
        }
    }



    protected override void PickupItem()
    {
        base.PickupItem();
        if (ArmorSystem.instance.CreateShield(shieldAmount))
        {
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