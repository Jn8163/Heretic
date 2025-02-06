using UnityEngine;

public class LockMapCameraRotation : MonoBehaviour
{
	private GameObject player;
	private void Start()
	{
		player = GameObject.Find("Player");
	}
	private void Update()
	{
		transform.position = player.transform.position;
	}
}
