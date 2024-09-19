using UnityEngine;

public abstract class BulletSystem : MonoBehaviour
{
    public Transform bullet_spawn;
    public float bullet_speed = 1.00f;
    public float cooldown = 1.00f;

    public virtual void Start()
    {

    }

    public virtual void OnHit()
    {

    }

    public virtual void Update()
    {

    }
}
