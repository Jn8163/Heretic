using UnityEngine;

public abstract class BulletSystem : MonoBehaviour
{
    public Transform bullet_spawn;
    public float bullet_speed = 1.00f;
    public float cooldown = 1.00f;

    public int bulletDMG;

    public virtual void Start()
    {

    }

    public virtual void OnHit()
    {

    }

    public virtual void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            if (!healthSystem.bPlayer)
            {
                Hit(healthSystem);
            }
        }
        Destroy(this.gameObject);
    }

    void Hit(HealthSystem healthSystem)
    {
        healthSystem.UpdateHealth(bulletDMG);
    }
}
