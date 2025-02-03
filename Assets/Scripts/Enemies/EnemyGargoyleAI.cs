using UnityEngine;
using System.Collections;

public class EnemyGargoyleAI : EnemyBaseClass
{
    [SerializeField] private float hoverDist;
    [SerializeField] private float DashRange = 11;
    [SerializeField] private float randomMoveInterval = 1.2f;
    [SerializeField] private float hoverMoveRadius = 2f;
    [SerializeField] private float dodgeChance = 0.3f; // 30% chance to dodge

    private RaycastHit hit;
    private Vector3 pos;

    protected override void Update()
    {
        // Keep hovering at a set height above the ground
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            pos = transform.position;
            pos.y = hit.point.y + hoverDist;
            transform.position = pos;
        }

        if (isChasing)
        {
            StartCoroutine(RandomHoverMovement());
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

    IEnumerator RandomHoverMovement()
    {
        while (isChasing)
        {
            // Slightly random movement in air
            Vector3 randomOffset = new Vector3(
                Random.Range(-hoverMoveRadius, hoverMoveRadius),
                Random.Range(-hoverMoveRadius, hoverMoveRadius),
                Random.Range(-hoverMoveRadius, hoverMoveRadius)
            );

            Vector3 newTarget = player.position + randomOffset;
            agent.SetDestination(newTarget);

            // Occasional dodge to left/right
            if (Random.value < dodgeChance)
            {
                Vector3 dodgeDirection = (Random.value < 0.5f) ? transform.right : -transform.right;
                Vector3 jukeTarget = transform.position + dodgeDirection * hoverMoveRadius;
                agent.SetDestination(jukeTarget);
            }

            yield return new WaitForSeconds(randomMoveInterval);
        }
    }

    IEnumerator resetSpeed()
    {
        yield return new WaitForSeconds(1.5f);
        agent.speed = 5;
    }
}
