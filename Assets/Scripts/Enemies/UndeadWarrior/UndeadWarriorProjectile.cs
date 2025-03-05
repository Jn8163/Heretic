using UnityEngine;

public class UndeadWarriorProjectile : MonoBehaviour
{
    [SerializeField] private int damage = -5;  // Damage amount
    [SerializeField] private float lifetime = 5f;  // Destroy after X seconds
    [SerializeField] private float throwForce = 15f; // Adjust this value to control speed
    [SerializeField] private LayerMask playerLayer;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("UndeadWarriorProjectile: Rigidbody is missing!");
            return;
        }

        // Ensure Rigidbody settings are correct
        rb.useGravity = false; // Prevent projectile from dropping immediately
        rb.isKinematic = false; // Allow physics-based movement

        // Calculate the direction to the player
        Transform player = GameObject.FindWithTag("Player")?.transform;
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * throwForce; // Apply velocity to make it move
        }
        else
        {
            Debug.LogError("UndeadWarriorProjectile: No player found!");
        }

        Destroy(gameObject, lifetime); // Destroy after a set time
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.gameObject.name); 

        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            if (healthSystem.bPlayer) // Ensure it only damages the player
            {
                healthSystem.UpdateHealth(damage);
                Debug.Log("Projectile dealt " + damage + " damage to player!");
                Destroy(gameObject); // Destroy on impact
            }
        }
        else if (other.CompareTag("Environment") || other.CompareTag("Obstacle"))
        {
            Debug.Log("Projectile hit an obstacle and was destroyed.");
            Destroy(gameObject);
        }
    }
}
