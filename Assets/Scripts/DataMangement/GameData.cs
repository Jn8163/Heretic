using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Save Profile")]
    public long lastUpdated;

    [Header("Game Progress")]
    public int currentLevel;
    public int levelUnlocked;
    public bool yKey;
    public bool gKey;
    public bool bKey;

    [Header("Player Stats")]
    public Vector3 playerPosition;
    public Vector4 playerRotation;
    public int pMaxShield;
    public int pCurrentShieldH;
    public float pArmorReductionAmount;
    public bool shieldEquipped;
    public SerializableDictionary<int, int> ammoMax;
    public SerializableDictionary<int, int> currentAmmo;
    public List<bool> weaponsAquired;
    public bool staffActive;
    public bool gauntletUnlocked;
    public int currentWeapon;

    [Header("Level Stats")]
    public SerializableDictionary<string, bool> spawnPointStatues;
    public SerializableDictionary<string, int> currentHealths;
    public SerializableDictionary<string, bool> deathState;
    public SerializableDictionary<string, bool> doorsOpen;
    public SerializableDictionary<string, bool> buttonsPressed;
    public SerializableDictionary<string, Vector3> enemyPositions;
    public SerializableDictionary<string, Vector4> enemyRotations;

    //New Save values
    public GameData()
    {
        currentLevel = 1;
        levelUnlocked = 1;
        yKey = false;
        gKey = false;
        bKey = false;

        playerPosition = Vector3.zero;
        pMaxShield = 200;
        pCurrentShieldH = 0;
        pArmorReductionAmount = 0;
        shieldEquipped = false;
        ammoMax = new SerializableDictionary<int, int>();
        currentAmmo = new SerializableDictionary<int, int>();
        weaponsAquired = new List<bool>();
        staffActive = false;
        gauntletUnlocked = false;
        currentWeapon = 1;

    spawnPointStatues = new SerializableDictionary<string, bool>();
        currentHealths = new SerializableDictionary<string, int>();
        deathState = new SerializableDictionary<string, bool>();
        doorsOpen = new SerializableDictionary<string, bool>();
        buttonsPressed = new SerializableDictionary<string, bool>();
        enemyPositions = new SerializableDictionary<string, Vector3>();
        enemyRotations = new SerializableDictionary<string, Vector4>();
    }
}