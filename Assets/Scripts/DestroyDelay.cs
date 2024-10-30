using System.Collections;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] private float delay = .5f;
    private void Start()
    {
        StartCoroutine(nameof(DestroyAfterDelay));
    }



    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}