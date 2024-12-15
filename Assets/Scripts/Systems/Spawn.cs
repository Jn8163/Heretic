using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int difficulty;
    [Tooltip("True if the object has more spawn points as difficulty increases. " +
        "Otherwise false. Ex: Enemies would be true because there are more " +
        "enemies at higher difficulties.")]
    [SerializeField] private bool Increasing;
    [Tooltip("Sometimes objects spawn in completely different locations for one " +
        "difficulty, if that's true check this box true")]
    [SerializeField] private bool OnlyThisDifficulty;

    /// <summary>
    /// Spawns prefab from inspector, based on the object they will spawn from 
    /// lowest to highest or reverse if needed.
    /// </summary>
    public void SpawnObject()
    {
        int currentDifficulty = MenuSystem.instance.selectedDifficulty;
        GameObject go;
        if (prefab)
        {
            if (Increasing)
            {
                Debug.Log(currentDifficulty);
                if (difficulty <= currentDifficulty)
                {
                    Debug.Log(prefab);
                    if (OnlyThisDifficulty)
                    {
                        if (currentDifficulty == difficulty)
                        {
                            go = Instantiate(prefab, transform);
                            go.transform.position += transform.up;
                            StatTracker.maxKills++;
                        }
                    }
                    else
                    {
                        go = Instantiate(prefab, transform);
                        go.transform.position += transform.up;
						StatTracker.maxKills++;
					}
                }
            }
            else
            {
                if (difficulty >= currentDifficulty)
                {
                    if (OnlyThisDifficulty)
                    {
                        if (currentDifficulty == difficulty)
                        {
                            go = Instantiate(prefab, transform);
                            go.transform.position += transform.up;
                        }
                    }
                    else
                    {
                        go = Instantiate(prefab, transform);
                        go.transform.position += transform.up;
                    }
                }
            }
        }
        Debug.Log(StatTracker.maxKills);
    }
}