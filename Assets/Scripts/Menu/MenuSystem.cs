using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Managment of Menus in scene and buttons included in menus.
/// 
/// If bool is set to true, will locate corresponding menu.
/// On menu switch deactivates any located menu and activates the target menu.
/// Any menu button action flows through this script.
/// Pause menu is activated in this script from PauseSystem script.
/// </summary>
public class MenuSystem : MonoBehaviour
{
    #region Fields
    [SerializeField] private string startM;
    [SerializeField] private bool onStartActive, main, settings, load, pause, death, episode, difficulty, HUD;
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
            playerHUD = GameObject.FindWithTag("PlayerUI");
            menus.Add(playerHUD);
        }

        HealthSystem.GameOver += CallGameOverScreen;
        #endregion
    }


    private void CallGameOverScreen()
    {
        ActivateMenu(deathM);
        Cursor.visible = true;
    }

    private void Start()
    {
        if (onStartActive)
        {
            SwitchMenu(startM);
        }
    } 



    private void OnEnable()
    {
        PauseSystem.PauseMenuActive += PauseMenu;
        if (onStartActive)
        {
            SwitchMenu(startM);
        }
    }



    private void OnDisable()
    {
        PauseSystem.PauseMenuActive -= PauseMenu;
    }



    private void PauseMenu(bool b)
    {
        Debug.Log("Pause " + b);
        if (b)
        {
            SwitchMenu("PauseMenu");
        }
        else
        {
            DeactivateAllMenus();
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
    }   //Method called by buttons to switch menu.



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