using UnityEngine;

public class StatIncItem : MonoBehaviour
{
	private void Start()
	{
		StatTracker.maxItems++;
	}
	private void OnTriggerEnter(Collider other)
	{
		StatTracker.itemCount++;
	}
}
