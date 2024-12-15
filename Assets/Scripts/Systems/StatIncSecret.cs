using UnityEngine;

public class StatIncSecret : MonoBehaviour
{
	private void Start()
	{
		StatTracker.maxSecrets++;
		Debug.Log("Max Secrets: " + StatTracker.maxSecrets);
	}
	private void OnTriggerEnter(Collider other)
	{
		StatTracker.secretCount++;
		Debug.Log("Secret Count: " + StatTracker.secretCount);
		GetComponent<Collider>().enabled = false;
	}
}
