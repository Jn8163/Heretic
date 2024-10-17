using Unity.VisualScripting;
using UnityEngine;

public class ElvenWandBullet : BulletSystem
{
    void OnEnable()
    {
        bulletDMG = -10;
    }

}
