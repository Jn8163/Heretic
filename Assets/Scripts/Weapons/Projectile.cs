using System.Collections;
using UnityEngine;

/// <summary>
/// Moves projectile in a straight direction. Customize
/// fields in inspector for different prefab types.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 direction = new Vector3(0, 1, 0);
    [SerializeField] private float speed = 10;
    [SerializeField] private float deathdelay = 1f;



    private void Start()
    {
        StartCoroutine(nameof(WaitToDie));
    }



    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }



    private IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(deathdelay);
        Destroy(gameObject);
    }
}