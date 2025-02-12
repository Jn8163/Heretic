using System.Collections;
using UnityEngine;

public class CrossBowBolt : MonoBehaviour
{
    [SerializeField] private GameObject HitPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 || other.gameObject.layer == 0)
        {
            Instantiate(HitPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
