using Unity.VisualScripting;
using UnityEngine;

public class RevealMap : MonoBehaviour
{
	// private GameObject[] goArray = new GameObject[10000];
	private void Start()
	{
		/*goArray = FindObjectsByType<GameObject>(FindObjectsSortMode.None) as GameObject[];
		for (int i = 0; i < goArray.Length; i++)
		{
			if (goArray[i].layer == 10)
			{
				Debug.Log("Disabled the map mesh");
				goArray[i].GetComponent<MeshRenderer>().enabled = false;
			}
		}*/
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 10)
		{
			if (other.TryGetComponent<MeshRenderer>(out MeshRenderer mRenderer))
			{
				mRenderer.enabled = true;
			}
			if (other.TryGetComponent<SpriteRenderer>(out SpriteRenderer sRenderer))
			{
				sRenderer.enabled = true;
			}
		}
	}
}
