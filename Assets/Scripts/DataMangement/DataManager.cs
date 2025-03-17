using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public bool profilesFound;
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("Auto Saving Configuration")]
    [SerializeField] private float autoSaveTimeSeconds = 60f;

    private GameData gameData;
    private List<IManageData> ManageableDataObjects;
    private DataFileManagement DFileManager;

    private string selectedProfileId = "";

    private Coroutine autoSaveCoroutine;

    public static DataManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }

        DFileManager = new DataFileManagement(Application.persistentDataPath, fileName, useEncryption);

        InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        profilesFound = selectedProfileId != "";

        LoadGame();

        // start up the auto saving coroutine
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        if (selectedProfileId.CompareTo(newProfileId) != 0) {
            // update the profile to use for saving and loading
            selectedProfileId = newProfileId;
            // load the game, which will use that profile, updating our game data accordingly
            LoadGame();
        }
    }

    public void DeleteProfileData(string profileId)
    {
        // delete the data for this profile id
        DFileManager.Delete(profileId);
        // initialize the selected profile id
        InitializeSelectedProfileId();
        // reload the game so that our data matches the newly selected profile id
        LoadGame();
    }

    private void InitializeSelectedProfileId()
    {
        selectedProfileId = DFileManager.GetMostRecentlyUpdatedProfileId();

        if (overrideSelectedProfileId)
        {
            selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Overrode selected profile id with test id: " + testSelectedProfileId);
        }
        profilesFound = selectedProfileId != "";
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }

        if (selectedProfileId != "")
        {
            // load any saved data from a file using the data handler
            gameData = DFileManager.Load(selectedProfileId);
        }

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        ManageableDataObjects = FindAllDataPersistenceObjects();

        // push the loaded data to all other scripts that need it
        foreach (IManageData dataPersistenceObj in ManageableDataObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence || selectedProfileId == "")
        {
            return;
        }

        // if we don't have any data to save, log a warning here
        if (gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        ManageableDataObjects = FindAllDataPersistenceObjects();

        // pass the data to other scripts so they can update it
        foreach (IManageData dataPersistenceObj in ManageableDataObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // timestamp the data so we know when it was last saved
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        DFileManager.Save(gameData, selectedProfileId);
        Debug.Log(gameData.playerPosition);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IManageData> FindAllDataPersistenceObjects()
    {
        IEnumerable<IManageData> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IManageData>();

        return new List<IManageData>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return DFileManager.LoadAllProfiles();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}