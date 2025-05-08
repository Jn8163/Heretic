using System.Collections;
using UnityEngine;

public class UndeadWarriorProjectile : MonoBehaviour
{
    [SerializeField] private int damage = -5;  // Damage amount
    [SerializeField] private float lifetime = 5f;  // Destroy after X seconds
    [SerializeField] private float throwForce = 15f; // Adjust this value to control speed
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    private Rigidbody rb;

    private void Start()
    {
        audioSource.Play();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
#if UNITY_EDITOR
            Debug.LogError("UndeadWarriorProjectile: Rigidbody is missing!");
#endif
            return;
        }

        // Ensure Rigidbody settings are correct
        rb.useGravity = false; // Prevent projectile from dropping immediately
        rb.isKinematic = false; // Allow physics-based movement

        // Calculate the direction to the player
        Vector3 player = GameObject.FindWithTag("Player").transform.position;
        if (player != null)
        {
            player.y += 1.5f;
            Vector3 direction = (player - transform.position).normalized;
            rb.linearVelocity = direction * throwForce; // Apply velocity to make it move
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("UndeadWarriorProjectile: No player found!");
#endif
        }

        Destroy(gameObject, lifetime); // Destroy after a set time
    }

    private void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        Debug.Log("Projectile hit: " + other.gameObject.name);
#endif

        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            if (healthSystem.bPlayer) // Ensure it only damages the player
            {
                healthSystem.UpdateHealth(damage);
#if UNITY_EDITOR
                Debug.Log("Projectile dealt " + damage + " damage to player!");
#endif
                StartCoroutine(nameof(DestroyAxe)); // Destroy on impact
            }
        }
        else if (other.gameObject.layer == 0 || other.gameObject.layer == 4)
        {
#if UNITY_EDITOR
            Debug.Log("Projectile hit an obstacle and was destroyed.");
#endif
            StartCoroutine(nameof(DestroyAxe));
        }
    }

    IEnumerator DestroyAxe()
    {
        rb.linearVelocity = Vector3.zero;
        anim.SetBool("Explode", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
