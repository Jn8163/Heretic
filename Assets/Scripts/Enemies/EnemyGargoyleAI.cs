using UnityEngine;

public class EnemyGargoyleAI : EnemyBaseClass
{
	[SerializeField]
	private float hoverDist;
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
}
