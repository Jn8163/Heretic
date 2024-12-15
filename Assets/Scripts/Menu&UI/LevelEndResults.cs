using TMPro;
using UnityEngine;

public class LevelEndResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kills, items, secrets;

	private void Start()
	{
		kills.text = StatTracker.killCount + " / " + StatTracker.maxKills;
		items.text = StatTracker.itemCount + " / " + StatTracker.maxItems;
		secrets.text = StatTracker.secretCount + " / " + StatTracker.maxSecrets;
	}
}
