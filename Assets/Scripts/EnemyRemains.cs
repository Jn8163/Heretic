using UnityEngine;

public class EnemyRemains : MonoBehaviour
{
    private void OnEnable()
    {
        Ray ray = new(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            transform.position = hit.point;
        }
    }
}