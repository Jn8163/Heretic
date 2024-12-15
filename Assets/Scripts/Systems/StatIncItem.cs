using UnityEngine;

public class StatIncItem : MonoBehaviour
{
	private void Start()
	{
		StatTracker.maxItems++;
		Debug.Log("Max Items: " + StatTracker.maxItems);
	}
	private void OnTriggerEnter(Collider other)
	{
		StatTracker.itemCount++;
		Debug.Log("Item Count: " + StatTracker.itemCount);
	}
}
