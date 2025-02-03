using System.Collections;
using UnityEngine;

public class Golem : EnemyBaseClass
{
    [SerializeField] private float zigZagInterval = 1.5f; // Time between zig-zag switches
    [SerializeField] private float zigZagAmount = 3f; // How far to move sideways

    private bool movingRight = true;

    protected override void Update()
    {
        base.Update();

        if (isChasing)
        {
            StartCoroutine(ZigZagMovement());
        }
    }

    IEnumerator ZigZagMovement()
    {
        while (isChasing)
        {
            // Compute sideways movement
            Vector3 lateralMovement = transform.right * (movingRight ? 1 : -1) * zigZagAmount;
            Vector3 newTarget = player.position + lateralMovement;

            // Move toward the zig-zag point
            agent.SetDestination(newTarget);

            // Switch direction
            movingRight = !movingRight;
            yield return new WaitForSeconds(zigZagInterval);
        }
    }
}
