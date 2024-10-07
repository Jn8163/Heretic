using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private Transform target;

	private void Start()
	{
		target = GameObject.Find("Player").transform;
	}

	private void Update()
	{
		transform.LookAt(target);
	}
}
