using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssetSwapEntities : MonoBehaviour
{
    private PlayerInput pInput;
	private bool graphicsToggle; // false is old graphics, true is new graphics

	public static Action<bool> SwapGraphics = delegate { };

	private void Start()
	{
		pInput = new PlayerInput();
		pInput.Enable();
		graphicsToggle = false;

		pInput.Player.GraphicsSwap.performed += Swap;
	}

	private void OnDisable()
	{
		pInput.Player.GraphicsSwap.performed -= Swap;
	}

	private void Swap(InputAction.CallbackContext c)
	{
		graphicsToggle = !graphicsToggle;

		SwapGraphics(graphicsToggle);
	}
}
