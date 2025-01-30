using System.Collections;
using UnityEngine;

public class CrossBowBolt : MonoBehaviour
{
    [SerializeField] private GameObject HitPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Instantiate(HitPrefab, other.transform.position, other.gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
