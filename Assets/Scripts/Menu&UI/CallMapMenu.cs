using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CallMapMenu : MonoBehaviour
{
    private PlayerInput pInput;
	private bool isOpen;

	[SerializeField] private GameObject mapMenu;

	private void OnEnable()
	{
		pInput = new PlayerInput();
		pInput.Enable();
		pInput.Player.Map.performed += OpenMap;
	}

	private void OnDisable()
	{
		pInput.Disable();
		pInput.Player.Map.performed -= OpenMap;
	}

	private void OpenMap(InputAction.CallbackContext c)
	{
		if (!isOpen)
		{
			isOpen = true;
			mapMenu.SetActive(true);
		}
		else
		{
			isOpen = false;
			mapMenu.SetActive(false);
		}
		
	}
}
