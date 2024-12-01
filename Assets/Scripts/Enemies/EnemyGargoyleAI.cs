using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGargoyleAI : EnemyBaseClass
{
	[SerializeField]
	private float hoverDist;
	[SerializeField]
	private float DashRange = 11;
	RaycastHit hit;
	Vector3 pos;
	protected override void Awake()
	{
		base.Awake();
		// test
	}
	protected override void Update()
	{
		// send out raycast downward to check for ground below
		// have gargoyle hover a certain distance above the ground
		if (Physics.Raycast(transform.position, -transform.up, out hit)){
			pos = transform.position;
			pos.y = hit.point.y;
			pos.y += hoverDist;
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
                Debug.Log("dash");
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
