using System.Collections;
using UnityEngine;

public class Golem : EnemyBaseClass
{
    [SerializeField] private float zigZagInterval = 1.5f; // Time between zig-zag switches
    [SerializeField] private float zigZagAmount = 3f; // How far to move sideways

    private bool movingRight = true;
    private Coroutine zigZagCoroutine = null;

    protected override void Update()
    {
        base.Update();

        if (isChasing && zigZagCoroutine == null)
        {
            zigZagCoroutine = StartCoroutine(ZigZagMovement());
        }
        else if (!isChasing && zigZagCoroutine != null)
        {
            StopCoroutine(zigZagCoroutine);
            zigZagCoroutine = null;
        }
    }

    IEnumerator ZigZagMovement()
    {
        while (isChasing)
        {
            Vector3 lateralMovement = transform.right * (movingRight ? 1 : -1) * zigZagAmount;
            Vector3 newTarget = player.position + lateralMovement;

            agent.SetDestination(newTarget);

            movingRight = !movingRight;
            yield return new WaitForSeconds(zigZagInterval);
        }

        // Just in case isChasing becomes false within the loop
        zigZagCoroutine = null;
    }
}
