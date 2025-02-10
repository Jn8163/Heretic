using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DisableDifficultyButtons : MonoBehaviour
{
    [SerializeField] private List<Button> buttonList = new List<Button>();
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
}
