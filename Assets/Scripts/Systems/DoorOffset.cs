using UnityEngine;

public class DoorOffset : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer mRenderer;
	[SerializeField]
	private float offset;

	private void Start()
	{
		// mRenderer = GetComponent<MeshRenderer>();
		mRenderer.material.SetFloat("_yOffset", offset - (transform.position.y / 2));
	}
}
