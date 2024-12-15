using UnityEngine;

public class StatResetter : MonoBehaviour
{
	private void OnEnable()
	{
		// We need this to be called at the start of every level
		// Otherwise, the counters tracking the stats for the endscreen will keep increasing past the limit
		StatTracker.ResetStats();
	}
}
