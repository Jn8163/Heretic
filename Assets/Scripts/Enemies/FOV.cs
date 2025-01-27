using Unity.VisualScripting;
using UnityEngine;

public class FOV
{
    public float radius;
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

}
