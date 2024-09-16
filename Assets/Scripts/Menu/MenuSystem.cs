using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// On Input/Menu Button change state and track 
/// </summary>
public class MenuSystem : MonoBehaviour
{
    #region Fields
    [SerializeField] private string startM;
    [SerializeField] private bool main, settings, load, pause, death, episode, difficulty, HUD;
    private GameObject mainM, settingsM, loadM, pauseM, deathM, episodeM, difficultyM, playerHUD;
    private List<GameObject> menus = new List<GameObject>();
    private int selectedLevel, selectedDifficulty;

    #endregion



    #region Methods

    private void Awake()
    {
        #region GameObject Assignment
        if (main && (mainM = transform.Find("MainMenu").gameObject))
        {
            menus.Add(mainM);
        }
        
        if (settings && (settingsM = transform.Find("SettingsMenu").gameObject))
        {
            menus.Add(settingsM);
        }
        
        if (load && (loadM = transform.Find("LoadMenu").gameObject))
        {
            menus.Add(loadM);
        }
        
        if (pause && (pauseM = transform.Find("PauseMenu").gameObject))
        {
            menus.Add(pauseM);
        }

        if (death && (deathM = transform.Find("DeathMenu").gameObject))
        {
            menus.Add(deathM);
        }
        
        if (episode && (episodeM = transform.Find("EpisodeMenu").gameObject))
        {
            menus.Add(episodeM);
        }
        
        if (difficulty && (difficultyM = transform.Find("DifficultyMenu").gameObject))
        {
            menus.Add(difficultyM);
        }

        if (HUD)
        {
            playerHUD = transform.Find("PlayerHUD").gameObject;
        }

        #endregion
    }



    private void Start()
    {
        SwitchMenu(startM);
    }



    private void OnEnable()
    {
        SwitchMenu(startM);
    }



    public void SwitchMenu(string targetMenu)
    {
        switch (targetMenu)
        {
            case "MainMenu":
                ActivateMenu(mainM);
                return;
            case "SettingsMenu":
                //ActivateMenu(settingsM);
                Debug.Log(targetMenu + " not implemented");
                return;
            case "LoadMenu":
                //ActivateMenu(loadM);
                Debug.Log(targetMenu + " not implemented");
                return;
            case "PauseMenu":
                ActivateMenu(pauseM);
                return;
            case "DeathMenu":
                ActivateMenu(deathM);
                return;
            case "EpisodeMenu":
                ActivateMenu(episodeM);
                return;
            case "DifficultyMenu":
                ActivateMenu(difficultyM);
                return;
            case null:
                Debug.Log("Menu to switch to was not specified - please enter a menu name in the inspector.");
                return;
            default:
                Debug.Log("Menu name isn't accounted for.");
                return;

        }
    }



    private void ActivateMenu(GameObject g)
    {
        if (g)
        {
            DeactivateAllMenus();
            g.SetActive(true);
        }
        else
        {
            Debug.Log("Menu was not located in scene");
        }
    }



    private void DeactivateAllMenus()
    {
        foreach(GameObject g in menus)
        {
            g.SetActive(false);
        }
    }



    public void Continue()
    {
        Debug.Log("Save-System not implemented: Currently Starts first level");

        SetLevel(1);
        ChangeScene();
    }



    public void SetLevel(int i)
    {
        selectedLevel = i;
    }



    public void SetDifficulty(int i)
    {
        selectedDifficulty = i;
    }


    
    public void ChangeScene()
    {
        SceneManager.LoadScene(selectedLevel);
    }



    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    #endregion
}