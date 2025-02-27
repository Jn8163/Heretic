using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public abstract class ImmediatePickup : Item
{
    [SerializeField] private GameObject mesh, sprite;



    protected override void PickupItem()
    {
        base.PickupItem();
        StartCoroutine(nameof(DestroyPickup));
    }



    protected override IEnumerator DestroyPickup()
    {
        GetComponent<Collider>().enabled = false;
        if (mesh)
        {
            mesh.GetComponent<MeshRenderer>().enabled = false;
        }
        if (sprite)
        {
            sprite.GetComponent<SpriteRenderer>().enabled = false;
        }
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}