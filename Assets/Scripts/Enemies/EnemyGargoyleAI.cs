using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyGargoyleAI : EnemyBaseClass
{
    [SerializeField] private float hoverDist;
    [SerializeField] private float DashRange = 11;
    private RaycastHit hit;
    private Vector3 pos;

    protected override void Update()
    {
        // Keep the Gargoyle hovering at a set height above the ground
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            pos = transform.position;
            pos.y = hit.point.y + hoverDist;
            transform.position = pos;
        }

        base.Update();
    }

    protected override void ChasePlayer()
    {
        Vector3 distanceToPlayer = transform.position - player.position;
        float distance = distanceToPlayer.magnitude;

        if (Mathf.Abs(distance - DashRange) < 5)
        {
            int dashChance = Random.Range(1, 10);
            if (dashChance == 1)
            {
                agent.speed = 15;
                StartCoroutine(nameof(resetSpeed));
            }
        }
        base.ChasePlayer();
    }

    IEnumerator resetSpeed()
    {
        yield return new WaitForSeconds(1.5f);
        agent.speed = 5;
    }
}
