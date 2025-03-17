using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Save Profile")]
    public long lastUpdated;

    [Header("Game Progress")]
    public int currentLevel;
    public int levelUnlocked;

    public Vector3 playerPosition;
    public Vector4 playerRotation;
    public SerializableDictionary<string, bool> spawnPointStatues;
    public SerializableDictionary<string, int> currentHealths;
    public SerializableDictionary<string, bool> deathState;

    //New Save values
    public GameData()
    {
        currentLevel = 1;
        levelUnlocked = 1;
        playerPosition = Vector3.zero;
        spawnPointStatues = new SerializableDictionary<string, bool>();
    }
}