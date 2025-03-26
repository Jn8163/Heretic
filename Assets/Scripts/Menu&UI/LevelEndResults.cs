using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kills, items, secrets, time, timeCounter;
	private float waitTime = 18f / 21f;
	private int counter = 0;
	private float timer = 0;

	private bool isInterrupted;

	[SerializeField] private AudioSource aSource;
	[SerializeField] private string sceneName;

	public static bool testBool = false;

	private void Start()
	{
		Debug.Log("grug");
		StartCoroutine(nameof(DisplayStats));
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (Input.anyKeyDown && isInterrupted)
		{
			Debug.Log("load scene");
			SceneManager.LoadScene(sceneName);
		}
		if (Input.anyKeyDown && !isInterrupted && timer > 1)
		{
			ForceDisplay();
		}
	}

	IEnumerator DisplayStats()
	{
		yield return new WaitForSecondsRealtime(waitTime);

		if (!isInterrupted)
		{
			Debug.Log("is not interrupted");
			if (counter == 0)
			{
				kills.text = StatTracker.killCount + " / " + StatTracker.maxKills;
				aSource.Play();
			}
			if (counter == 1)
			{
				items.text = StatTracker.itemCount + " / " + StatTracker.maxItems;
				aSource.Play();
			}
			if (counter == 2)
			{
				secrets.text = StatTracker.secretCount + " / " + StatTracker.maxSecrets;
				aSource.Play();
				waitTime *= 2;
			}
			if (counter == 3)
			{
				time.text = "time";
                if (StatTracker.time % 60 <= 9)
                {
                    timeCounter.text = StatTracker.time / 60 + ":" + "0" + StatTracker.time % 60;
                }
                else
                {
                    timeCounter.text = StatTracker.time / 60 + ":" + StatTracker.time % 60;
                }
                aSource.Play();
				StartCoroutine(nameof(StartSwitchTimer));
			}
			counter++;

			if (counter < 4)
			{
				Looper();
			}
		}
	}

	IEnumerator StartSwitchTimer()
	{
		yield return new WaitForSecondsRealtime(5.333f);

		Debug.Log("timer complete");
		SceneManager.LoadScene(sceneName);
	}

	private void Looper()
	{
		StartCoroutine(nameof(DisplayStats));
	}

	private void ForceDisplay()
	{
		kills.text = StatTracker.killCount + " / " + StatTracker.maxKills;
		items.text = StatTracker.itemCount + " / " + StatTracker.maxItems;
		secrets.text = StatTracker.secretCount + " / " + StatTracker.maxSecrets;
		time.text = "time";
		if(StatTracker.time % 60 <= 9)
		{
            timeCounter.text = StatTracker.time / 60 + ":" + "0" + StatTracker.time % 60;
        }
		else
		{
            timeCounter.text = StatTracker.time / 60 + ":" + StatTracker.time % 60;
        }
		aSource.Play();

		isInterrupted = true;
		StartCoroutine(nameof(StartSwitchTimer));
	}
}
