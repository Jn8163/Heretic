using UnityEngine;

public class FlipSprite : MonoBehaviour
{
	[SerializeField]
	private GameObject enemy;
	[SerializeField]
	private Animator anim;

    private Transform player;

	private Vector3 enemyLookDirection;
	private Vector3 playerRelativeDirection;
	private int animationIndex;

	private void Start()
	{
		player = GameObject.Find("Player").transform;
		
	}

	private void Update()
	{
		enemyLookDirection = enemy.transform.forward;
		playerRelativeDirection = (player.position - new Vector3(enemy.transform.position.x, player.position.y, enemy.transform.position.z)).normalized;

		float angle = Vector3.SignedAngle(enemyLookDirection, playerRelativeDirection, Vector3.up);


		if (-22.5 < angle && angle < 22.5)
		{
			// front
			anim.SetInteger("index", 1);
		}

		if (22.5 < angle && angle < 67.5)
		{
			// front right diagonal
			anim.SetInteger("index", 2);
		}

		if (67.5 < angle && angle < 112.5)
		{
			// right
			anim.SetInteger("index", 3);
		}

		if (112.5 < angle && angle < 157.5)
		{
			// back right diagonal
			anim.SetInteger("index", 4);
		}

		if (157.5 < angle || angle < -157.5)
		{
			// back
			anim.SetInteger("index", 5);
		}

		if (-157.5 < angle && angle < -112.5)
		{
			// back left diagonal
			anim.SetInteger("index", 6);
		}

		if (-112.5 < angle && angle < -67.5)
		{
			// left
			anim.SetInteger("index", 7);
		}

		if (-67.5 < angle && angle < -22.5)
		{
			// front left diagonal
			anim.SetInteger("index", 8);
		}


		////
	}
}
