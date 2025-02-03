using UnityEngine;

public class FlexibleStun : MonoBehaviour
{
    [SerializeField] private int stunValue = 1;
	[SerializeField] private int knockbackValue = 1;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out StunSystem stunSystem))
		{
			stunSystem.TryStun(stunValue, knockbackValue);
		}
	}
}
