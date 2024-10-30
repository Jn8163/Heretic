using UnityEngine;

public abstract class BulletSystem : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    public Transform bullet_spawn;
    public float bullet_speed = 1.00f;
    public float cooldown = 1.00f;
    public float lifetime = 2f;

    public int bulletDMG;
    public Vector3 direction;



    public virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }



    public virtual void OnHit()
    {

    }



    public virtual void Update()
    {
        transform.position = transform.position + (direction * bullet_speed * Time.deltaTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (effect)
        {
            GameObject g = Instantiate(effect, transform);
            g.transform.position -= direction * .1f;
            g.transform.parent = other.transform;
        }

        HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            if (!healthSystem.bPlayer)
            {
                
                Hit(healthSystem);
            }
        }
        if (healthSystem == null)
        {
            Destroy(this.gameObject);
        }
    }


    
    void Hit(HealthSystem healthSystem)
    {
        healthSystem.UpdateHealth(bulletDMG);
    }



    public virtual void InstantiateBullet(Vector3 dir)
    {
        direction = dir;
    }
}