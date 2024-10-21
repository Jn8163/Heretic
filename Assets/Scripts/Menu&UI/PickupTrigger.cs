using System;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    public static Action<Item, GameObject> PickUpObject = delegate { };
    [SerializeField] private Item pickup;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.Play();

        PickUpObject(pickup, gameObject);
    }
}
