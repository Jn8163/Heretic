using System.Collections;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float radius;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    private GameObject player;

    private Vector3 tmp;
    private RaycastHit hit;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Vector3 directionToTarget = (player.transform.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToTarget > radius)
            distanceToTarget = radius;

        if(Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, targetMask))
            canSeePlayer = true;
        else
            canSeePlayer = false;

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
	}
}
