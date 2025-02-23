using UnityEngine;

public class TomedClawProjectile : MonoBehaviour
{
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject ripperProjectile;
    [SerializeField] private float ripperSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 0)
        {
            
        }
    }

    private void OnEnable()
    {
        Instantiate(hitPrefab, transform.position, transform.rotation);

        // Rippers
        Projectile P1 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P1.SetProjectile(ripperSpeed, transform.forward);

        Projectile P2 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P2.SetProjectile(ripperSpeed, -transform.forward);

        Projectile P3 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P3.SetProjectile(ripperSpeed, transform.right);

        Projectile P4 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P4.SetProjectile(ripperSpeed, -transform.right);

        // Diagonals
        Projectile P5 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P5.SetProjectile(ripperSpeed, transform.forward + transform.right);

        Projectile P6 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P6.SetProjectile(ripperSpeed, -transform.forward + transform.right);

        Projectile P7 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P7.SetProjectile(ripperSpeed, -transform.forward - transform.right);

        Projectile P8 = Instantiate(ripperProjectile, transform.position, transform.rotation).GetComponent<Projectile>();
        P8.SetProjectile(ripperSpeed, transform.forward - transform.right);

        Destroy(gameObject);
    }
}
