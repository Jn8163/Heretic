using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActivateRing : MonoBehaviour
{
    public static bool isInvincible;
	[SerializeField] private Image screenOverlay;

	private void OnDisable()
	{
		isInvincible = false;
		screenOverlay.color = new Vector4(1, 0.42f, 0.42f, 0.11f);
		screenOverlay.gameObject.SetActive(false);
	}

	public void RingUsed()
	{
		StartCoroutine(nameof(RingTimer));
	}

	IEnumerator RingTimer()
	{
		screenOverlay.color = new Vector4(1, 0.85f, 0.29f, 0.25f);
		screenOverlay.gameObject.SetActive(true);
		isInvincible = true;
		yield return new WaitForSeconds(30);
		screenOverlay.color = new Vector4(1, 0.42f, 0.42f, 0.11f);
		screenOverlay.gameObject.SetActive(false);
		isInvincible = false;
	}
}
