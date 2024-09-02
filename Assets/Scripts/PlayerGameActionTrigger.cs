using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameActionTrigger : MonoBehaviour
{
    [SerializeField]
    private List<GameAction> actions;

    [SerializeField]
    private bool bToggle;

	private bool bActive, bDeaction;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (bActive)
				return;

			bActive = true;
			StartCoroutine(nameof(ExecuteActions));
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if(bActive)
				return;

			if (bToggle)
			{
				bActive = true;
				bDeaction = true;
				StartCoroutine(nameof(ExecuteActions));
			}
		}
	}

	IEnumerator ExecuteActions()
	{
		foreach (GameAction action in actions)
		{
			yield return new WaitForSeconds(action.delay);

			if (bDeaction)
			{
				action.DeAction();
			}
			else
			{
				action.Action();
			}
		}
		bActive = false;
		bDeaction = false;
	}
}
