using UnityEngine;

public class Spawn : MonoBehaviour
{
    public string id;
    public bool targetActive = true;
    [SerializeField] private GameObject target;
    [SerializeField] private int difficulty, currentDifficulty;
    [Tooltip("True if the object has more spawn points as difficulty increases. " +
        "Otherwise false. Ex: Enemies would be true because there are more " +
        "enemies at higher difficulties.")]
    [SerializeField] private bool Increasing;
    [Tooltip("Sometimes objects spawn in completely different locations for one " +
        "difficulty, if that's true check this box true")]
    [SerializeField] private bool OnlyThisDifficulty;
    [SerializeField] private bool isEnemy;

    private void Awake()
    {
        id = GetComponent<GameObjectID>().GetID();
    }

    public void SpawnObject()
    {
        currentDifficulty = MenuSystem.instance.selectedDifficulty;

        if (target && targetActive)
        {
            if (Increasing)
            {
                if (difficulty <= currentDifficulty)
                {
                    ToggleTarget(true);
                }
                else
                {
                    ToggleTarget(false);
                }
            }
            else
            {
                if (difficulty >= currentDifficulty)
                {
                    ToggleTarget(true);
                }
                else
                {
                    ToggleTarget(false);
                }
            }
        }else if (target)
        {
            ToggleTarget(false);
        }
    }

    public void ToggleTarget(bool isActive)
    {
        currentDifficulty = MenuSystem.instance.selectedDifficulty;

        if ((OnlyThisDifficulty && currentDifficulty == difficulty) || !OnlyThisDifficulty || !isActive)
        {
            target.SetActive(isActive);
            
            if (currentDifficulty == difficulty || (currentDifficulty >= difficulty) && Increasing && isActive)
            {
                if (isEnemy)
                {
                    StatTracker.maxKills++;
                    #if UNITY_EDITOR
                    Debug.Log("Max Kills: " + StatTracker.maxKills);
                    Debug.Log(target);
                    #endif
                }
            }
        }
        else
        {
            ToggleTarget(false);
        }
    }
}