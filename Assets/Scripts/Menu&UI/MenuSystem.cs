using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Management of menus through preset inspector refrences.
/// Menu logs in DDOL- if another instance is created it's destroyed.
/// </summary>
public class MenuSystem : MonoBehaviour
{
    #region Fields
    private static MenuSystem instance;
    [SerializeField] private GameObject mainM, settingsM, loadM, pauseM, deathM, episodeM, difficultyM, playerHUD;
    [SerializeField] private List<GameObject> menus = new List<GameObject>();
    private int selectedLevel, selectedDifficulty;

    public static Action<bool> MenuActive = delegate { };
    public static Action<bool> FreezeTime = delegate { };
    public static Action Reset = delegate { };
    public static Action Restart = delegate { };
    public static Action Resume = delegate { };

    #endregion



    #region Methods

    private void Awake()
    {
        //Ensures only one instance is active in scene at all times.
        //DDOL to preserve states
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }



    private void CallGameOverScreen()
    {
        ActivateMenu(deathM);
    }



    private void OnEnable()
    {
        HealthSystem.GameOver += CallGameOverScreen;
        PauseSystem.PauseMenuActive += PauseMenu;
        SceneInitializer.MenuActiveOnStart += SwitchMenu;
    }



    private void Start()
    {
        playerHUD = GameObject.FindWithTag("PlayerUI");
        if (playerHUD)
        {
            menus.Add(playerHUD);
        }
    }



    private void OnDisable()
    {
        HealthSystem.GameOver -= CallGameOverScreen; 
        PauseSystem.PauseMenuActive -= PauseMenu;
        SceneInitializer.MenuActiveOnStart -= SwitchMenu;
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
            if (playerHUD)
            {
                playerHUD.SetActive(true);
            }
        }
    }



    private void ActivateMenu(GameObject g)
    {
        if (g)
        {
            DeactivateAllMenus();
            g.SetActive(true);
            MenuActive(true);
            FreezeTime(true);
        }
        else
        {
            Debug.Log("Menu was not located identified in inspector.");
        }
    }



    private void DeactivateAllMenus()
    {
        foreach (GameObject g in menus)
        {
            g.SetActive(false);
        }

        if (playerHUD)
        {
            playerHUD.SetActive(true);
        }
        MenuActive(false);
        FreezeTime(false);
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
        DeactivateAllMenus();
        SceneManager.LoadScene(selectedLevel);
    }



    public void ResumeCall()
    {
        Resume();
    }



    public void ResetCall()
    {
        //Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    #endregion
}