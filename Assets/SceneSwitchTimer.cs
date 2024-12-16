using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchTimer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private string sceneName;
    void Start()
    {
        StartCoroutine(nameof(SwitchScene));
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(time);

		SceneManager.LoadScene(sceneName);
	}
}
