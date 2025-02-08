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
		Vector3 pos = transform.position;
		pos = player.transform.position;
		pos.y += 30;
		transform.position = pos;
	}
}
