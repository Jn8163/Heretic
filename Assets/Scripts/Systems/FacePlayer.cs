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
		transform.LookAt(player, Vector3.up);
	}
}