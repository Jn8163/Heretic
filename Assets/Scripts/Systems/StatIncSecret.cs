using UnityEngine;

public class StatIncSecret : MonoBehaviour
{
	private void Start()
	{
		StatTracker.maxSecrets++;
	}
	private void OnTriggerEnter(Collider other)
	{
		StatTracker.secretCount++;
		GetComponent<Collider>().enabled = false;
	}
}
