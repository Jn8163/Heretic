using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Management of menus through preset inspector refrences.
/// Menu logs in DDOL- if another instance is created it's destroyed.
/// </summary>
public class MenuSystem : MonoBehaviour, IManageData
{
    #region Fields
    public static MenuSystem instance;
    public List<GameObject> continues = new List<GameObject>();
    [SerializeField] private GameObject mainM, settingsM, loadM, saveM, pauseM, deathM, episodeM, difficultyM, creditsM, statsM, playerHUD;
    [SerializeField] private List<GameObject> menus = new List<GameObject>();
    public int selectedLevel, selectedDifficulty = 1, unlockedLevel;
    private bool activeHUD, destroy = false;
    private DataManager dManager;

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
        }
        else if(instance != this)
        {
            destroy = true;
            gameObject.SetActive(false);
        }
    }



    private void CallGameOverScreen()
    {
        ActivateMenu(deathM);
    }



    private void OnEnable()
    {
        dManager = DataManager.instance;

        HealthSystem.GameOver += CallGameOverScreen;
        PauseSystem.PauseMenuActive += PauseMenu;
        SceneInitializer.MenuActiveOnStart += SwitchMenu;
        SceneInitializer.PlayerHUDActive += PlayerHUDActive;
        CallStatsMenu.CallStats += SwitchMenu;
    }



    private void Start()
    {
        playerHUD = GameObject.FindWithTag("PlayerUI");
        if (playerHUD)
        {
            menus.Add(playerHUD);
            activeHUD = true;
        }
    }



    private void Update()
    {
        if (continues.Count > 0)
        {
            foreach(GameObject g in continues)
            {
                g.SetActive(dManager.profilesFound);
            }
        }
    }



    private void OnDisable()
    {
        if (!destroy)
        {
            if (playerHUD)
            {
                menus.Remove(playerHUD);
            }
            HealthSystem.GameOver -= CallGameOverScreen;
            PauseSystem.PauseMenuActive -= PauseMenu;
            SceneInitializer.MenuActiveOnStart -= SwitchMenu;
            SceneInitializer.PlayerHUDActive -= PlayerHUDActive;
			CallStatsMenu.CallStats -= SwitchMenu;
		}
        else
        {
            Destroy(gameObject);
        }
    }



    private void PauseMenu(bool b)
    {
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
            if (playerHUD)
            {
                playerHUD.SetActive(activeHUD);
            }
            g.SetActive(true);
            MenuActive(true);
            if(g != statsM)
                FreezeTime(true);
        }
        else
        {
            Debug.Log("Menu was not located identified in inspector.");
        }
    }



    private void DeactivateAllMenus()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i]) {
                menus[i].SetActive(false);
            }
            else
            {
                menus.RemoveAt(i);
            }
        }

        if (playerHUD && activeHUD)
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
                if (mainM)
                {
                    ActivateMenu(mainM);
                }
                return;
            case "SettingsMenu":
                if (settingsM)
                {
                    ActivateMenu(settingsM);
                }
                return;
            case "LoadMenu":
                ActivateMenu(loadM);
                //Debug.Log(targetMenu + " not implemented");
                return;
            case "SaveMenu":
                ActivateMenu(saveM);
                return;
            case "PauseMenu":
                if (pauseM)
                {
                    ActivateMenu(pauseM);
                }
                return;
            case "DeathMenu":
                if (deathM)
                {
                    ActivateMenu(deathM);
                }
                return;
            case "EpisodeMenu":
                if (episodeM)
                {
                    ActivateMenu(episodeM);
                }
                return;
            case "DifficultyMenu":
                if (difficultyM)
                {
                    ActivateMenu(difficultyM);
                }
                return;
            case "CreditsMenu":
                if (creditsM)
                {
                    ActivateMenu(creditsM);
                }
                return;
			case "StatsMenu":
				if (statsM)
				{
					ActivateMenu(statsM);
				}
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
        ChangeScene();
    }



    public void SetLevel(int i)
    {
        selectedLevel = i;
        if(unlockedLevel < i)
        {
            unlockedLevel = i;
        }
    }



    public void SetDifficulty(int i)
    {
        selectedDifficulty = i;
    }



    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(selectedLevel);
    }



    public void ResumeCall()
    {
        Resume();
    }



    public void ResetCall()
    {
        //Reset();
        ResetScript();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }



    public void PlayerHUDActive(bool b)
    {
        activeHUD = b;
        playerHUD = GameObject.FindWithTag("PlayerUI");
        if (playerHUD && activeHUD)
        {
            menus.Add(playerHUD);
            activeHUD = true;
        }
    }

    private void ResetScript()
    {
        DeactivateAllMenus();

        if (playerHUD)
        {
            menus.Remove(playerHUD);
            activeHUD = true;
        }
    }

    public void LoadData(GameData data)
    {
        SetLevel(data.currentLevel);
    }

    public void SaveData(ref GameData data)
    {
        data.currentLevel = unlockedLevel;
    }

    #endregion

}