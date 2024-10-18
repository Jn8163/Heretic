using System;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    public static Action<Item, GameObject> PickUpObject = delegate { };
    [SerializeField] private Item pickup;

    private void OnTriggerEnter(Collider other)
    {
        PickUpObject(pickup, gameObject);
    }
}
