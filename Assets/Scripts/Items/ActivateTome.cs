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
#if UNITY_EDITOR
		Debug.Log("timer start");
#endif
		isCharged = true;
		yield return new WaitForSeconds(40);
#if UNITY_EDITOR
		Debug.Log("timer up");
#endif
		isCharged = false;
	}
}
