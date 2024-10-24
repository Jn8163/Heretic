using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public abstract class ImmediatePickup : MonoBehaviour
{
    protected float destroyDelay = 1.0f;
    protected virtual void PickupItem()
    {
    }



    protected virtual IEnumerator DestroyPickup()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}