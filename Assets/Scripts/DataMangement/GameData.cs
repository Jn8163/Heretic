using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Game Progress")]
    public int currentLevel;
    public int levelUnlocked;

    public long lastUpdated;
    public int deathCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> spawnPointStatues;

    //New Save values
    public GameData()
    {
        currentLevel = 1;
        levelUnlocked = 1;
        deathCount = 0;
        playerPosition = Vector3.zero;
        spawnPointStatues = new SerializableDictionary<string, bool>();
    }
}