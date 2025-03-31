using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DisableDifficultyButtons : MonoBehaviour
{
    [SerializeField] private List<Button> buttonList = new List<Button>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public void DisableButton()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].interactable = false;
        }
    }

    public void ReEnableButton()
    {
		for (int i = 0; i < buttonList.Count; i++)
		{
			buttonList[i].interactable = true;
		}
	}

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ReEnableButton();
    }
}
