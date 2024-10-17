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
        if (collision.gameObject.GetComponent<HealthSystem>() != null)
        {
            Hit(collision.gameObject.GetComponent<HealthSystem>());
        }
        Destroy(gameObject);
    }

    void Hit(HealthSystem healthSystem)
    {
        healthSystem.UpdateHealth(bulletDMG);
    }
}
