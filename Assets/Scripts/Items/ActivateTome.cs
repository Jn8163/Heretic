using System.Collections;
using UnityEngine;

public class ActivateTome : MonoBehaviour
{
    public static bool isCharged;

	private void OnDisable()
	{
		isCharged = false;
	}

	public void TomeUsed()
	{
		StartCoroutine(nameof(TomeTimer));
	}

	IEnumerator TomeTimer()
	{
		Debug.Log("timer start");
		isCharged = true;
		yield return new WaitForSeconds(40);
		Debug.Log("timer up");
		isCharged = false;
	}
}
