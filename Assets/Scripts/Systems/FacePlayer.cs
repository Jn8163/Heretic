using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform player;

	private void Start()
	{
		player = GameObject.Find("Player").transform;
	}

	private void Update()
	{
		transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z), Vector3.up);
	}
}
